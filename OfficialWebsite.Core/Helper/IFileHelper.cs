namespace OfficialWebsite.Core.Helper
{
    /// <summary>
    /// 檔案服務
    /// </summary>
    public interface IFileHelper
    {
        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="destinationPath">目標路徑</param>
        /// <param name="fileName">檔案名稱</param>
        /// <param name="fileStream"></param>
        public void Upload(string destinationPath, string fileName, Stream fileStream);

        public void CreateDirectoryWhenNotExists(string destinationPath);

        /// <summary>
        /// 將路徑中的斜線反斜線替換成符合OS的規則
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReplaceSlashAndBackslash(string path);

        public void DeleteFile(string destinationPath);

        public void Copy(string sourcePath, string destinationPath);

        public bool Exists(string destinationPath);

        public string GetCompletePath(string path);
    }
}
