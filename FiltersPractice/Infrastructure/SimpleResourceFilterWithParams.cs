using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersPractice.Infrastructure
{
    public class SimpleResourceFilterWithParams : Attribute, IAsyncResourceFilter
    {
        private readonly int id;
        private readonly string token;
        public SimpleResourceFilterWithParams(int id, string token) => (this.id, this.token) = (id, token);
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            context.HttpContext.Response.Headers.Add("id", id.ToString());
            context.HttpContext.Response.Headers.Add("token", token);
            await next();
        }
    }
}
