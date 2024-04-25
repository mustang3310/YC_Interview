namespace YCInterview.Web.Mappers
{
    using Domain.Models;
    using YCInterview.Web.ViewModels;

    public class AccountMapper : BaseMapper
    {
        public AccountMapper()
        {
            _ = CreateMap<LoginViewModel, UserModel>().ReverseMap();
            _ = CreateMap<ChangePasswordViewModel, UserModel>().ReverseMap();
            _ = CreateMap<LoginViewModel, LoginLogModel>().ReverseMap();
        }
    }
}
