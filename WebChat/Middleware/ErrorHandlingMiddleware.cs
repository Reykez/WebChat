﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            catch(Exception e)
            {
                Console.Write($"uknown error: {e.Message}");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
            throw new NotImplementedException();
        }
    }
}
