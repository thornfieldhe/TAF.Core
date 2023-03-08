// 何翔华
// Taf.Core.Extension
// IDto.cs

namespace Taf.Core.Extension;

/// <summary>
/// Dto 对象Id可以为空
/// </summary>
public interface IDto  {
    Guid? Id{ get; set; }
}
