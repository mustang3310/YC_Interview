namespace OfficialWebsite.Core.Attributes
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Check if the image's length bigger than setting value
    /// </summary>
    public class MaxImageLengthAttribute : ValidationAttribute
    {
        public int MaxLength { get; set; }
        public MaxImageLengthAttribute(int maxLength) => this.MaxLength = maxLength;
        public override bool IsValid(object? value)
        {
            if (value is null) return true;
            if (value is not IFormFile formfile) return false;
            return formfile.Length <= this.MaxLength;
        }

    }
}
