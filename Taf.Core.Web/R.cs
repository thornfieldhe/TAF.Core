using Taf.Core.Extension;

namespace Taf.Core.Web;

/// <summary>
/// 
/// </summary>
/// <param name="success">处理状态：成功、失败</param>
/// <param name="code">需要返回前端编码，前后端协商确定</param>
/// <param name="message">异常消息</param>
/// <param name="traceId">跟踪Id</param>
/// <param name="authority">授权状态</param>
public record R(bool Success = true, int Code = 200, string Message = "", string TraceId = "", int Authority = 4){
    /// <summary>
    /// true:成功;false:失败
    /// </summary>
    public bool Success{ get; } = Success;

    /// <summary>
    /// 逻辑业务消息码,前后端协商确定
    /// </summary>
    public int Code{ get; } = Code;

    /// <summary>
    /// 消息
    /// </summary>
    public string Message{ get; } = Message;

    /// <summary>
    /// 1:拒绝，2：读,:4:写,6读+写,使用与操作
    /// (Authority&2)==2 表示拥有写权限
    /// </summary>
    public int Authority{ get; } = Authority;

    /// <summary>
    /// 错误跟踪Id
    /// </summary>
    public string TraceId{ get; } = TraceId;

    /// <summary>
    /// 产生时间
    /// </summary>
    public DateTime CreationTime => DateTime.UtcNow;
}

public record R<T>(
    bool Success = true, int Code = 200, string Message = "", string TraceId = "", int Authority = 4
  , T    Data    = default);

/// <summary>
/// Http请求返回对象
/// </summary>
public static class HttpObjectResult{
    /// <summary>
    /// 无数据返回
    /// </summary>
    /// <returns></returns>
    public static R Success(string traceId="") => new(TraceId: traceId);
    
    /// <summary>
    /// 客户端错误
    /// </summary>
    /// <returns></returns>
    public static R ShowMessage(string message,string traceId="") => new(Message:message,TraceId: traceId,Code:WebConst.CodeBadRequest);

    /// <summary>
    /// 返回数据
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static R<T> GetResult<T>(T data, string traceId = "") => new(Data: data, TraceId: traceId);

    /// <summary>
    /// 未授权,需要登录
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static R Unauthorized(string errorCode ="", string traceId = "") =>
        new(Success: false, Code: WebConst.CodeUnauthorized, $"Unauthorized error,error code:{errorCode}", TraceId: traceId);

    /// <summary>
    /// 操作拒绝
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static R Forbidden(string errorCode ="", string traceId = "") =>
        new(Success: false, Code: WebConst.CodeForbidden, Message: $"Forbidden error,error code:{errorCode}", TraceId: traceId);

    /// <summary>
    /// 对象不存在
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static R NotFound(string errorCode ="", string traceId = "") =>
        new(Success: false, Code: WebConst.CodeNotFound,  Message: $"Not found error,error code:{errorCode}", TraceId: traceId);

    /// <summary>
    /// 拒绝服务
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static R ServiceUnavailable(string errorCode="", string traceId = "") =>
        new(Success: false, Code: WebConst.CodeServiceUnavailable, Message: $"Denial of service,error code:{errorCode}", TraceId: traceId);

    /// <summary>
    /// 返回服务器异常
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static R InternalServerError(string errorCode, string traceId = "") =>
        new(Success: false, Code: WebConst.CodeInternalServerError, Message: $"Internal server error. error code:{errorCode}", TraceId: traceId);
}
