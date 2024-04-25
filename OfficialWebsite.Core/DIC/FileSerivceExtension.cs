namespace OfficialWebsite.Core.DIC
{
    using Microsoft.Extensions.DependencyInjection;
    using OfficialWebsite.Core.Helper;

    public static class FileSerivceExtension
    {
        /// <summary>
        /// 注入 FileService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileSerivceExtension(this IServiceCollection services)
        {
            services.AddScoped<IFileHelper, LocalFileHelper>();

            return services;
        }
    }
}
