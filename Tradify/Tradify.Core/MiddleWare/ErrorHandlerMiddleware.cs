using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using Tradify.Core.Bases;
using Tradify.Core.Behaviors;

namespace Tradify.Core.MiddleWare
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate requestDelegate;

        public ErrorHandlerMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {

            try
            {
             await   requestDelegate(context);
            }

            catch (Exception error)
            {
                var response = context.Response;

                response.ContentType = "application/json";
                var responseModel= new Response<string> { Succeeded=false,Message=error?.Message};

                switch (error)
                {
                    case ArgumentNullException:
                        responseModel.Message = "Required parameter is missing";
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = 400;
                        break;

                    case ArgumentException:
                    case FormatException:
                        responseModel.Message = "Invalid request data";
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = 400;
                        break;

                    case TimeoutException:
                        responseModel.Message = "Request timeout";
                        responseModel.StatusCode = HttpStatusCode.RequestTimeout;
                        response.StatusCode = 408;
                        break;

                    case OperationCanceledException:
                        responseModel.Message = "Request was cancelled";
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = 400;
                        break;

                    case SecurityTokenExpiredException:
                        responseModel.Message = "Token expired";
                        responseModel.StatusCode = HttpStatusCode.Unauthorized;
                        response.StatusCode = 401;
                        break;
                    case SecurityTokenException:
                        responseModel.Message = "Invalid token";
                        response.StatusCode = 401;
                        break;

                    case UnauthorizedAccessException:
                        responseModel.Message = "Unauthorized access";
                        responseModel.StatusCode = HttpStatusCode.Unauthorized;
                        response.StatusCode = 401;
                        break;

                    case KeyNotFoundException:
                        responseModel.Message = "Resource not found";
                        responseModel.StatusCode = HttpStatusCode.NotFound;
                        response.StatusCode = 404;
                        break;

                    case DbUpdateException:
                        responseModel.Message = "Database operation failed";
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = 400;
                        break;

                    case CustomValidationExeption:
                        responseModel.Message = error.Message;
                        responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                        response.StatusCode = 422;
                        break;

                    default:
                        responseModel.Message = "Internal Server Error";
                        responseModel.StatusCode = HttpStatusCode.InternalServerError;
                        response.StatusCode = 500;
                        break;
                }



                var json = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(json);
            }

        }
    }
}
