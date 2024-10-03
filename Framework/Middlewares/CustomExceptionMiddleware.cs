using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace RealEstateListingApi.Framework.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
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
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var problemDetails = new
            {
                title = "Validation Error",
                status = context.Response.StatusCode,
                errors = new List<object>()
            };

            if (exception is ValidationException validationException)
            {
                var errors = validationException.ValidationResult.MemberNames
                    .Select(e => new
                    {
                        field = e,
                        message = validationException.ValidationResult.ErrorMessage
                    })
                    .ToList<object>();

                problemDetails = new
                {
                    title = "Validation Error",
                    status = context.Response.StatusCode,
                    errors = errors
                };
            }

            return context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
        }
    }

}