// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationService.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Taf.Core.Utility;

// 何翔华
// Taf.Core.Web
// ApplicationService.cs

namespace Taf.Core.Web;

/// <summary>
/// 登录后用户信息
/// </summary>
public class LoginService : ILoginService,ITransientDependency{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public LoginService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;


    /// <summary>
    ///  UserId
    /// </summary>
    public Guid? UserId{
        get{
            var u = _httpContextAccessor?.HttpContext.Request.Headers
                                         .SingleOrDefault(r => r.Key.ToLower() == "UserId").Value
                                         .FirstOrDefault();
            return Guid.TryParse(u, out var uId) ? uId : null;
        }
    }

    /// <summary>
    /// 用户姓名 
    /// </summary>
    public string? Name =>
        _httpContextAccessor?.HttpContext.Request.Headers
                             .SingleOrDefault(r => r.Key.ToLower() == "Name").Value.FirstOrDefault();

    public string? Authorization{ get; }


    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email =>
        _httpContextAccessor?.HttpContext.Request.Headers
                             .SingleOrDefault(r => r.Key.ToLower() == "Email").Value
                             .FirstOrDefault();


    /// <summary>
    ///  追踪Id,用于跨系统跟踪请求
    /// </summary>
    public string TraceId{
        get{
            var traceId = _httpContextAccessor?.HttpContext.Request.Headers
                                               .SingleOrDefault(r => r.Key.ToLower() == "TraceId").Value
                                               .FirstOrDefault();
            Fx.If(string.IsNullOrWhiteSpace(traceId)).Then(() => {
                traceId = Randoms.GetRandomCode(6,"0123456789abcdefghijklmnopqrstuvwxyz");
                _httpContextAccessor?.HttpContext.Request.Headers.TryAdd("TraceId", traceId);
            });
            return traceId;
        }
    }

    /// <summary>
    /// 区域
    /// </summary>
    public string LangKey{
        get{
            var langKey = _httpContextAccessor?.HttpContext.Request.Headers
                                               .SingleOrDefault(r => r.Key.ToLower() == "LangKey").Value
                                               .FirstOrDefault();
            return string.IsNullOrWhiteSpace(langKey) ? "zh-CN" : langKey;
        }
    }

    /// <summary>
    /// 租户Id
    /// </summary>
    public int? TenantId{
        get{
            var tenantId = _httpContextAccessor?.HttpContext.Request.Headers
                                               .SingleOrDefault(r => r.Key.ToLower() == "TenantId").Value
                                               .FirstOrDefault();
            return int.TryParse(tenantId, out var id) ? id : null;
        }
    }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? PhoneNum =>
        _httpContextAccessor?.HttpContext.Request.Headers
                             .SingleOrDefault(r => r.Key.ToLower() == "PhoneNum").Value
                             .FirstOrDefault();

    /// <summary>
    /// 使用1_55,2_8表示权限
    /// </summary>
    public Dictionary<char, ulong> Permissions{
        get{
            return _httpContextAccessor?.HttpContext.Request.Headers
                                                 .SingleOrDefault(r => r.Key.ToLower() == "Permissions").Value
                                                 .FirstOrDefault()
                                                ?.As<IStringExt>().SplitToList(',').ToDictionary(
                                                    c => c[0], c => ulong.Parse(c.Substring(2)));
        }
    }
}
