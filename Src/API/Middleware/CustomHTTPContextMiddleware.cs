using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using API.Utils;

namespace API.Middleware
{
    public class CustomHTTPContextMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomHTTPContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            httpContext.Items.Add("RequestUUID", Guid.NewGuid().ToString());
            httpContext.Items.Add("token", httpContext.Request.Headers.ContainsKey(ApiHeaders.AUTHORIZATION) ? httpContext.Request.Headers[ApiHeaders.AUTHORIZATION].ToString() : "");
            return _next(httpContext);
        }
    }

    public static class CustomHTTPContextMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomHTTPContextMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomHTTPContextMiddleware>();
        }
    }
}
