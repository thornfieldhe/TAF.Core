// 何翔华
// Taf.Core.Net.Utility
// EntityActionFilter.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

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
        if(context.Result is JsonResult json){
            context.Result = new JsonResult(new R(Data: json.Value));
        } else if(context.Result is ObjectResult obj){
            context.Result = new JsonResult(new R(Data:obj.Value));
        } else if(context.Result is EmptyResult emputy){
            context.Result = new JsonResult(new R());
        } else if(context.Result is ContentResult content){
            context.Result = new JsonResult(new R<string>(Data: content.Content));
        }  else if(context.Result is NoContentResult noContent){
            context.Result = new JsonResult(new R());
        }else{
              //todos: 后续补充  
        }
    }
}
