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
using System.Reflection;
using Taf.Core.Utility;

namespace Taf.Core.Web;

/// <summary>
/// 
/// </summary>
public class ExceptionFilter : IExceptionFilter{
    private readonly ILogger<ExceptionFilter> _logger;
    private readonly ILoginService               _loginService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="remoteEventPublisher"></param>
    public ExceptionFilter(ILogger<ExceptionFilter> logger, ILoginService loginService){
        _logger    = logger;
        _loginService = loginService;
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
        var errorCode = $"ERR_{Randoms.GetRandomCode(6, "0123456789abcdefghijklmnopqrstuvwxyz")}";
        switch(exception){
            case UserFriendlyException userException:
                result = HttpObjectResult.ShowMessage(userException.Message, userException.Code, _loginService.TraceId);
                context.HttpContext.Response.StatusCode = WebConst.CodeBadRequest;
                break;
            case AggregateException{ InnerException:{ } } aggregateException:
                ExceptionAction(context, aggregateException.InnerException);
                break;
            case UnauthorizedAccessException unauthorizedAccessException:
                message = $"{new string('-', 30)}\n[授权异常]:{errorCode},{unauthorizedAccessException.Message}";
                result = GetInternalServerError(message, errorCode, unauthorizedAccessException);
                context.HttpContext.Response.StatusCode = WebConst.CodeUnauthorized;
                break;
            case BussinessException bussinessException:
                message =
                    $"{new string('-', 30)}\n[业务异常]:{errorCode},{bussinessException.Message},Path:{bussinessException.ErrorCode}]";
                result = GetInternalServerError(message, errorCode, bussinessException);
                context.HttpContext.Response.StatusCode = WebConst.CodeInternalServerError;
                break;
            case ArgumentNullException nullException:
                message =
                    $"{new string('-', 30)}\n[系统异常]:{errorCode},{nullException.Message},参数不能为空错误:方法名:{nullException.TargetSite.Name},参数:{nullException.ParamName},来源:{nullException.Source}";
                result = HttpObjectResult.ShowErr(nullException.Message, errorCode, _loginService.TraceId);
                GetInternalServerError(message, errorCode, nullException);
                context.HttpContext.Response.StatusCode = WebConst.CodeInternalServerError;
                break;
            case EntityNotFoundException entityNotFoundException:
                message =
                    $"{new string('-', 30)}\n[数据异常]:{errorCode},{entityNotFoundException.Message},对象类型:{entityNotFoundException.Type.Name},Path:{entityNotFoundException.ErrorCode}";
                result = GetInternalServerError(message, errorCode, entityNotFoundException);
                context.HttpContext.Response.StatusCode = WebConst.CodeNotFound;
                break;
            case ValidationException validationException:
                var attributes = string.Join(',', validationException.ValidationErrors);
                message =
                    $"{new string('-', 30)}\n[参数异常]:{errorCode},参数验证未通过:{attributes},来源:{validationException.Source}";
                result = HttpObjectResult.ShowMessage(attributes, WebConst.CodeBadRequest
                                                    , _loginService.TraceId);
                context.HttpContext.Response.StatusCode = WebConst.CodeBadRequest;
                break;
            case NullReferenceException referenceException:
                message =
                    $"{new string('-', 30)}\n[系统异常]:{errorCode},{referenceException.Message},对象不能为空错误";
                result = GetInternalServerError(message, errorCode, referenceException);
                context.HttpContext.Response.StatusCode = WebConst.CodeInternalServerError;
                break;
            case InvalidOperationException invalidOperationException:
                message =
                    $"{new string('-', 30)}\n[系统异常]:{errorCode},{invalidOperationException.Message},无效操作";
                result = GetInternalServerError(message, errorCode, invalidOperationException);
                context.HttpContext.Response.StatusCode = WebConst.CodeInternalServerError;
                break;
            case IndexOutOfRangeException invalidOperationException:
                context.HttpContext.Response.StatusCode = 500;
                message =
                    $"{new string('-', 30)}\n[系统异常]:{errorCode},{invalidOperationException.Message},数组越界";
                result = GetInternalServerError(message, errorCode, invalidOperationException);
                context.HttpContext.Response.StatusCode = WebConst.CodeInternalServerError;
                break;
            case TargetInvocationException targetInvocationException:
                var innerException = targetInvocationException.InnerException ?? targetInvocationException;
                ExceptionAction(context, innerException);
                break;
            case ServiceUnavailableException serviceUnavailableException:
                message =
                    $"{new string('-', 30)}\n[远程服务异常]:{errorCode},{serviceUnavailableException.Message},url:{serviceUnavailableException.Url}";
                result = GetInternalServerError(message, errorCode, serviceUnavailableException);
                context.HttpContext.Response.StatusCode = WebConst.CodeServiceUnavailable;
                break;
            default:
                message =
                    $"{new string('-', 30)}\n[系统异常]:{errorCode}, 异常类型:{exception.GetType().Name},{exception.Message}";
                result                                  = GetInternalServerError(message, errorCode, exception);
                context.HttpContext.Response.StatusCode = WebConst.CodeServiceUnavailable;
                break;
        }

        context.Result = new JsonResult(result);
    }

    private R GetInternalServerError(string message, string errorCode, Exception exception){
        _logger.LogError(message, exception);
        return HttpObjectResult.ShowErr(exception.Message, errorCode, _loginService.TraceId);
    }
}
