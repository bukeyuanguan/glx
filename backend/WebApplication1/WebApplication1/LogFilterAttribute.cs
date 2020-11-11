using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class LogFilterAttribute: ActionFilterAttribute
    {
        private ILogger _logger;
        public LogFilterAttribute(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("Logger3");
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"Request Path {context.HttpContext.Request.Path}");

            var executed = await next();

            if (executed.Exception == null)
            {
                _logger.LogInformation($"Return {context.HttpContext.Response.StatusCode}");
            }
            else
            {
                _logger.LogError(executed.Exception, null);
            }
        }
    }
}
