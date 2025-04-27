using Azure.Core;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Messages;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using System.Text.Json;

namespace FIAP.Pos.Tech.Challenge.Api
{
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
            => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }

    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Concat(ex.InnerException?.Message ?? ex.Message, context.Request.Method,
                context.Request.IsHttps ? " https://" : " http://",
                context.Request.Host.Value,
                context.Request.Path.Value ?? "",
                context.Request.QueryString.Value ?? ""));
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string stackTrace = (ex.StackTrace ?? "");
            stackTrace = stackTrace.Length > 200 ? stackTrace.IndexOf(" in ") > -1 ? stackTrace.Substring(0, stackTrace.IndexOf(" in ")) : stackTrace.Substring(0, 200) : stackTrace;
            stackTrace += " ...";

            var m = ModelResultFactory.Error<object>(default!,
                string.Concat(context.Request.Method,
                context.Request.IsHttps ? " https://" : " http://",
                context.Request.Host.Value,
                context.Request.Path.Value ?? "",
                context.Request.QueryString.Value ?? ""),
                ex.InnerException?.Message ?? ex.Message,
                stackTrace);
            m.AddMessage(ErrorMessages.InternalServerError);

            string exceptionResult = JsonSerializer.Serialize(m);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
