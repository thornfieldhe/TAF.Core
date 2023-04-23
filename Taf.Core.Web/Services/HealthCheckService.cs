// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HealthCheck.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   健康检查
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net;
using System.Net.Sockets;

namespace Taf.Core.Web
{
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
          public static WebApplication UseHealthCheck(this WebApplication application,string serviceName) => UseHealthCheck(application,serviceName, new PathString("/health"));

          /// <summary>
          /// 
          /// </summary>
          /// <param name="webApplication"></param>
          /// <param name="path"></param>
          /// <returns></returns>
          private static WebApplication UseHealthCheck(this WebApplication webApplication,string serviceName, PathString path)
          {
              webApplication.MapGet(path, 
                                         context => {
                                             var host = Dns.GetHostEntry(Dns.GetHostName()).AddressList
                                                           .FirstOrDefault(
                                                                address => address.AddressFamily
                                                                        == AddressFamily.InterNetwork)
                                                          ?.ToString();
                                             context.Response.StatusCode = 200;
                                             context.Response.Headers.Add("Content-Type","text/ plain;charset=utf-8");
                                             return context.Response.WriteAsync($"[{serviceName}]{host}:healthy");
                                         });
              return webApplication;
          }
      }
}
