using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersPractice.Infrastructure
{
    public class ProfileAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch stopwatch = new Stopwatch();
        private long actionTime;
        
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            this.stopwatch.Start();
            await next();

            actionTime = stopwatch.ElapsedMilliseconds;
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await next();
            this.stopwatch.Stop();

            await context.HttpContext.Response.WriteAsync(
                $"<div>Action time: {this.actionTime}.\nResponse time: {this.stopwatch.ElapsedMilliseconds}</div>");
        }
    }
}
