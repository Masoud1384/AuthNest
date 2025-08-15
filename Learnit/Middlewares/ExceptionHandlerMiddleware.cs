using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace AuthNest.Middlewares
{
    public static class ExceptionHandlerExtention
    {
        public static IApplicationBuilder UseAuthNestExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // this is supposed to be exception handler but right now i've got no logic for handling :)
                await _next(context);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";


                // in production env we are not supposed to log the error this way
                await context.Response.WriteAsync(JsonConvert.SerializeObject(
                    new
                    {
                        Message = "UnexpectedErrorOccurred",
                        Error = e.Message
                    }));
            }
        }
    }
}
