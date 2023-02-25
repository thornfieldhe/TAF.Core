// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="ExceptionFilter.cs" company="" author="何翔华">
// //   
// // </copyright>
// // <summary>
// //   异常过滤器
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------
//
// using System.Linq;
// using AutoMapper;
// using Castle.Core.Logging;
// using MassTransit;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;
// using System.Reflection;
// using System.Runtime.ExceptionServices;
// using Taf.Core.Extension;
// using Taf.Core.Web;
// using UAParser;
//
// namespace eFootprintV3.Web.Startup{
//     using System;
//
//     /// <summary>
//     /// 
//     /// </summary>
//     public class PlusExceptionFilter : IExceptionFilter{
//         private readonly IRemoteEventPublisher _remoteEventPublisher;
//         private readonly IMailSender           _mailSender;
//         private readonly ILogger               _logger;
//         private          string                _traceId;
//         /// <summary>
//         /// 
//         /// </summary>
//         /// <param name="remoteEventPublisher"></param>
//         public PlusExceptionFilter(IRemoteEventPublisher remoteEventPublisher, IMailSender mailSender, ILogger logger){
//             _remoteEventPublisher = remoteEventPublisher;
//             _mailSender           = mailSender;
//             _logger               =logger;
//
//         }
//
//         /// <summary>针对用户自定义异常进行封装</summary>
//         /// <param name="context"></param>
//         /// <summary>
//         /// 
//         /// </summary>
//         /// <param name="context"></param>
//         /// <exception cref="NotImplementedException"></exception>
//         public void OnException(ExceptionContext context){
//             ExceptionAction(context, context.Exception);
//             context.ExceptionHandled = true;
//         }
//
//
//  
//
//         private void ExceptionAction(ExceptionContext context, Exception exception){
//             _traceId = context.HttpContext.Request.Headers.SingleOrDefault(r => r.Key == "traceId").Value
//                               .FirstOrDefault();
//             var result  = new R();
//             var message = string.Empty;
//             switch(exception){
//                 case UserFriendlyException userException:
//                     result         = HttpObjectResult.ShowMessage(userException.Message, _traceId);
//                     break;
//                 case AggregateException{ InnerException:{ } } aggregateException:
//                     ExceptionAction(context, aggregateException.InnerException);
//                     break;
//                 case UnauthorizedAccessException unauthorizedAccessException:
//                     result         = HttpObjectResult.Unauthorized(unauthorizedAccessException.Message, _traceId);
//                     _logger.Error(message, unauthorizedAccessException);
//                     _mailSender.SendEmail(unauthorizedAccessException, context.HttpContext.Request, context.HttpContext.Response.StatusCode,message);
//                     break;
//                 case ArgumentNullException nullException:
//                     message =
//                         $"{nullException.Message},参数不能为空错误:方法名:{nullException.TargetSite.Name},参数:{nullException.ParamName},来源:{nullException.Source}";
//                      result = GetInternalServerError("服务器错误", message);
//                    _logger.Error(message, nullException);
//                     _mailSender.SendEmail(context.Exception, context.HttpContext.Request, context.HttpContext.Response.StatusCode, message);
//                     break;
//                 case NullReferenceException referenceException:
//                     context.HttpContext.Response.StatusCode = 500;
//                     message =
//                         $"{referenceException.Message},对象不能为空错误";
//                     result = HttpObjectResult.InternalServerError(message, _traceId);
//                    _logger.Error(message, referenceException);
//                     _mailSender.SendEmail(context.Exception, context.HttpContext.Request, context.HttpContext.Response.StatusCode, message);
//                     break;
//                 case InvalidOperationException invalidOperationException:
//                     context.HttpContext.Response.StatusCode = 500;
//                     message =
//                         $"{invalidOperationException.Message},无效操作: {errorCode}";
//                     context.Result = (IActionResult)new JsonResult((object)new R(false, "500",
//                                                                                      $"系统错误,{errorCode}",
//                                                                                      _traceId, 1));
//                    _logger.Error(message, invalidOperationException);
//                     _mailSender.SendEmail(context.Exception, context.HttpContext.Request, context.HttpContext.Response.StatusCode, message);
//                     break;
//                 case IndexOutOfRangeException invalidOperationException:
//                     context.HttpContext.Response.StatusCode = 500;
//                     message =
//                         $"{invalidOperationException.Message},数组越界: {errorCode}";
//                     context.Result = (IActionResult)new JsonResult((object)new R(false, "500",
//                                                                                      $"系统错误,{errorCode}",
//                                                                                      _traceId, 1));
//                    _logger.Error(message, invalidOperationException);
//                     _mailSender.SendEmail(context.Exception, context.HttpContext.Request, context.HttpContext.Response.StatusCode, message);
//                     break;
//                 case TargetInvocationException targetInvocationException:
//                     var innerException = targetInvocationException.InnerException ?? targetInvocationException;
//                     if(innerException != null){
//                         ExceptionAction(context, innerException);
//                     } else{
//                         context.HttpContext.Response.StatusCode = 500;
//                         message =
//                             $"{targetInvocationException.Message}, 目标d: {errorCode}";
//                         context.Result = (IActionResult)new JsonResult((object)new R(false, "500",
//                                                                                          $"系统错误,{errorCode}",
//                                                                                          _traceId, 1));
//                         _logger.Error(message, targetInvocationException);
//                         _mailSender.SendEmail(context.Exception, context.HttpContext.Request, context.HttpContext.Response.StatusCode, message);
//                     }
//         
//                     break;
//                 default:
//                     context.HttpContext.Response.StatusCode = 500;
//                     message                                 = $"{exception.Message} {errorCode}";
//                     context.Result                          = (IActionResult)new JsonResult((object)new R(false, "500", $"系统错误,{errorCode}", _traceId, 1));
//                    _logger.Error(message, exception);
//                     _mailSender.SendEmail(context.Exception, context.HttpContext.Request, context.HttpContext.Response.StatusCode, message);
//                     break;
//             }
//
//             context.HttpContext.Response.StatusCode = result.Code;
//             context.Result                          = new JsonResult(result);
//             void FriendExceptionAction(UserFriendlyException userException){
//                 context.Result = new JsonResult(HttpObjectResult.ShowMessage(userException.Message));
//             }
//         }
//
//         private R GetInternalServerError( string message, string details){
//         #if DEBUG
//             return HttpObjectResult.InternalServerError(details, _traceId);
//         #else
//             return HttpObjectResult.InternalServerError(message, _traceId);
//         #endif
//         }
//
//         /// <summary>
//         /// 执行其他异常操作
//         /// </summary>
//         public Func<ExceptionContext, bool> ExcuteOtherException{ get; set; }
//     }
// }
