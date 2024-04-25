namespace OfficialWebsite.Core.DIC
{
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class FluentValidationExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFluentValidationExtension(this IServiceCollection services, Assembly assembly) =>
            services.AddValidatorsFromAssembly(assembly);
    }
}
