using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Auth.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly ILogger<AuthorizationMiddleware> _logger;
        private readonly RequestDelegate _next;
        public AuthorizationMiddleware(RequestDelegate next, ILogger<AuthorizationMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.GetEndpoint().Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
            { 
                await _next(httpContext).ConfigureAwait(false);
            }
            else if (!httpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await httpContext.Response.WriteAsJsonAsync("Authorization Header missing");
            }
            else
            { 
                //TODO implement Token verification and authorization compare here
                await _next(httpContext).ConfigureAwait(false);
            }
        }
    }
}
