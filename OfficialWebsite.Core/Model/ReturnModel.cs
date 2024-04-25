namespace OfficialWebsite.Core.Model
{
    public class ReturnModel
    {
        private readonly Guid guid = Guid.NewGuid();

        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public Guid Guid => guid;

        public ReturnModel(bool success, string? message = null, object? data = null)
        {
            this.Success = success;
            this.Message = message;
            this.Data = data;
        }
    }
}
