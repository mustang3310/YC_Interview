namespace OfficialWebsite.Core.Attributes
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class HeightRangeAttribute : ValidationAttribute
    {
        public double MinHeight { get; set; }
        public double MaxHeight { get; set; }

        public HeightRangeAttribute(double minHeight, double maxHeight)
        {
            this.MinHeight = minHeight;
            this.MaxHeight = maxHeight;
        }

        public override bool IsValid(object? value)
        {
            if (value is null) return true;
            if (value is not IFormFile inputImage) return false;

            using Stream stream = inputImage.OpenReadStream();
            Image imageSharp = Image.Load(stream);
            if (imageSharp != null)
                return imageSharp.Height <= this.MaxHeight && imageSharp.Height >= this.MinHeight;
            else
            {
                return false;
            }
        }

        //public override string FormatErrorMessage(string name) => $"圖片寬度限制在{MinHeight} ~ {MaxHeight}";
    }
}
