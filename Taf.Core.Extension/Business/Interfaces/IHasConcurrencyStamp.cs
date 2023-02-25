// 何翔华
// Taf.Core.Net.Utility
// IHasConcurrencyStamp.cs

namespace Taf.Core.Extension;

public interface IHasConcurrencyStamp{
    /// <summary>
    /// 更新锁,用于确保乐观锁 
    /// </summary>
    public string ConcurrencyStamp{ get; set; } 
}
