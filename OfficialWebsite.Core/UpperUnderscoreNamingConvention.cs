namespace OfficialWebsite.Core
{
    using AutoMapper;

    public sealed class UpperUnderscoreNamingConvention : INamingConvention
    {
        public static readonly UpperUnderscoreNamingConvention Instance = new();
        public string SeparatorCharacter => "_";
        public string[] Split(string input) => input.Split('_', StringSplitOptions.RemoveEmptyEntries);
    }
}
