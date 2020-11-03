using System;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZPastel.Service.Exceptions;

namespace ZPastel.API.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger logger;
        private readonly IHostEnvironment hostEnvironment;

        public ExceptionFilter(ILogger<ExceptionFilter> logger, IHostEnvironment hostEnvironment)
        {
            this.logger = logger;
            this.hostEnvironment = hostEnvironment;
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is NotFoundException notFoundException)
            {
                HandleException(context, HttpStatusCode.NotFound, notFoundException.Message);
            }
            else if (exception is ArgumentException argumentException)
            {
                HandleException(context, HttpStatusCode.BadRequest, argumentException.Message);
            }
            else
            {
                var message = hostEnvironment.IsProduction() ? "Unhandled Exception" : $"Unhandled Exception: {exception}";

                HandleException(context, HttpStatusCode.InternalServerError, message);
            }
        }

        private void HandleException(ExceptionContext context, HttpStatusCode statusCode, string message)
        {
            context.ExceptionHandled = true;

            var ex = context.Exception;
            logger.LogError(ex, message);

            var response = new
            {
                message,
                stackTrace = !hostEnvironment.IsProduction() ? ex.StackTrace : null
            };

            context.Result = new JsonResult(response, new JsonSerializerOptions { IgnoreNullValues = true })
            {
                StatusCode = (int)statusCode
            };
        }
    }
}
