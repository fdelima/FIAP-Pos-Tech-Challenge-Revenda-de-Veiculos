using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Messages;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net;
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
            catch (InvalidOperationException ex)
            {
                await HandleInvalidOperationExceptionAsync(context, ex);
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

        private static Task WriteResponseAsync(HttpContext context, ModelResult<object> modelResult, int statusCode)
        {
            string exceptionResult = JsonSerializer.Serialize(modelResult);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(exceptionResult);
        }

        private static Task HandleInvalidOperationExceptionAsync(HttpContext context, InvalidOperationException ex)
        {
            ModelResult<object> m = ModelResultFactory.None();
            m.AddMessage(ex.InnerException?.Message ?? ex.Message);
            return WriteResponseAsync(context, m, (int)HttpStatusCode.BadRequest);
        }



        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string stackTrace = (ex.StackTrace ?? "");
            stackTrace = stackTrace.Length > 200 ? stackTrace.IndexOf(" in ") > -1 ? stackTrace.Substring(0, stackTrace.IndexOf(" in ")) : stackTrace.Substring(0, 200) : stackTrace;
            stackTrace += " ...";

            ModelResult<object> m = ModelResultFactory.Error<object>(default!,
                string.Concat(context.Request.Method,
                context.Request.IsHttps ? " https://" : " http://",
                context.Request.Host.Value,
                context.Request.Path.Value ?? "",
                context.Request.QueryString.Value ?? ""),
                ex.InnerException?.Message ?? ex.Message,
                stackTrace);
            m.AddMessage(ErrorMessages.InternalServerError);

            return WriteResponseAsync(context, m, (int)HttpStatusCode.InternalServerError);

        }
    }
}
