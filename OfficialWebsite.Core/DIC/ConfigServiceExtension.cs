namespace OfficialWebsite.Core.DIC
{
    using Microsoft.Extensions.DependencyInjection;
    using OfficialWebsite.Core.Services;

    public static class ConfigServiceExtension
    {
        /// <summary>
        /// 注入取得 Appsettings 的 Service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConfigServiceExtension(this IServiceCollection services)
            => services.AddScoped(typeof(ConfigurationService));
    }
}
