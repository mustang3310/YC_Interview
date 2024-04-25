namespace OfficialWebsite.Core.Model
{
    using Newtonsoft.Json;
    using System;

    public class RecaptchaModel
    {
        public string Url { get; set; }

        public string Key { get; set; }

        public string Secret { get; set; }

        public string GetUrl(string response)
            => $"{this.Url}?secret={this.Secret}&response={response}";
    }

    public class RecaptchaResultModel
    {
#pragma warning disable IDE1006 // 命名樣式
        public bool success { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }

        [JsonProperty("error-codes")]
        public IList<string> ErrorMessage { get; set; }
#pragma warning restore IDE1006 // 命名樣式
    }
}
