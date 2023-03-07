// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalizationLangKeyFilter.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   本地化语言过滤器
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Globalization;

namespace Taf.Core.Web{
    using System;

    /// <summary>
    /// 本地化语言过滤器
    /// </summary>
    public class LocalizationLangKeyFilter:IActionFilter
    {
        private string _langKey;
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _langKey = context.HttpContext.Request.Headers.SingleOrDefault(r => r.Key == "langKey").Value
                              .FirstOrDefault();
            if(string.IsNullOrWhiteSpace(_langKey)) return;
            var cultureInfo = new CultureInfo(_langKey); 
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture   = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
