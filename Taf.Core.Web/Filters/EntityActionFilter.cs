// 何翔华
// Taf.Core.Net.Utility
// EntityActionFilter.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Taf.Core.Extension;
using Taf.Core.Utility;

namespace Taf.Core.Web;

/// <summary>
/// http请求结果包装过滤器
/// </summary>
public class EntityActionFilter : IActionFilter{
    private readonly ILogger<EntityActionFilter> _logger;
    public EntityActionFilter(ILogger<EntityActionFilter> logger) => _logger = logger;

    public void OnActionExecuting(ActionExecutingContext context){
    }

    public void OnActionExecuted(ActionExecutedContext context){
        if (!context.HttpContext.Request.Headers.TryGetValue("traceId",out var traceId)){
            traceId = Randoms.GetRandomCode(6,"0123456789abcdefghijklmnopqrstuvwxyz");
        }
        if(context.Result is JsonResult json){
            context.Result = new JsonResult(new R(Data: json.Value,TraceId:traceId));
        } else if(context.Result is ObjectResult obj){
            context.Result = new JsonResult(new R(Data:obj.Value,TraceId:traceId));
        } else if(context.Result is EmptyResult emputy){
            context.Result = new JsonResult(new R());
        } else if(context.Result is ContentResult content){
            context.Result = new JsonResult(new R<string>(Data: content.Content,TraceId:traceId));
        }  else if(context.Result is NoContentResult noContent){
            context.Result = new JsonResult(new R(TraceId:traceId));
        }
        //todos: 后续补充  
    }
}
