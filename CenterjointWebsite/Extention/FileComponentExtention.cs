namespace YCInterview.Web.Extention
{
    using YCInterview.Web.FileOperate;

    public static class FileComponentExtension
    {
        /// <summary>
        /// 注入 FileService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileComponentExtension(this IServiceCollection services)
        {
            services.AddScoped<IFileComponent, FileComponent>();

            return services;
        }
    }
}
