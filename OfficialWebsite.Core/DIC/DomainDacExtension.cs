namespace OfficialWebsite.Core.DIC
{
    using Microsoft.Extensions.DependencyInjection;
    using OfficialWebsite.Core;

    public static class DomainDacExtension
    {
        /// <summary>
        /// 注入 Domain 的 Dac
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainDacExtension(this IServiceCollection services)
        {
            services.RegisterCustomTypes("Domain", "Dac");

            return services;
        }
    }
}
