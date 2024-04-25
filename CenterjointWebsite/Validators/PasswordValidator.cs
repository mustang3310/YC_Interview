namespace YCInterview.Web.Validators
{
    using FluentValidation;
    using YCInterview.Web.ViewModels;

    public class PasswordValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public PasswordValidator()
        {
            // 最小長度
            _ = this.RuleFor(m => m.NewPassword).MinimumLength(8).WithMessage("最小長度為8");

            // 最大長度
            _ = this.RuleFor(m => m.NewPassword).MaximumLength(12).WithMessage("最大長度為12");

            // 包含 {Number} 以上的大寫字母
            _ = this.RuleFor(m => m.NewPassword).Matches("(?:[A-Z].*){1,}").WithMessage("至少包含一個以上的大寫字母");

            // 包含 {Number} 以上的小寫字母
            _ = this.RuleFor(m => m.NewPassword).Matches("(?:[a-z].*){1,}").WithMessage("至少包含一個以上的小寫字母");

            // 包含 {Number} 以上的特殊字元
            _ = this.RuleFor(m => m.NewPassword).Matches("(?:[A-Z].*){1,}").WithMessage("至少包含一個以上的特殊字元");

            // 包含 {Number} 以上的數字
            _ = this.RuleFor(m => m.NewPassword).Matches("\\d{1,}").WithMessage("至少包含一個以上的數字");

            // 是否與 確認密碼一致
            _ = this.RuleFor(m => m.NewPassword).Equal(m => m.ConfirmedPassword).WithMessage("兩次密碼內容須一致");
        }
    }
}
