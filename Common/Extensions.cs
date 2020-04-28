using System;
using System.Text.RegularExpressions;

namespace Common
{
    public static class Extensions
    {
        /// <summary>
        /// returns Turkish date time for a UTC date time.
        /// </summary>
        /// <param name="utcDateTime">UTC date time</param>
        /// <returns></returns>
        public static DateTime ToTurkeyDateTime(this DateTime utcDateTime)
        {
            //TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("GTB Standard Time");
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, tzi); // convert from utc to local
        }

        /// <summary>
        /// returns date time for a formatted string.
        /// string format must be [dd.MM.yyyy HH:mm]
        /// </summary>
        /// <param name="dateTimeStr"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string dateTimeStr)
        {
            var pat = @"^(0?[1-9]|[12][0-9]|3[01])\.(0?[1-9]|1[0-2])\.\d\d\d\d (0?[0-9]|1[0-9]|2[0-3]):([0-9]|[0-5][0-9])$";

            // Instantiate the regular expression object.
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match m = r.Match(dateTimeStr);

            if (m.Success)
            {
                DateTime date = DateTime.ParseExact(dateTimeStr, "dd.MM.yyyy HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture);
                return date;
            }

            //01.01.1900 for no match
            return new DateTime(1900, 1, 1);
        }

        /// <summary>
        /// returns formatted string for a dateTime.
        /// string format will be [dd.MM.yyyy HH:mm]
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy HH:mm");
        }


        /// <summary>
        /// returns date for a formatted string.
        /// string format must be [dd.MM.yyyy]
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static DateTime ToDate(this string dateStr)
        {
            dateStr += " 00:00"; //add hour and min
            var pat = @"^(0?[1-9]|[12][0-9]|3[01])\.(0?[1-9]|1[0-2])\.\d\d\d\d (00|[0-9]|1[0-9]|2[0-3]):([0-9]|[0-5][0-9])$";

            // Instantiate the regular expression object.
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match m = r.Match(dateStr);

            if (m.Success)
            {
                DateTime date = DateTime.ParseExact(dateStr, "dd.MM.yyyy HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture);
                return date;
            }

            //01.01.1900 for no match
            return new DateTime(1900, 1, 1);
        }

        /// <summary>
        /// returns formatted string for a dateTime.
        /// string format will be [dd.MM.yyyy]
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// removes turkish chars from the given text and returns english ones
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToEnglishChars(this string text)
        {
            return text.Replace('ş', 's').Replace('Ş', 'S')
                .Replace('ğ', 'g').Replace('Ğ', 'G')
                .Replace('ü', 'u').Replace('Ü', 'U')
                .Replace('ı', 'i').Replace('İ', 'I')
                .Replace('ö', 'o').Replace('Ö', 'O')
                .Replace('ı', 'i').Replace('İ', 'I')
                .Replace('ç', 'c').Replace('Ç', 'C');
        }

        public static string ToPriceText(this decimal price)
        {
            return price.ToString("0.00");
        }

        /// <summary>
        /// returns two digits after decimal point
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal ToPriceDecimal(this decimal price)
        {
            return Convert.ToDecimal(price.ToString("0.00"));
        }

        public static string ToSnakeCase(this string o) =>
           Regex.Replace(o, @"(\w)([A-Z])", "$1_$2").ToLower();
    }
}
