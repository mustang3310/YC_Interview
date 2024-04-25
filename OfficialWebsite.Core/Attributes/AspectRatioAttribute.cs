namespace OfficialWebsite.Core.Attributes
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Check if the image's Aspect Ratio same as setting value
    /// </summary>
    public class AspectRatioAttribute : ValidationAttribute
    {
        public double Width { get; }
        public double Height { get; }

        public double AspectRatio => this.Width / this.Height;

        public AspectRatioAttribute(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return true;
            if (value is not IFormFile file) return false;

            using Stream stream = file.OpenReadStream();
            try
            {
                Image imageSharp = Image.Load(stream);

                return imageSharp != null && (double)imageSharp.Width / (double)imageSharp.Height == this.AspectRatio;
            }
            catch (UnknownImageFormatException)
            {
                return false;
            }
        }
    }
}
