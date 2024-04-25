namespace OfficialWebsite.Core.Middleware
{
    using Microsoft.AspNetCore.Builder;

    public static class CspHeadersMiddleware
    {
        public static HeaderPolicyCollection GetCspHeadersMiddleware()
            => new HeaderPolicyCollection()
                .AddContentSecurityPolicy(builder =>
                {
                    _ = builder.AddUpgradeInsecureRequests();
                    _ = builder.AddDefaultSrc().From("*");
                    _ = builder.AddFontSrc().From("https://fonts.gstatic.com").Self();
                    _ = builder.AddImgSrc().Self()
                    .From("https://www.w3.org")
                    .From("data:")
                    .From("blob:");
                    _ = builder.AddScriptSrc().WithNonce().Self();
                    _ = builder.AddStyleSrc().From("https://fonts.googleapis.com").Self();
                    _ = builder.AddFrameSrc().From("https://www.google.com").Self();
                    _ = builder.AddObjectSrc().None();
                    _ = builder.AddBaseUri().Self();
                });
    }

    public static class CspHeadersMiddlewareExtension
    {
        public static IApplicationBuilder LoadCspHeadersMiddlewareExtension(this IApplicationBuilder app)
            => app.UseSecurityHeaders(CspHeadersMiddleware.GetCspHeadersMiddleware());
    }
}
