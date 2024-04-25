namespace YCInterview.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        [Required(ErrorMessage = "請輸入帳號")]
        [Display(Name = "帳號:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [Display(Name = "密碼:")]
        public string Password { get; set; }

        public string? Token { get; set; }

        public string? ErrorMessage { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
