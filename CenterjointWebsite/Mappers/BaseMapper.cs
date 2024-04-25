namespace YCInterview.Web.Mappers
{
    using AutoMapper;
    using OfficialWebsite.Core;

    public class BaseMapper : Profile
    {
        public BaseMapper()
        {
            SourceMemberNamingConvention = new UpperUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();
        }
    }
}
