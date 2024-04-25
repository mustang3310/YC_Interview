namespace OfficialWebsite.Core.Attributes
{
    using Microsoft.AspNetCore.Http;
    using OfficialWebsite.Core.Extensions;
    using System.ComponentModel.DataAnnotations;

    public class ImageFormatAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is null) return true;
            if (value is not IFormFile formFile) return false;
            return formFile.ValidImageExtension();
        }
    }
}