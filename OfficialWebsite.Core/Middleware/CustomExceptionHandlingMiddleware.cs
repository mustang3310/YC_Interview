namespace OfficialWebsite.Core.Middleware
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using OfficialWebsite.Core.Model;
    using System.Net;
    using System.Text;
    using System.Text.Json;

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this._next = next;
            this.logger = logger;
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
            context.Response.ContentType = "application/json";

            HttpResponse response = context.Response;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ReturnModel errorResponse = new(false);

            StringBuilder sb = new();
            _ = sb.AppendLine(string.Empty);
            _ = sb.AppendLine("{Guid} | {Message}");
            _ = sb.AppendLine("{Exception}");

            this.logger.LogError(
                sb.ToString(),
                errorResponse.Guid.ToString(),
                exception.Message,
                exception.StackTrace
            );

            // 如果不是 ajax 就重新導向
            if (!IsAjax(context.Request))
                context.Response.Redirect($"/System/Error/{errorResponse.Guid}", false);

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }

        /// <summary>
        /// 判斷 Request 是否是 ajax
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static bool IsAjax(HttpRequest request) =>
            request.Headers["X-Requested-With"].Equals("XMLHttpRequest")
            || request.Headers["Content-Type"].Equals("application/json");
    }

    public static class LoadExceptionHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
            => app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
