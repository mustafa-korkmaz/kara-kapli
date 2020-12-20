using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Common
{
    /// <summary>
    /// provides common purpose usage methods
    /// </summary>
    public static class Utility
    {
        public static string GetStatusText(Status status)
        {
            switch (status)
            {
                case Status.Active:
                    return "Active";
                case Status.Passive:
                    return "Passive";
                case Status.Suspended:
                    return "Suspended";
                case Status.Deleted:
                    return "Deleted";
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        public static DateTime GetTurkeyCurrentDateTime()
        {
            DateTime utcTime = DateTime.UtcNow;
            //TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("GTB Standard Time");
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
        }

        public static string GetHashValue(string source)
        {
            string hash = "";
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, source);
            }
            return hash;
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string GetSqlWhereConditionInValues<T>(IEnumerable<T> inValues)
        {
            return string.Join(", ", inValues.Select(p => $"'{p.ToString()}'"));
        }

        public static DeviceType GetDeviceTypeByChannel(string channelType)
        {
            switch (channelType)
            {
                case ChannelType.Ios:
                    return DeviceType.Ios;
                case ChannelType.Android:
                    return DeviceType.Android;
                default:
                    return DeviceType.None;
            }
        }

        public static TResult GetResultFromJson<TResult>(string json)
        {
            return JsonConvert.DeserializeObject<TResult>(json, Converter.Settings);
        }

        public static string GetJsonFromObject<TRequest>(TRequest obj)
        {
            return JsonConvert.SerializeObject(obj, Converter.Settings);
        }

        public static string GetMethodResultCacheKey(Delegate method, IEnumerable<object> arguments)
        {
            var methodInfo = GetMethodInfo(method);

            return GetMethodResultCacheKey(methodInfo, arguments);
        }

        public static string GetMethodResultCacheKey(MemberInfo methodInfo, IEnumerable<object> arguments)
        {
            var methodName = string.Format("{0}.{1}.{2}",
                                       methodInfo.ReflectedType.Namespace,
                                       methodInfo.ReflectedType.Name,
                                       methodInfo.Name);

            var key = string.Format(
              "{0}({1})",
              methodName,
              string.Join(", ", arguments.Select(x => x != null ? x.ToString() : "<Null>")));
            return key;
        }

        public static MethodInfo GetMethodInfo(Delegate d)
        {
            return d.Method;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static int GetUnixTimeStamp(DateTime date)
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static (string fileName, string extension) GetFileNameAndExtension(string fileFullName)
        {
            var array = fileFullName.Split('.');

            if (array.Length < 2)
            {
                return (fileFullName, "");
            }

            var fileName = "";

            var extension = "";

            for (int i = 0; i < array.Length; i++)
            {
                if (i == array.Length - 1)
                {
                    extension = array[i];
                    break;
                }

                fileName += array[i];
            }

            return (fileName, extension);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy
                {
                    ProcessDictionaryKeys = true
                }
            },
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
