namespace YCInterview.Web.FileOperate
{
    public interface IFileComponent
    {
        /// <summary>
        /// 儲存檔案到Temp資料夾
        /// </summary>
        /// <param name="sourcePath">輸入路徑</param>
        /// <param name="formFile">儲存的檔案</param>
        public void SaveTempFile(string sourcePath, IFormFile formFile);

        /// <summary>
        /// 確認Temp資料夾有無此檔案
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool CheckTempFile(string fileName);

        /// <summary>
        /// 將檔案從Temp資料夾copy到目標資料夾
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationFolder"></param>
        public void CopyFilesFromTempToDestination(string sourcePath, string destinationFolder);
        public void CopyFilesFromTempToDestination(IList<string> sourcePathList, string destinationFolder);

        /// <summary>
        /// 刪除檔案
        /// </summary>
        /// <param name="sourcePath"></param>
        public void DeleteFiles(string sourcePath);
        public void DeleteFiles(IList<string> sourcePaths);

        /// <summary>
        /// 刪除暫存資料夾檔案
        /// </summary>
        /// <param name="sourcePath"></param>
        public void DeleteTempFiles(string sourcePath);
        public void DeleteTempFiles(IList<string> sourcePaths);

        /// <summary>
        /// 刪除備份檔案
        /// </summary>
        /// <param name="sourcePath"></param>
        public void DeleteBackUpFiles(string sourcePath);
        public void DeleteBackUpFiles(IList<string> sourcePaths);

        /// <summary>
        /// 備粉檔案到備份資料夾
        /// </summary>
        /// <param name="sourcePath"></param>
        public void BackupFiles(string sourcePath);
        public void BackupFiles(IList<string> sourcePaths);

        /// <summary>
        /// 將備份資料回復
        /// </summary>
        /// <param name="sourcePath"></param>
        public void RestoreFiles(string sourcePath);
        public void RestoreFiles(IList<string> sourcePaths);

        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="destinationPath"></param>
        /// <param name="formFile"></param>
        public void UploadFile(string destinationPath, IFormFile formFile);

        /// <summary>
        /// 加上上傳路徑
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <returns></returns>
        public string SetUploadPath(string sourcePath);

        /// <summary>
        /// 取得上傳路徑
        /// </summary>
        /// <returns></returns>
        public string GetUploadPath();

        /// <summary>
        /// 確認temp資料存在時間並清理資料夾
        /// </summary>
        public void CheckAndCleanTemp();
    }
}
