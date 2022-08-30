using ExceptionHandlerSimple.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace ExceptionHandlerSimple.Middleware
{
    public class ExceptionHandlerSimpleMiddleware
    {
        private RequestDelegate _next;

        public ExceptionHandlerSimpleMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                IServiceProvider serviceProvider = context.RequestServices;
                ILoggerHandler Logger = serviceProvider.GetRequiredService<ILoggerHandler>();
                Logger.LogError($"{ex.Message}\r\n{ ex.StackTrace}");
            }
        }
    }
}
