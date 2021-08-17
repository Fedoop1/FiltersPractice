using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersPractice.Infrastructure
{
    public class MessageAttribute : ResultFilterAttribute
    {
        private readonly string message;
        public MessageAttribute(string message) => this.message = message;

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            WriteMessage(context, this.message);
            await next();
        }

        private static async void WriteMessage(ResultExecutingContext context, string message) =>
            await context.HttpContext.Response.WriteAsync($"<div>{message}</div>");
    }
}
