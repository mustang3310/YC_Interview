namespace OfficialWebsite.Core.Helper
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System.Text;

    public static class SessionHelper
    {
        /// <summary>
        /// 設定 Session 內容
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            JsonSerializerSettings settings = new()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            session.SetString(key, JsonConvert.SerializeObject(value, settings));
        }

        /// <summary>
        /// 取得 Session 內容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetObjectFromJson<T>(this ISession session, string key) where T : new()
        {
            string? value = session.GetString(key) ?? default;

            if (value is null)
                return new T();
            else
                return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 移除 Session 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        public static void Remove(this ISession session, string key)
            => session.Remove(key);
    }
}
