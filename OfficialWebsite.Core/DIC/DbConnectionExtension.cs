namespace OfficialWebsite.Core.DIC
{
    using Infrastructure;
    using Microsoft.Extensions.DependencyInjection;

    public static class DbConnectionExtension
    {
        /// <summary>
        /// 注入 DB 連線
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbConnectionExtension(this IServiceCollection services) =>
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    }
}
