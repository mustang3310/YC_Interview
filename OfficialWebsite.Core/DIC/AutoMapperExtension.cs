namespace OfficialWebsite.Core.DIC
{
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;
    using OfficialWebsite.Core;
    using System;
    using System.Reflection;

    public static class AutoMapperExtension
    {
        /// <summary>
        /// 自動注入 
        /// 1.各 WebApplication 資料夾下的 Mapper
        /// 2.AutoMapper的 Global Setting
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static IServiceCollection AddAutoMapperExtension(this IServiceCollection services, Assembly assembly) =>
            services.AddAutoMapper(cfg =>
                {
                    cfg.SourceMemberNamingConvention = new UpperUnderscoreNamingConvention();
                    cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention();

                    string currentAssemblyName =
                        Assembly.GetExecutingAssembly().FullName
                        ?? throw new NullReferenceException(nameof(currentAssemblyName));

                    DependencyInjectionUtil
                        .GetClassTypes(currentAssemblyName, "Mapper")
                        .ForEach(t => cfg.AddProfile(t));

                }, assembly);
    }
}
