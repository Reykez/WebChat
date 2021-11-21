using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using WebChat.Database.Exceptions;

namespace WebChat.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException e)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(e.Message);
            }
            catch(Exception e)
            {
                Console.Write($"unknown error: {e.Message}");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
