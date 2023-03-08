﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogingBuilderExr.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Serilog;
using System.Collections.Generic;

// 何翔华
// Taf.Core.Web
// LogingBuilderExr.cs

namespace Taf.Core.Web.Loging;

using System;

/// <summary>
/// $Summary$
/// </summary>
public class LogingBuilderExt{
   
    /// <summary>
    /// 添加日志配置,需要在SqlSugar之前注入
    /// </summary>
    /// <param name="host"></param>
    public static void UseLogger(IHostBuilder host){
        const string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
        Log.Logger = new LoggerConfiguration()
                 #if DEBUG
                    .MinimumLevel.Debug()
                 #else
                    .MinimumLevel.Information()
                 #endif
                    .Enrich.FromLogContext()
                    .WriteTo.Console(outputTemplate: outputTemplate)
                 #if !DEBUG
                    .WriteTo.File("logs/app.log"
                            , rollingInterval: RollingInterval.Hour
                            , outputTemplate: outputTemplate)
                 #endif
                    .CreateLogger();
        host.UseSerilog(Log.Logger);
    } 
}