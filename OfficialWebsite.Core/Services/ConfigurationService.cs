namespace OfficialWebsite.Core.Services
{
    using Microsoft.Extensions.Configuration;

    public class ConfigurationService
    {
        private readonly IConfiguration configuration;
        public ConfigurationService(IConfiguration configuration)
            => this.configuration = configuration;

        public string GetValue(ConfigurationOptions option)
        {
            return configuration.GetSection(option.ToString()).Value ?? throw new ArgumentException(null, nameof(option));
        }

        public T GetModel<T>(ConfigurationOptions option) where T : new()
        {
            T model = new();
            configuration.GetSection(option.ToString()).Bind(model);

            return model;
        }
    }
}
