
using LinuxExamAPI.Models;
using LinuxExamAPI.Services;
using System.Text;

namespace Domain.Helpers
{
    public class LoggerService
    {
        public LoggerService() { }

        public static void WriteApiLogInText(LogModel log)
        {
            Task.Factory.StartNew(() =>
            {
                SaveApiLogInTxt(log);
            });
        }

        public static void ExceptionFromTextLogWrite(string mainLogStr, Exception ex, string methodName)
        {
            try
            {
                string sb = string.Empty;

                sb = new
                {
                    logMethodName = methodName,
                    errorMessage = ExMsgSubString(ex, "", 256),
                    mainLogStr,
                    errorDetails = ex.StackTrace,
                    createdDate = DateTime.Now,
                    logId = DateTime.Now.Ticks
                }.ToJsonString() + ",";

                TextLogWriter.WriteLogFromLogWriteError(sb);
            }
            catch (Exception)
            {
            }
        }


        #region Private Methods

        private static void SaveApiLogInTxt(LogModel log)
        {
            StringBuilder summarySb = new();
            StringBuilder detailsSb = new();

            try
            {
                ApiSummaryLog summary = new(log);
                summarySb.Append(summary.ToJsonString() + ",");

                TextLogWriter.WriteApiLogToFile(summarySb, "SummaryLogs");
            }
            catch (Exception ex)
            {
                ExceptionFromTextLogWrite(log.ToJsonString(), ex, "SaveApiLogInTxt");
            }
        }

        private static string ExMsgSubString(Exception ex, string methodName, int length = 1000)
        {
            string retString = "";
            try
            {
                string errMsg = "";
                if (ex.InnerException != null)
                {
                    if (string.IsNullOrEmpty(methodName))
                        errMsg = ex.InnerException.Message;
                    else
                        errMsg = methodName + " || " + ex.InnerException.Message;

                    retString = errMsg.Substring(0, Math.Min(errMsg.Length, length));
                }
                else
                {
                    if (string.IsNullOrEmpty(methodName))
                        errMsg = ex.Message;
                    else
                        errMsg = methodName + " || " + ex.Message;

                    retString = errMsg.Substring(0, Math.Min(errMsg.Length, length));
                }
            }
            catch
            { }

            return retString;
        }

        #endregion

    }
}