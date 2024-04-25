namespace Domain
{
    using Microsoft.AspNetCore.Http;

    public static class DateTimeExtension
    {
        public static string ToUtcTimeFormat(this DateTime datetime, string formatter = "yyyy-MM-dd HH:mm:ss")
            => datetime.ToUniversalTime().ToString(formatter);

        public static string ToUtcTimeFormat(this DateTime? datetime, string formatter = "yyyy-MM-dd HH:mm:ss")
            => datetime.HasValue ? datetime.Value.ToUtcTimeFormat() : string.Empty;

        public static string ToFormat(this DateTime? datetime, string formatter = "yyyy-MM-dd HH:mm:ss")
            => datetime.HasValue ? datetime.Value.ToString(formatter) : string.Empty;

        public static string ToLocalTimeFormat(this DateTime datetime, HttpContext httpContext, string formatter = "yyyy-MM-dd HH:mm:ss")
        {
            DateTime utcDateTime = DateTime.SpecifyKind(datetime, DateTimeKind.Utc);

            TimeZoneInfo defaultTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Asia/Taipei");
            TimeZoneInfo localZone;
            try
            {
                string? timeOffset = httpContext.Request.Cookies["timeoffest"];
                int timeZoneOffsetMinutes = int.Parse(timeOffset);
                TimeSpan offset = TimeSpan.FromMinutes(timeZoneOffsetMinutes);
                localZone = TimeZoneInfo.CreateCustomTimeZone("LocalZone", offset, "LocalZone", "LocalZone");
            }
            catch (Exception)
            {
                localZone = defaultTimeZoneInfo;
            }

            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localZone);

            return localTime.ToString(formatter);
        }
    }
}
