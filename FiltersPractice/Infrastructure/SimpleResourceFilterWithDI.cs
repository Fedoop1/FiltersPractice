using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FiltersPractice.Infrastructure
{
    public class SimpleResourceFilterWithDI : Attribute, IAsyncResourceFilter
    {
        private readonly ILogger logger;
        public SimpleResourceFilterWithDI(ILogger<SimpleResourceFilterWithDI> logger) => this.logger = logger;

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            this.logger.LogInformation($"{DateTime.Now} | Resource execution start");
            await next();
            this.logger.LogInformation($"{DateTime.Now} | Resource execution end");
        }
    }
}
