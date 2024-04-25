namespace OfficialWebsite.Core.DIC
{
    using Microsoft.Extensions.DependencyInjection;
    using OfficialWebsite.Core;

    public static class ApplicationServiceExtension
    {
        /// <summary>
        /// 加入 Application 的 Services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServiceExtension(this IServiceCollection services)
        {
            services.RegisterCustomTypes("Application", "Service");

            return services;
        }
    }
}
