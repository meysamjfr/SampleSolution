using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Project.Exceptions;
using Project.Models;

namespace Project.Extentions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
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
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var res = new Response<string>(ResponseStatus.Failed);

            switch (exception)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    res = new Response<string>(ResponseStatus.BadRequest);
                    res.Errors.Add(badRequestException.Message);
                    break;

                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    res = new Response<string>(ResponseStatus.BadRequest);
                    res.Errors.Add(validationException.Message);
                    res.Errors = validationException.Errors;
                    break;

                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    res = new Response<string>(ResponseStatus.NotFound);
                    res.Errors.Add(notFoundException.Message);
                    break;

                case UnauthorizedAccessException unauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    res = new Response<string>(ResponseStatus.Unauthorized);
                    res.Errors.Add(unauthorizedAccessException.Message);
                    break;

                default:
                    statusCode = HttpStatusCode.BadRequest;
                    res = new Response<string>(ResponseStatus.BadRequest);
                    res.Errors.Add(exception.Message);
                    break;
            }

            if (exception.InnerException != null)
            {
                res.Errors.Add(exception.InnerException.Message);
            }

            string result = JsonConvert.SerializeObject(res);

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}