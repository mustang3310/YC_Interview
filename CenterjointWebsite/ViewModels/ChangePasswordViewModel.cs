namespace YCInterview.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ChangePasswordViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "帳號")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "新密碼")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "確認密碼")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }

        public string? ErrorMessages { get; set; }
    }
}
