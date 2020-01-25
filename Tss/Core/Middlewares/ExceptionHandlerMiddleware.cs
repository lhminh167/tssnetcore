using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tss.Core.Constants;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Tss.Core.Models;

namespace Tss.Core.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger
        )
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Response response = exception.Data[nameof(Response)] as Response ?? 
                new Response
                (
                    HttpStatusCode.InternalServerError,
                    HttpMessage.InternalServerErrorTitle,
                    exception.Message
                );

            context.Response.StatusCode = (int)response.Status;
            context.Response.ContentType = HttpContentType.ProblemJson;

            _logger.LogError(exception, exception.Message);

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
