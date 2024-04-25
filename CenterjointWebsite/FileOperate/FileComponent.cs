namespace YCInterview.Web.FileOperate
{
    using OfficialWebsite.Core.Helper;
    using OfficialWebsite.Core.Services;

    public class FileComponent : IFileComponent
    {
        private readonly ConfigurationService configurationService;
        private readonly IFileHelper fileHelper;
        private const string backupFolder = "Backup";
        private const string tempFolder = "Temp";

        public FileComponent(IFileHelper fileHelper, ConfigurationService configurationService)
        {
            this.fileHelper = fileHelper;
            this.configurationService = configurationService;
        }

        public void SaveTempFile(string sourcePath, IFormFile formFile)
        {
            string fileName = Path.GetFileName(sourcePath);
            string completeSorcePath = SetUploadPath(tempFolder);
            fileHelper.Upload(completeSorcePath, fileName, formFile.OpenReadStream());
        }

        public bool CheckTempFile(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            string path = Path.Combine(tempFolder, fileName);
            path = SetUploadPath(path);
            return fileHelper.Exists(path);
        }

        public void CopyFilesFromTempToDestination(string sourcePath, string destinationFolder)
        {
            string fileName = Path.GetFileName(sourcePath);
            fileHelper.CreateDirectoryWhenNotExists(SetUploadPath(tempFolder));
            string tempPath = Path.Combine(SetUploadPath(tempFolder), fileName);
            fileHelper.CreateDirectoryWhenNotExists(SetUploadPath(destinationFolder));

            string destinationPath = Path.Combine(SetUploadPath(destinationFolder), fileName);
            fileHelper.Copy(tempPath, destinationPath);
        }

        public void CopyFilesFromTempToDestination(IList<string> sourcePathList, string destinationFolder)
        {
            foreach (string sourcePath in sourcePathList)
            {
                this.CopyFilesFromTempToDestination(sourcePath, destinationFolder);
            }
        }

        public void DeleteFiles(string sourcePath)
        {
            string completeSorcePath = SetUploadPath(sourcePath);
            fileHelper.DeleteFile(completeSorcePath);
        }

        public void DeleteFiles(IList<string> sourcePaths)
        {
            foreach (string fileName in sourcePaths)
            {
                this.DeleteFiles(fileName);
            }
        }

        public void DeleteTempFiles(string sourcePath)
        {
            sourcePath = Path.GetFileName(sourcePath);
            string tempFileName = SetUploadPath(Path.Combine(tempFolder, sourcePath));
            fileHelper.DeleteFile(tempFileName);
        }

        public void DeleteTempFiles(IList<string> sourcePaths)
        {
            foreach (string fileName in sourcePaths)
            {
                this.DeleteTempFiles(fileName);
            }
        }

        public void DeleteBackUpFiles(string sourcePath)
        {
            string backupPath = SetBackUpPath(sourcePath);
            fileHelper.DeleteFile(backupPath);
        }

        public void DeleteBackUpFiles(IList<string> sourcePaths)
        {
            foreach (string sourcePath in sourcePaths)
            {
                this.DeleteBackUpFiles(sourcePath);
            }
        }

        public void BackupFiles(string sourcePath)
        {
            string completeSorcePath = SetUploadPath(sourcePath);
            string backupPath = SetBackUpPath(sourcePath);
            fileHelper.Copy(completeSorcePath, backupPath);
        }

        public void BackupFiles(IList<string> sourcePaths)
        {
            foreach (string sourcePath in sourcePaths)
            {
                this.BackupFiles(sourcePath);
            }
        }

        public void RestoreFiles(string sourcePath)
        {
            string completeSorcePath = SetUploadPath(sourcePath);
            string backupPath = SetBackUpPath(sourcePath);
            fileHelper.Copy(backupPath, completeSorcePath);
        }

        public void RestoreFiles(IList<string> sourcePaths)
        {
            foreach (string sourcePath in sourcePaths)
            {
                this.RestoreFiles(sourcePath);
            }
        }

        public void UploadFile(string destinationPath, IFormFile formFile)
        {
            string directory = Path.GetDirectoryName(destinationPath) ?? "";
            string completeDirectoryPath = SetUploadPath(directory);
            fileHelper.Upload(completeDirectoryPath, Path.GetFileName(destinationPath), formFile.OpenReadStream());
        }

        public void CheckAndCleanTemp()
        {
            string destnationPath = SetUploadPath(tempFolder);
            string folderPath = fileHelper.GetCompletePath(destnationPath);
            if (!Directory.Exists(folderPath))
                return;
            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);

                DateTime createTimeDifference = DateTime.Now.AddTicks(-fileInfo.CreationTime.Ticks);
                if (createTimeDifference.Day > 1)
                    File.Delete(file);
            }
        }

        public string SetUploadPath(string sourcePath)
        {
            string uploadFolder = configurationService.GetValue(ConfigurationOptions.UploadFilePath);
            return Path.Combine(uploadFolder, sourcePath);
        }

        public string GetUploadPath()
        {
            return configurationService.GetValue(ConfigurationOptions.UploadFilePath);
        }

        private string SetBackUpPath(string source)
        {
            string backupDirectory = SetUploadPath(backupFolder);
            fileHelper.CreateDirectoryWhenNotExists(backupDirectory);
            string backupPath = Path.Combine(backupDirectory, Path.GetFileName(source));
            return backupPath;
        }
    }
}
