using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersPractice.Infrastructure
{
    public class DiagnosticFilter : IAsyncResultFilter
    {
        private readonly IDiagnosticFilter diagnosticFilter;

        public DiagnosticFilter(IDiagnosticFilter filter) => this.diagnosticFilter =
            filter ?? throw new ArgumentNullException(nameof(filter), "Filter is null");

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await next();

            foreach (var message in diagnosticFilter.Messages)
            {
                await context.HttpContext.Response.WriteAsync($"<div>{message}</div>");
            }
        }
    }
}
