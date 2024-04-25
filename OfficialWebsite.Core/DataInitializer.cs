namespace OfficialWebsite.Core
{
    using Application.Services;

    public class DataInitializer
    {
        private readonly string _sqlTableFilePath = "AppData/Database/schema.sql";
        private readonly string _sqlDataFilePath = "AppData/Database/data.sql";
        private readonly DataInitialService dataInitialService;

        public DataInitializer(DataInitialService dataInitialService)
        {
            this.dataInitialService = dataInitialService;

            CreateSchema();
            InsertData();
        }

        private void CreateSchema()
        {
            using StreamReader sr = new StreamReader(_sqlTableFilePath);
            dataInitialService.Excute(sr.ReadToEnd());
        }

        private void InsertData()
        {
            using StreamReader sr = new StreamReader(_sqlDataFilePath);
            dataInitialService.Excute(sr.ReadToEnd());
        }
    }
}