using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Zoo.BLL.Exceptions;

namespace Zoo.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate) 
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context,ex);
            }
        }

        public async Task HandleException(HttpContext context, Exception ex) 
        {
            
            context.Response.ContentType="application/json";
            int statusCode = 400;
            switch(ex) 
            {
                // Special exceptions to implement a unique behaviour.
                case LoginException e:
                    await SendResponse(context, e);
                    break;
                case RegisterException e:
                    await SendResponse(context,e);
                    break;
                case NotFoundException e:
                    await SendResponse(context,e);
                    break;
                case Exception:
                    statusCode=500;
                    context.Response.StatusCode = statusCode;
                    var response = new
                    {
                        message = ex.Message,
                    };
                    var jsonResponse = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsync(jsonResponse);
                    break;

                default:
                    throw new Exception("Impossibility");
            }
        }

        public async Task SendResponse(HttpContext context,ZooException e) 
        {
            context.Response.StatusCode=e.StatusCode;
            var response = new
            {
                message = e.Content,
            };
            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
