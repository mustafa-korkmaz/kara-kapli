using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public static string GetUserLogTypeText(UserLogType userLogType)
        {
            switch (userLogType)
            {
                case UserLogType.None:
                    return "";
                case UserLogType.ProfileApproved:
                    return "Profile approved";
                case UserLogType.ProfileDeclined:
                    return "Profile declined";
                case UserLogType.InProgress:
                    return "Profile made in progress";
                case UserLogType.InProgressCompleted:
                    return "Profile made in progress completed";
                case UserLogType.Passive:
                    return "Profile passived";
                case UserLogType.VideoVerified:
                    return "Video verified";
                case UserLogType.VideoUnverified:
                    return "Video unverified";
                case UserLogType.BackgroundDocumentVerified:
                    return "Background Check document verified";
                case UserLogType.BackgroundDocumentUnverified:
                    return "Background Check document unverified";
                case UserLogType.ResumeVerified:
                    return "Resume verified";
                case UserLogType.ResumeUnverified:
                    return "Resume unverified";
                case UserLogType.IdentityVerified:
                    return "Identity verified";
                case UserLogType.IdentityUnverified:
                    return "Identity unverified";
                case UserLogType.CitizenshipVerified:
                    return "Citizenship verified";
                case UserLogType.CitizenshipUnverified:
                    return "Citizenship unverified";
                case UserLogType.DegreesVerified:
                    return "Degree's verified";
                case UserLogType.DegreesUnverified:
                    return "Degree's unverified";
                case UserLogType.CertificationVerified:
                    return "Certification verified";
                case UserLogType.CertificationUnverified:
                    return "Certification unverified";
                case UserLogType.TranscriptVerified:
                    return "Transcript verified";
                case UserLogType.TranscriptUnverified:
                    return "Transcript unverified";
                case UserLogType.EssayVerified:
                    return "Essay verified";
                case UserLogType.EssayUnverified:
                    return "Essay unverified";
                case UserLogType.TestScoreVerified:
                    return "TestScore verified";
                case UserLogType.TestScoreUnverified:
                    return "TestScore unverified";
                case UserLogType.ApplicationStatusUpdated:
                    return "Student application status updated";
                case UserLogType.ReferenceApproved:
                    return "Reference approved";
                case UserLogType.ReferenceCallAgain:
                    return "Reference call again";
                case UserLogType.ReferenceDeclined:
                    return "Reference declined";
                case UserLogType.ReferencesVerified:
                    return "All references verified";
                case UserLogType.ReferencesUnverified:
                    return "All references unverified";
                case UserLogType.ReferenceLetterVerified:
                    return "School ref. letter verified";
                case UserLogType.ReferenceLetterUnverified:
                    return "School ref. letter unverified";
                case UserLogType.WhatsappInteraction:
                    return "Contacted via WhatsApp";
                case UserLogType.WechatInteraction:
                    return "Contacted via WeChat";
                case UserLogType.EmailInteraction:
                    return "Contacted via Email";
                case UserLogType.PhoneInteraction:
                    return "Contacted via Phone";
                case UserLogType.CellPhoneInteraction:
                    return "Contacted via Cell Phone";
                case UserLogType.SkypeInteraction:
                    return "Contacted via Skype";
                case UserLogType.OtherInteraction:
                    return "Contacted via Other";
                case UserLogType.FormReferenceApproved:
                    return "Form reference approved";
                case UserLogType.FormReferenceCallAgain:
                    return "Form reference call again";
                case UserLogType.FormReferenceDeclined:
                    return "Form reference declined";
                case UserLogType.ProductUpgradedToCore:
                    return "Account plan upgraded to core";
                case UserLogType.ProductUpgradedToPlus:
                    return "Account plan upgraded to plus";
                case UserLogType.ProductUpgradedToAdvance:
                    return "Account plan upgraded to advance";
                case UserLogType.TeacherDeleted:
                    return "Teacher account deleted";
                case UserLogType.SchoolDeleted:
                    return "School account deleted";
                case UserLogType.StudentDeleted:
                    return "Student account deleted";
                case UserLogType.Notes:
                    return "Notes added";
                case UserLogType.ReminderInteraction:
                    return "Reminder added";
                case UserLogType.NameSurnameUpdated:
                    return "Name or surname updated";
                case UserLogType.CurrentCityUpdated:
                    return "Current living city updated";
                case UserLogType.TeacherDescriptionUpdated:
                    return "Description updated";
                case UserLogType.PreviouslyWorkUpdated:
                    return "Previous works updated";
                case UserLogType.AchievementsUpdated:
                    return "Achievements updated";
                case UserLogType.RelatedExperienceUpdated:
                    return "Related experience updated";
                case UserLogType.EducationDegreeUpdated:
                    return "Education/Degree updated";
                case UserLogType.TeachingExperienceUpdated:
                    return "Teaching experience updated";
                case UserLogType.SchoolNameUpdated:
                    return "Student school name updated";
                case UserLogType.SchoolCityNameUpdated:
                    return "Student school city updated";
                case UserLogType.StudentNameSurnameUpdated:
                    return "Student name or surname updated";
                case UserLogType.StudentCurrentCityUpdated:
                    return "Student city updated";
                case UserLogType.StudentPaymentOverridden:
                    return "Student payment overridden";
                case UserLogType.InteractionAttemptFailed:
                    return "An interaction attempt failed";
                case UserLogType.DepositFeeChargedBack:
                    return "Deposit Fee charged back";
                case UserLogType.ApplicationFeeChargedBack:
                    return "Application Fee charged back";
                case UserLogType.NotificationStatusChanged:
                    return "Notification status changed";
                case UserLogType.AddedToTeacherPool:
                    return "Added to teacher pool";
                case UserLogType.RemovedFromTeacherPool:
                    return "Removed from teacher pool";
                case UserLogType.TeacherWireTransferApproved:
                    return "Teacher wire transfer approved";
                case UserLogType.SchoolWireTransferApproved:
                    return "School wire transfer approved";
                case UserLogType.ProductAdded:
                    return "Product added";
                case UserLogType.ProductUpdated:
                    return "Product updated";
                case UserLogType.ProductDeleted:
                    return "Product deleted";
                case UserLogType.SchoolPropertyUpdated:
                    return "School claims updated";
                default:
                    throw new ArgumentOutOfRangeException(nameof(userLogType), userLogType, null);
            }
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
