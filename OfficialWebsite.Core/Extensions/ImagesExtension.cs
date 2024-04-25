namespace OfficialWebsite.Core.Extensions
{
    using Microsoft.AspNetCore.Http;

    public static class ImagesExtension
    {
        // 設定允許的預設格式
        public static readonly IEnumerable<string> AcceptImgExtentions
            = new List<string> { ".jpg", ".jpeg", ".png", ".bmp" };

        /// <summary>
        /// 確認圖片長寬是否符合
        /// </summary>
        /// <param name="file"></param>
        /// <param name="width">寬</param>
        /// <param name="height">高</param>
        /// <returns></returns>
        public static bool CheckSize(this IFormFile file, int width, int height)
        {
            if (file != null)
            {
                using (Stream stream = file.OpenReadStream())
                {
                    Image imageSharp = Image.Load(stream);

                    if (imageSharp != null)
                        return imageSharp.Width <= width && imageSharp.Height <= height;
                }
            }
            return false;
        }

        /// <summary>
        /// 確認檔案格式
        /// </summary>
        /// <param name="file"></param>
        /// <param name="acceptImgExtentions">允許的圖片格式</param>
        /// <returns></returns>
        public static bool ValidImageExtension(this IFormFile file, IList<string>? acceptImgExtentions = null)
        {
            // 設定允許的預設格式
            acceptImgExtentions ??= AcceptImgExtentions.ToList();

            // 取得檔案格式
            string fileExtention = Path.GetExtension(file.FileName);

            return acceptImgExtentions.Any(x => x.Equals(fileExtention));
        }
    }
}
