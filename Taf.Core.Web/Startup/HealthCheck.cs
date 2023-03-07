// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HealthCheck.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   健康检查
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Taf.Core.Web
{
    using System;

    /// <summary>
    /// 健康检查
    /// </summary>
     public static class HealthCheck
      {
          /// <summary>
          /// 
          /// </summary>
          /// <param name="application"></param>
          /// <returns></returns>
          public static WebApplication UseHealthCheck(this WebApplication application) => UseHealthCheck(application, new PathString("/health"));

          /// <summary>
          /// 
          /// </summary>
          /// <param name="webApplication"></param>
          /// <param name="path"></param>
          /// <returns></returns>
          private static WebApplication UseHealthCheck(this WebApplication webApplication, PathString path)
          {
              webApplication.MapGet(path, 
                                         (context) => {
                                             var host = System
                                                       .Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList
                                                       .FirstOrDefault(
                                                            address => address.AddressFamily
                                                                    == System.Net.Sockets.AddressFamily.InterNetwork)
                                                      ?.ToString();
                                             context.Response.StatusCode = 200;
                                             return context.Response.WriteAsync($"{host}:healthy");
                                         });
              return webApplication;
          }
      }
}
