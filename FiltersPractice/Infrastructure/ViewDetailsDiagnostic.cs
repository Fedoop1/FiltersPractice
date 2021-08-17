using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersPractice.Infrastructure
{
    public class ViewDetailsDiagnostic : IAsyncActionFilter
    {
        private readonly IDiagnosticFilter diagnosticFilter;
        public ViewDetailsDiagnostic(IDiagnosticFilter diagnosticFilter) => this.diagnosticFilter = diagnosticFilter;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Result is ViewResult viewResult)
            {
                diagnosticFilter.AddMessage($"View name: {viewResult.ViewName}\nView model type: {viewResult.Model.GetType().Name}\nView model data: {viewResult.Model}");
            }

            await next();
        }
    }
}
