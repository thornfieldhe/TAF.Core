// 何翔华
// Taf.Core.Web
// ModelValidFilter.cs

using Microsoft.AspNetCore.Mvc.Filters;
using Taf.Core.Extension;

namespace Taf.Core.Web;

/// <summary>
/// 模型验证异常过滤器
/// </summary>
public class ModelValidFilter:IActionFilter{
    public void OnActionExecuting(ActionExecutingContext context){
        if (!context.ModelState.IsValid){
            throw new ValidationException(
                context.ModelState.SelectMany(s => s.Value.Errors).Select(s => s.ErrorMessage));
        } 
    }

    public void OnActionExecuted(ActionExecutedContext context){
    }
}
