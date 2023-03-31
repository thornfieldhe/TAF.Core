// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthExtend.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Security.Claims;

// 何翔华
// Taf.Core.Web
// AuthExtend.cs

namespace Taf.Core.Web;

using System;

/// <summary>
/// 授权扩展
/// </summary>
public static class AuthExtend{
    /// <summary>
    /// 添加框架默认授权
    /// </summary>
    /// <param name="services"></param>
    private static void AddAuth(IServiceCollection services){
        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, IkePermissionPolicyProvider>();
        services.AddAuthentication(HeaderAuthenticationDefaults.AuthenticationSchema)
                .AddHeader(HeaderAuthenticationDefaults.AuthenticationSchema
                         , options => {
                               options.AdditionalHeaderToClaims.Add("authorization", ClaimTypes.Authentication);
                           });
    }
}

/// <summary>
///     自定义授权策略
/// </summary>
public class IkeAuthorizationRequirement : IAuthorizationRequirement{
    public IkeAuthorizationRequirement(ulong permission) => Permission = permission;
    public ulong Permission{ get; init; }
}

/// <summary>
///     自定义授权处理类
/// </summary>
public class PermissionRequirementHandler : AuthorizationHandler<IkeAuthorizationRequirement>{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, IkeAuthorizationRequirement requirement){
        if(ulong.TryParse(context.User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Role)?.Value ?? "0"
                        , out var permissions)){
            if(permissions                            != 0
            && (permissions | requirement.Permission) == permissions){
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }

        context.Fail();
        return Task.CompletedTask;
    }
}

public class IkePermissionPolicyProvider : IAuthorizationPolicyProvider{
    private const string PolicyPrefix = "IkeAuthorize";

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
        Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());

    public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy>(null);

    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName){
        if(policyName.StartsWith(PolicyPrefix, StringComparison.OrdinalIgnoreCase)){
            if(ulong.TryParse(policyName.Substring(PolicyPrefix.Length), out var permission)){
                return Task.FromResult(new AuthorizationPolicyBuilder()
                                      .AddRequirements(new IkeAuthorizationRequirement(permission)).Build());
            }
        }

        return Task.FromResult<AuthorizationPolicy>(null);
    }
}
