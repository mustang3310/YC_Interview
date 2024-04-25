namespace OfficialWebsite.Core
{
    using Application.Implements;
    using Application.Interfaces;
    using Domain.Implements;
    using Domain.Interfaces;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class DependencyInjectionUtil
    {
        public static void RegisterCustomTypes(this IServiceCollection services, string assemblyName, string name)
        {

            List<Type> serviceTypes = GetClassTypes(assemblyName, name);

            foreach (Type? serviceType in serviceTypes)
            {
                _ = services.AddScoped(serviceType);
            }

            _ = services.AddScoped<IBaseService, BaseService>();
            _ = services.AddScoped<IAccountService, AccountService>();
            _ = services.AddScoped<IUserDac, UserDac>();
            _ = services.AddScoped<ILoginLogService, LoginLogService>();
            _ = services.AddScoped<ILoginLogDac, LoginLogDac>();

            _ = services.AddScoped<IParameterService, ParameterService>();
            _ = services.AddScoped<IParameterDac, ParameterDac>();

            _ = services.AddMemoryCache();

        }

        /// <summary>
        /// 取得 Class Type
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="name">Class Name 的結尾</param>
        public static List<Type> GetClassTypes(string assemblyName, string name)
        {
            Assembly assembly = Assembly.Load(assemblyName);

            return
                assembly
                    .GetTypes()
                    .Where(type => type.IsClass && type.Name.EndsWith(name)
                    && type.Namespace is not null && (type.Namespace.Equals("Application.Services") || type.Namespace.Equals("Domain.Dacs"))
                    )
            .ToList();
        }
    }
}
