using System.Net.Http;
using System;
using System.Net;
using System.Threading.Tasks;
// using DeviceManager.Api.Model;
// using FluentValidation;
using Microsoft.AspNetCore.Http;
using Tss.Core.Constants;
using Newtonsoft.Json;
using Tss.Core.Exceptions;
using Tss.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var instance = Guid.NewGuid().ToString();
            
            var response = exception.Data["Response"] as Response;
            
            if (response == null) 
            {
                response = new Response
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = HttpMessage.InternalServerErrorTitle,
                    Detail = exception.Message,
                };
            }

            context.Response.StatusCode = (int)response.Status;
            context.Response.ContentType = HttpContentType.ProblemJson;

            response.Type = $"https://httpstatuses.com/{response.Status}";
            response.Title += $" Error Instance ID: {instance}";
            response.Instance = instance ;

            _logger.LogError(exception, exception.Message);

            return WriteAsync(context, response);
        }

        private Task WriteAsync(HttpContext context, Response response)
        {
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }


    }
}
