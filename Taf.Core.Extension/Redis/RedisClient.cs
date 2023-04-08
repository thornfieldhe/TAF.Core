// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedisClient.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using FreeRedis;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Taf.Core.Utility;
using Microsoft.Extensions.DependencyInjection;

// 何翔华
// Taf.Core.Extension
// RedisClient.cs

namespace Taf.Core.Extension;

using System;

/// <summary>
/// redis客户端
/// </summary>
public class RedisClientServer:SingletonBase<RedisClientServer>{
    private string      _connectionString;
    public  RedisClient Client{ get;private set; }

    public void LoadConfig(string? connection){
        Fx.If(connection == null)
          .Then(() => {
               var config = ServiceLocator.Instance.ServiceProvider.GetService<IConfiguration>();
               connection = config["Redis:ConnectionString"];
           
           });
        Client        =  new RedisClient(connection);
        Client.Notice += (s, e) => Console.WriteLine(e.Log); //打印命令日志
    }
        
    /// <summary>
    ///     缓存过期时间
    /// </summary>
    public int Expiry =>43200;
    
}

