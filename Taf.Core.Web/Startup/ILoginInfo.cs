// 何翔华
// Taf.Core.Web
// IApplicationService.cs

namespace Taf.Core.Web;

public interface ILoginInfo{

    /// <summary>
    /// 
    /// </summary>
    Guid? UserId{ get; }

    /// <summary>
    /// 
    /// </summary>
    string? Name{ get; }

    /// <summary>
    /// 
    /// </summary>
    string? Authorization{ get; }

    /// <summary>
    /// 
    /// </summary>
    string? Email{ get; }

    /// <summary>
    /// 
    /// </summary>
    string TraceId{ get; }

    /// <summary>
    /// 
    /// </summary>
    string LangKey{ get; }

    /// <summary>
    ///
    /// </summary>
    int? TenantId{ get; }

    /// <summary>
    /// 使用1_55,2_8表示权限
    /// </summary>
    Dictionary<int, long> Permissions{ get; }
}
