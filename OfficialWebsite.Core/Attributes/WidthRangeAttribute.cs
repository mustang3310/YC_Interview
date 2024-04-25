namespace OfficialWebsite.Core.Attributes
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class WidthRangeAttribute : ValidationAttribute
    {
        public double MinWidth { get; set; }
        public double MaxWidth { get; set; }

        public WidthRangeAttribute(double minWidth, double maxWidth)
        {
            this.MinWidth = minWidth;
            this.MaxWidth = maxWidth;
        }

        public override bool IsValid(object? value)
        {
            if (value is null) return true;
            if (value is not IFormFile inputImage)
                return false;

            using Stream stream = inputImage.OpenReadStream();
            Image imageSharp = Image.Load(stream);

            if (imageSharp != null)
            {
                return imageSharp.Width <= this.MaxWidth && imageSharp.Width >= this.MinWidth;
            }
            else
            {
                return false;
            }
        }

        //public override string FormatErrorMessage(string name) => $"圖片長度限制在{MinWidth} ~ {MaxWidth}";

    }
}
