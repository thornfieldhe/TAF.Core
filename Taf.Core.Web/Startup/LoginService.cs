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
                                         .SingleOrDefault(r => r.Key.ToLower() == "userid").Value
                                         .FirstOrDefault();
            return Guid.TryParse(u, out var uId) ? uId : null;
        }
    }

    /// <summary>
    /// 用户姓名 
    /// </summary>
    public string? Name =>
        _httpContextAccessor?.HttpContext.Request.Headers
                             .SingleOrDefault(r => r.Key.ToLower() == "name").Value.FirstOrDefault();

    /// <summary>
    /// JWT Token 
    /// </summary>
    public string? Authorization =>
        _httpContextAccessor?.HttpContext.Request.Headers
                             .SingleOrDefault(r => r.Key.ToLower() == "authorization").Value
                             .FirstOrDefault();

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email =>
        _httpContextAccessor?.HttpContext.Request.Headers
                             .SingleOrDefault(r => r.Key.ToLower() == "email").Value
                             .FirstOrDefault();


    /// <summary>
    ///  追踪Id,用于跨系统跟踪请求
    /// </summary>
    public string TraceId{
        get{
            var traceId = _httpContextAccessor?.HttpContext.Request.Headers
                                               .SingleOrDefault(r => r.Key.ToLower() == "traceid").Value
                                               .FirstOrDefault();
            Fx.If(string.IsNullOrWhiteSpace(traceId)).Then(() => {
                traceId = Randoms.GetRandomCode(6,"0123456789abcdefghijklmnopqrstuvwxyz");
                _httpContextAccessor?.HttpContext.Request.Headers.TryAdd("traceid", traceId);
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
                                               .SingleOrDefault(r => r.Key.ToLower() == "langkey").Value
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
                                               .SingleOrDefault(r => r.Key.ToLower() == "tenantid").Value
                                               .FirstOrDefault();
            return int.TryParse(tenantId, out var id) ? id : null;
        }
    }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? PhoneNum =>
        _httpContextAccessor?.HttpContext.Request.Headers
                             .SingleOrDefault(r => r.Key.ToLower() == "phonenum").Value
                             .FirstOrDefault();

    /// <summary>
    /// 使用1_55,2_8表示权限
    /// </summary>
    public Dictionary<int, long> Permissions{
        get{
            var permissions =_httpContextAccessor?.HttpContext.Request.Headers
                                                 .SingleOrDefault(r => r.Key.ToLower() == "permissions").Value
                                                 .FirstOrDefault()
                                                ?.Split(',').ToList()
                          ?? new List<string>();
            var result = new Dictionary<int, long>();
            
            foreach(var permission in permissions){
                var group = permission.Split('_');
                if(group.Length == 2){
                    result.Add(group[0].ToInt(), group[1].ToLong());
                }
            }

            return result;
        }
    }
}
