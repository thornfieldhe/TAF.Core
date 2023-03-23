// 何翔华
// Taf.Core.Web
// RemoteConfigurationChangeToken.cs

using Microsoft.Extensions.Primitives;
using Taf.Core.Utility;

namespace Taf.Core.Web;


/// <summary>
/// 远程配置更新令牌
/// </summary>
public static class RemoteConfigurationChangeToken{
    private static WatchTokenInfo _watchToken = new();
    public static  List<string>   Keys        = new();

    public static IChangeToken Watch(){
        var tokenSource = new CancellationTokenSource();
        var changeToken = new CancellationChangeToken(tokenSource.Token);
        _watchToken = new WatchTokenInfo(){ CancellationTokenSource = tokenSource, ChangeToken = changeToken };
        return _watchToken.ChangeToken;
    }

    public static void OnKeyChanged(string key){
        Fx.If(Keys.Contains(key))
          .Then(() => _watchToken.CancellationTokenSource.Cancel());
    }
}
