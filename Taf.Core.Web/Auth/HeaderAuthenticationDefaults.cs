// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderAuthenticationDefaults.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

// 何翔华
// EfootprintPlus.HttpApi.Host
// HeaderAuthenticationDefaults.cs

namespace Taf.Core.Web;

/// <summary>
/// </summary>
public static class HeaderAuthenticationDefaults{
    public const string AuthenticationSchema = "Header";
}

public static class AuthenticationBuilderExtension{
#region AddHeader

    public static AuthenticationBuilder AddHeader(this AuthenticationBuilder builder) => builder.AddHeader(HeaderAuthenticationDefaults.AuthenticationSchema);

    public static AuthenticationBuilder AddHeader(this AuthenticationBuilder builder, string schema){
        return builder.AddHeader(schema, _ => { });
    }

    public static AuthenticationBuilder AddHeader(
        this AuthenticationBuilder          builder,
        Action<HeaderAuthenticationOptions> configureOptions) =>
        builder.AddHeader(HeaderAuthenticationDefaults.AuthenticationSchema,
                          configureOptions);

    public static AuthenticationBuilder AddHeader(
        this AuthenticationBuilder          builder, string schema,
        Action<HeaderAuthenticationOptions> configureOptions) =>
        builder.AddScheme<HeaderAuthenticationOptions, HeaderAuthenticationHandler>(schema,
                                                                                    configureOptions);

#endregion AddHeader
}

public class HeaderAuthenticationOptions : AuthenticationSchemeOptions{
    private string _authorizationHeaderName = "authorization";
    private string _delimiter               = ",";

    public string AuthorizationHeaderName{
        get => _authorizationHeaderName;
        set{
            if(!string.IsNullOrWhiteSpace(value)){
                _authorizationHeaderName = value;
            }
        }
    }


    /// <summary>
    ///     AdditionalHeaderToClaims
    ///     key: headerName
    ///     value: claimType
    /// </summary>
    public Dictionary<string, string> AdditionalHeaderToClaims{ get; } = new();

    public string Delimiter{
        get => _delimiter;
        set => _delimiter = string.IsNullOrEmpty(value) ? "," : value;
    }
}

public class HeaderAuthenticationHandler : AuthenticationHandler<HeaderAuthenticationOptions>{
    public HeaderAuthenticationHandler(
        IOptionsMonitor<HeaderAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder
      , ISystemClock                                 clock) : base(options, logger, encoder, clock){ }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync(){
        var token = Request.Headers[Options.AuthorizationHeaderName].ToString().Replace("Bearer ", "");
        if(string.IsNullOrWhiteSpace(token)){
            return Task.FromResult(AuthenticateResult.NoResult());
        }
        try
        {
            var payload = JwtBuilder.Create()
                                    .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                                    .WithSecret(SystemKeys.SecurityKey)
                                    .MustVerifySignature()
                                    .Decode<IDictionary<string, object>>(token);
            var claimIdentity = new ClaimsIdentity(
                new Claim[]{
                    new("name", payload["name"].ToString()), new("permissions", payload["permissions"].ToString())
                  , new("phoneNum", payload["phoneNum"].ToString())
                  , new("emailAddress", payload["emailAddress"].ToString()), new(ClaimTypes.Authentication, "true")
                }, "Basic");

            var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimIdentity), null, "IkeAuthorize");
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (TokenExpiredException){
            return Task.FromResult(AuthenticateResult.Fail("Token has expired"));
        }
        catch (SignatureVerificationException)
        {
            return Task.FromResult(AuthenticateResult.Fail("Token has invalid signature"));
        }
        return Task.FromResult(AuthenticateResult.Fail("用户未授权"));
    }
}
