using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;

namespace ContactsManager.API.Extension
{
    public class LogHandlerAttribute : ActionFilterAttribute
    {
        public LogHandlerAttribute()
        {
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _logger = context.HttpContext.RequestServices.GetService(typeof(ILogger)) as ILogger;

            var action = context.RouteData.Values["action"];
            var host = context.HttpContext.Request.Host;
            var requestTime = DateTime.Now;

            _logger.Information($"ActionName: {action}");
            _logger.Information($"Request Time: {requestTime}");
            _logger.Information($"Host Name: {host}");

            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var _logger = context.HttpContext.RequestServices.GetService(typeof(ILogger)) as ILogger;
            var requestCompletedTime = DateTime.Now;

            _logger.Information($"Request completed time: {requestCompletedTime}");
            base.OnActionExecuted(context);
        }
    }
}
