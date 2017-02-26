using System;

namespace EasyTicket.SharedResources {
    public static class DateExtensions {
        public static DateTime UnixTimeStampToDateTime(this int unixTimeStamp) {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static double ToUnixTimestamp(this DateTime dateTime) {
            TimeZoneInfo usersTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. Europe Standard Time");
            DateTime localDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, usersTimeZone);
            return (localDateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }
    }
}