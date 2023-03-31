namespace Taf.Core.Web;

/// <summary>
/// 
/// </summary>
/// <param name="Success">处理状态：成功、失败</param>
/// <param name="Code">需要返回前端编码，前后端协商确定</param>
/// <param name="Message">异常消息</param>
/// <param name="TraceId">跟踪Id</param>
/// <param name="Authority">授权状态</param>
public record R(int Code = 200, string Message = "", string TraceId = "", int Authority = 4, object? Data = default){
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
    public int Authority{ get; set; } = Authority;

    /// <summary>
    /// 错误跟踪Id
    /// </summary>
    public string TraceId{ get; set; } = TraceId;

    /// <summary>
    /// 产生时间
    /// </summary>
    public DateTime CreationTime => DateTime.UtcNow;
}

/// <summary>
/// 
/// </summary>
/// <param name="Success"></param>
/// <param name="Code"></param>
/// <param name="Message"></param>
/// <param name="TraceId"></param>
/// <param name="Authority"></param>
/// <param name="Data"></param>
/// <typeparam name="T"></typeparam>
public record R<T>(int Code = 200, string Message = "", string TraceId = "", int Authority = 4, T? Data = default);

/// <summary>
/// Http请求返回对象
/// </summary>
public static class HttpObjectResult{
    /// <summary>
    /// 无数据返回
    /// </summary>
    /// <returns></returns>
    public static R Success(string traceId = "") => new(TraceId: traceId);

    /// <summary>
    /// 客户端错误
    /// </summary>
    /// <returns></returns>
    public static R ShowMessage(string message, int code =400, string traceId = "") =>
        new(Message: message, TraceId: traceId, Code: code);

    /// <summary>
    /// 返回数据
    /// </summary>
    /// <param name="data"></param>
    /// <param name="traceId"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static R<T> GetResult<T>(T data, string traceId = "") => new(Data: data, TraceId: traceId);

    /// <summary>
    /// 异常消息
    /// </summary>
    /// <param name="errorCode"></param>
    /// <param name="message"></param>
    /// <param name="traceId"></param>
    /// <returns></returns>
    public static R ShowErr(string message,string errorCode = "", string traceId = "") =>
        new(Code: WebConst.CodeUnauthorized,Message:message, TraceId: traceId);
}
