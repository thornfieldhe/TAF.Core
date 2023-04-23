// 何翔华
// Taf.Core.Web
// RedisExtend.cs

namespace Taf.Core.Web;

public static class RedisExtend{
    public static void AddRedis(this WebApplicationBuilder builder){
        RedisClientServer.Instance.LoadConfig(builder.Configuration["Redis"]);
    }
}
