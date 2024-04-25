namespace OfficialWebsite.Core.Helper
{
    using Microsoft.AspNetCore.Hosting;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 本機檔案服務
    /// </summary>
    public class LocalFileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _webHost;

        /// <summary>
        /// 根目錄
        /// </summary>
        /// <param name="rootPath"></param>
        public LocalFileHelper(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        /// <summary>
        /// 檔案上傳
        /// </summary>
        /// <param name="destinationPath">相對路徑(會自動加上根目錄)</param>
        /// <param name="fileByte"></param>
        public void Upload(string destinationPath, string fileName, Stream fileStream)
        {
            string path = Path.Combine(_webHost.ContentRootPath, RemoveSlashFromBegin(destinationPath));

            path = ReplaceSlashAndBackslash(path);

            CreateDirectory(path);

            if (File.Exists(path))
                File.Delete(path);

            //寫入檔案
            using (FileStream fileStreamDestination = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                fileStream.CopyTo(fileStreamDestination);
            }
        }

        public void CreateDirectoryWhenNotExists(string destinationPath)
        {
            string path = Path.Combine(_webHost.ContentRootPath, RemoveSlashFromBegin(destinationPath));

            path = ReplaceSlashAndBackslash(path);

            CreateDirectory(path);
        }

        public void DeleteFile(string destinationPath)
        {
            string path = Path.Combine(_webHost.ContentRootPath, RemoveSlashFromBegin(destinationPath));

            path = ReplaceSlashAndBackslash(path);
            if (File.Exists(path))
                File.Delete(path);
        }

        public void Copy(string sourcePath, string destinationPath)
        {
            string completeSourcePath = Path.Combine(_webHost.ContentRootPath, RemoveSlashFromBegin(sourcePath));
            completeSourcePath = ReplaceSlashAndBackslash(completeSourcePath);

            string completeDestinationPath = Path.Combine(_webHost.ContentRootPath, RemoveSlashFromBegin(destinationPath));
            completeDestinationPath = ReplaceSlashAndBackslash(completeDestinationPath);

            File.Copy(completeSourcePath, completeDestinationPath);
        }

        private void CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        /// <summary>
        /// 刪除路徑前面的斜線與反斜線
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string RemoveSlashFromBegin(string path)
        => Regex.Replace(
            path,
            @"^[/\\]+",
            "",
            RegexOptions.None,
            TimeSpan.FromSeconds(10));


        /// <summary>
        /// 將路徑中的斜線反斜線替換成符合OS的規則
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReplaceSlashAndBackslash(string path)
        => Regex.Replace(
            path,
            @"[/\\]",
            Path.DirectorySeparatorChar.ToString(),
            RegexOptions.None,
            TimeSpan.FromSeconds(10));


        /// <summary>
        /// 檔案是否存在
        /// </summary>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        public bool Exists(string destinationPath)
        {
            string path = Path.Combine(_webHost.ContentRootPath, RemoveSlashFromBegin(destinationPath));
            path = ReplaceSlashAndBackslash(path);

            return File.Exists(path);
        }

        public string GetCompletePath(string path)
        {
            path = Path.Combine(_webHost.ContentRootPath, RemoveSlashFromBegin(path));
            path = ReplaceSlashAndBackslash(path);
            return path;
        }
    }
}
