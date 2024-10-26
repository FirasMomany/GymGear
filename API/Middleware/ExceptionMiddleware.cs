using API.Errors;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware(IHostEnvironment env, RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                await HandleExpectionAsync(context, ex, env);
            }

        }

        private static Task HandleExpectionAsync(HttpContext context, Exception ex, IHostEnvironment env)
        {
            context.Response.ContentType = "Application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment()
                ? new APIErrorResponse(context.Response.StatusCode, ex.Message, ex.StackTrace)
                : new APIErrorResponse(context.Response.StatusCode, ex.Message, "Internal Server Error"); // In Production

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            return context.Response.WriteAsync(json);

        }
     
    }
}
