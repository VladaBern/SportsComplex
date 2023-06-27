using Microsoft.AspNetCore.Http;
using SportsComplex.Exceptions;
using SportsComplex.Responses;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace SportsComplex.Middlewares
{
    public class ExceptionHandlerMidleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMidleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(InvalidDataException ex)
            {
                context.Response.StatusCode = 400;
                string jsonString = JsonSerializer.Serialize(new BadRequestResponse(ex.Message));
                await context.Response.WriteAsync(jsonString);
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
            }
        }
    }
}
