using Application.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using OfficialWebsite.Core;
using OfficialWebsite.Core.DIC;
using OfficialWebsite.Core.Middleware;
using Serilog;
using System.Reflection;
using System.Text.RegularExpressions;
using YCInterview.Web.Extention;

// Serilog Setting Phase 1
Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    // Serilog Setting Phase 2
    _ = builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
    );

    // Add services to the container.
    _ = builder.Services.AddControllersWithViews();

    _ = builder.Services
        .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "Centerjoint"; // 設置 Cookie 名稱
            options.Cookie.HttpOnly = true; // 設置 Cookie 為 HttpOnly，以增加安全性
            options.Cookie.SameSite = SameSiteMode.Lax; // 設置 SameSite 屬性以防止跨站請求偽造 (CSRF) 攻擊
            //options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // 設置 SecurePolicy 以要求使用 HTTPS
            options.LoginPath = "/Account/Login"; // 登入頁面的路徑
            //options.AccessDeniedPath = "/Account/AccessDenied"; // 拒絕訪問頁面的路徑
            options.LogoutPath = "/Account/Logout"; // 登出頁面的路徑
            options.ReturnUrlParameter = "returnUrl"; // 設置返回 URL 的查詢參數名稱
            options.SlidingExpiration = true; // 啟用滑動過期，允許在一段時間內無操作時更新身份驗證的過期時間
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // 設置身份驗證的過期時間
        });


    // Add Session
    _ = builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromSeconds(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    _ = builder.Services.AddApplicationServiceExtension();

    _ = builder.Services.AddAutoMapperExtension(Assembly.GetExecutingAssembly());

    _ = builder.Services.AddConfigServiceExtension();

    _ = builder.Services.AddDbConnectionExtension();

    _ = builder.Services.AddDomainDacExtension();

    _ = builder.Services.AddFileSerivceExtension();

    _ = builder.Services.AddFluentValidationExtension(Assembly.GetExecutingAssembly());

    _ = builder.Services.AddFileComponentExtension();

    WebApplication app = builder.Build();

    // 啟用客製化錯誤處理
    _ = app.UseExceptionHandling();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        _ = app.UseExceptionHandler("/Home/Error");
    }

    _ = app.UseSession();

    _ = app.UseStaticFiles();

    _ = app.UseRouting();

    _ = app.UseAuthorization();

    _ = app.UseAuthentication();

    _ = app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}/{id?}");

    #region CSP Settings
    HeaderPolicyCollection policyCollection = new HeaderPolicyCollection()
    .AddContentSecurityPolicy(builder =>
    {
        _ = builder.AddUpgradeInsecureRequests();
        _ = builder.AddDefaultSrc().From("*");
#pragma warning disable S1075 // URIs should not be hardcoded
        _ = builder.AddFontSrc().From("https://fonts.gstatic.com").Self();
        _ = builder.AddImgSrc().Self()
        .From("https://www.w3.org")
        .From("data:")
        .From("blob:");
        _ = builder.AddScriptSrc().WithNonce().Self()
        .From("https://www.gstatic.com/recaptcha/")
        .From("https://www.google.com/recaptcha/")
        .From("https://unpkg.com/trix@2.0.0/dist/");
        _ = builder.AddStyleSrc().From("https://fonts.googleapis.com").Self()
        .From("https://unpkg.com/trix@2.0.0/dist/").UnsafeInline();
        _ = builder.AddFrameSrc().From("https://www.google.com").Self();
#pragma warning restore S1075 // URIs should not be hardcoded
        _ = builder.AddObjectSrc().None();
        _ = builder.AddBaseUri().Self();
    });

    _ = app.UseSecurityHeaders(policyCollection);
    #endregion

    string str = Regex.Replace(
        Path.Combine(Directory.GetCurrentDirectory(), "AppData/Database/yc.sqlite"),
        @"[/\\]", Path.DirectorySeparatorChar.ToString(),
         RegexOptions.None,
         TimeSpan.FromSeconds(10)
        );

    if (!File.Exists(str))
    {
        Console.WriteLine("Initial Data Start");
        using (IServiceScope scope = app.Services.CreateScope())
        {
            DataInitialService dataService = scope.ServiceProvider.GetRequiredService<DataInitialService>();
            _ = new DataInitializer(dataService);
        }
        Console.WriteLine("Initial Data End");
    }
    Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}