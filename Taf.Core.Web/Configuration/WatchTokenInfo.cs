// 何翔华
// Taf.Core.Web
// WatchTokenInfo.cs

using Microsoft.Extensions.Primitives;

namespace Taf.Core.Web;

/// <summary>
/// CancellationTokenSource和IChangeToken组合实例来实现观察者模式
/// </summary>
public class WatchTokenInfo{
    public CancellationTokenSource CancellationTokenSource{ get; set; }

    public IChangeToken ChangeToken{ get; set; }
}
