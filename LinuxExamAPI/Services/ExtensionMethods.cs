using Newtonsoft.Json;
using System.Globalization;

namespace LinuxExamAPI.Services
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Convert any object to Json string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None);
        }


        public static string ToEnUSDateString(this DateTime dateTime, string formatStr)
        {
            string datetimeStr = dateTime.ToString(formatStr, CultureInfo.InvariantCulture);
            return datetimeStr;
        }


        public static string ExceptionMessage(this Exception ex)
        {
            string errMsg = ex.InnerException?.Message ?? ex.Message;
            return errMsg;
        }

    }
}
