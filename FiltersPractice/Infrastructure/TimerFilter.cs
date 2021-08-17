using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersPractice.Infrastructure
{
    public class TimerFilter : IAsyncActionFilter, IAsyncResultFilter
    {
        private readonly ConcurrentQueue<double> actionTimes = new ConcurrentQueue<double>();
        private readonly ConcurrentQueue<double> resultTimes = new ConcurrentQueue<double>();
        private readonly double resultTime;
        private readonly IDiagnosticFilter diagnosticFilter;
        public TimerFilter(IDiagnosticFilter diagnosticFilter) => this.diagnosticFilter = diagnosticFilter;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();
            await next();
            stopwatch.Stop();
            actionTimes.Enqueue(stopwatch.Elapsed.TotalMilliseconds);
            this.diagnosticFilter.AddMessage($"Action time: {stopwatch.Elapsed.TotalMilliseconds}. Average: {actionTimes.Average():F2}");
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();
            await next();
            stopwatch.Stop();
            resultTimes.Enqueue(stopwatch.Elapsed.TotalMilliseconds);
            this.diagnosticFilter.AddMessage($"Result time: {stopwatch.Elapsed.TotalMilliseconds}. Average: {resultTimes.Average():F2}");
        }
    }
}
