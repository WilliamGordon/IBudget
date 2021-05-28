using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ibudget.middleware
{
    public class LogRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public LogRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            using var buffer = new MemoryStream();
            var request = httpContext.Request;
            var response = httpContext.Response;
            var stream = response.Body;
            response.Body = buffer;
            await _next(httpContext);
            Debug.WriteLine($"Request content type:  {httpContext.Request.Headers["Accept"]} {Environment.NewLine} " +
                            $"Request path: {request.Path} {Environment.NewLine} " +
                            $"Response type: {response.ContentType} {Environment.NewLine} " +
                            $"Response length: {response.ContentLength ?? buffer.Length}");
            buffer.Position = 0;
            await buffer.CopyToAsync(stream);
        }
    }

    public static class LogRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogRequestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogRequestMiddleware>();
        }
    }
}
