using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FiltersPractice.Infrastructure
{
    public class ViewDetailsAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ViewResult viewResult)
            {
                var dictionary = new Dictionary<string, string>()
                {
                    ["View-Name"] = viewResult.ViewName,
                    ["Model-Type"] = viewResult.Model.GetType().Name,
                    ["Model-Data"] = viewResult.Model.ToString()
                };

                context.Result = new ViewResult()
                {
                    ViewName = "Message",
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = dictionary
                    }
                };
            }

            await next();
        }
    }
}
