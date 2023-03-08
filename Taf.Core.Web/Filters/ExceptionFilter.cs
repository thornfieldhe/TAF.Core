// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionFilter.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   异常过滤器
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Taf.Core.Extension;
using Taf.Core.Utility;
using Taf.Core.Web;

namespace Taf.Core.Web;

using System;

/// <summary>
/// 
/// </summary>
public class ExceptionFilter : IExceptionFilter{
    private readonly ILogger    _logger;
    private readonly ILoginInfo _loginInfo;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="remoteEventPublisher"></param>
    public ExceptionFilter(ILogger logger, ILoginInfo loginInfo){
        _logger    = logger;
        _loginInfo = loginInfo;
    }

    /// <summary>针对用户自定义异常进行封装</summary>
    /// <param name="context"></param>
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnException(ExceptionContext context){
        ExceptionAction(context, context.Exception);
        context.ExceptionHandled = true;
    }


    private void ExceptionAction(ExceptionContext context, Exception exception){
        var result    = new R();
        var message   = string.Empty;
        var errorCode = $"ERR_{Randoms.GetRandomCode(6, "1234567890")}";
        switch(exception){
            case UserFriendlyException userException:
                result = HttpObjectResult.ShowMessage(userException.Message, _loginInfo.TraceId);
                break;
            case AggregateException{ InnerException:{ } } aggregateException:
                ExceptionAction(context, aggregateException.InnerException);
                break;
            case UnauthorizedAccessException unauthorizedAccessException:
                message = $"{new string('-', 30)}\n[系统异常]:{errorCode},{unauthorizedAccessException.Message}";
                result  = HttpObjectResult.Unauthorized(errorCode, _loginInfo.TraceId);
                _logger.LogError(message, unauthorizedAccessException);
                break;
            case BussinessException bussinessException:
                message =
                    $"{new string('-', 30)}\n[系统异常]:{errorCode},{bussinessException.Message},Path:{bussinessException.ErrorCode}]";
                result = HttpObjectResult.Unauthorized(errorCode, _loginInfo.TraceId);
                _logger.LogError(message, bussinessException);
                break;
            case ArgumentNullException nullException:
                message =
                    $"{new string('-', 30)}\n[系统异常]:{errorCode},{nullException.Message},参数不能为空错误:方法名:{nullException.TargetSite.Name},参数:{nullException.ParamName},来源:{nullException.Source}";
                result = HttpObjectResult.Unauthorized(errorCode, _loginInfo.TraceId);
                _logger.LogError(message, nullException);
                break;
            case EntityNotFoundException entityNotFoundException:
                message =
                    $"{new string('-', 30)}\n[系统异常]:{errorCode},{entityNotFoundException.Message},对象类型:{entityNotFoundException.Type.Name},Path:{entityNotFoundException.ErrorCode}";
                result = HttpObjectResult.Unauthorized(errorCode, _loginInfo.TraceId);
                _logger.LogError(message, entityNotFoundException);
                break;
            case ValidationException validationException:
                var files = string.Join(',', validationException.ValidationErrors);
                message =
                    $"{new string('-', 30)}\n[系统异常]:{errorCode},参数验证未通过:{files},来源:{validationException.Source}";
                result = HttpObjectResult.Unauthorized(errorCode, _loginInfo.TraceId);
                _logger.LogError(message, validationException);
                break;
            case NullReferenceException referenceException:
                context.HttpContext.Response.StatusCode = 500;
                message =
                    $"{new string('-', 30)}\n[系统异常]:{errorCode},{referenceException.Message},对象不能为空错误";
                result = GetInternalServerError(message, errorCode, exception);
                break;
            case InvalidOperationException invalidOperationException:
                context.HttpContext.Response.StatusCode = 500;
                message =
                    $"{new string('-', 30)}\n[系统异常]:{errorCode},{invalidOperationException.Message},无效操作";
                result = GetInternalServerError(message, errorCode, exception);
                break;
            case IndexOutOfRangeException invalidOperationException:
                context.HttpContext.Response.StatusCode = 500;
                message =
                    $"{new string('-', 30)}\n[系统异常]:{errorCode},{invalidOperationException.Message},数组越界";
                result = GetInternalServerError(message, errorCode, exception);
                break;
            case TargetInvocationException targetInvocationException:
                var innerException = targetInvocationException.InnerException ?? targetInvocationException;
                ExceptionAction(context, innerException);

                break;
            case ServiceUnavailableException serviceUnavailableException:
                message =
                    $"{new string('-', 30)}\n[系统异常]:{errorCode},{serviceUnavailableException.Message},url:{serviceUnavailableException.Url}";
                result = GetInternalServerError(message, errorCode, exception);
                break;
            default:
                message =
                    $"{new string('-', 30)}\n[系统异常]:{errorCode}, 异常类型:{exception.GetType().Name},{exception.Message}";
                result = GetInternalServerError(message, errorCode, exception);
                break;
        }

        context.HttpContext.Response.StatusCode = result.Code;
        context.Result                          = new JsonResult(result);
    }

    private R GetInternalServerError(string message, string errorCode, Exception exception){
        _logger.LogError(message, exception);
        return HttpObjectResult.InternalServerError(errorCode, _loginInfo.TraceId);
    }
}
