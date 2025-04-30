
using LinuxExamAPI.StaticClass;
using System.Globalization;
using System.Text;



namespace Domain.Helpers
{
    public class TextLogWriter
    {
        #region Global Parameters
        private static readonly ReaderWriterLockSlim locker = new();
        //private static readonly Mutex mutexLock = new Mutex();

        private const int NumberOfRetries = 3;
        // total time in milisecond
        private const int DelayOnRetry = 250;

        #endregion

        #region Write log into Text file

        public static void WriteApiLogToFile(StringBuilder logText, string path)
        {
            string logFolder = Path.Combine(AppSettingsKeys.TextLogPath, "ApiLogs", path);
            Directory.CreateDirectory(logFolder);
            string datetimeStr = DateTime.Now.ToString("ddMMyyyyHH", CultureInfo.InvariantCulture);

            string fileName = datetimeStr + ".txt";
            string filePath = Path.Combine(logFolder, fileName);

            if (!File.Exists(filePath))
                File.Create(filePath).Dispose();

            for (int i = 1; i <= NumberOfRetries; i++)
            {
                locker.EnterWriteLock();
                try
                {
                    using FileStream fs = new(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    using StreamWriter writer = new(fs);
                    writer.WriteLine(logText.ToString());
                    writer.Flush();
                    writer.Dispose();
                    fs.Close();

                    break;
                }
                catch (IOException) when (i <= NumberOfRetries)
                {
                    Thread.Sleep(DelayOnRetry);
                }
                finally
                {
                    locker.ExitWriteLock();
                }
            }
        }


        public static void WriteApiTraceLogToFile(StringBuilder logText, string path)
        {
            string logFolder = Path.Combine(AppSettingsKeys.TextLogPath, "ApiLogs", path);
            Directory.CreateDirectory(logFolder);

            string datetimeStr = DateTime.Now.ToString("ddMMyyyyHH", CultureInfo.InvariantCulture);

            string fileName = datetimeStr + ".txt";
            string filePath = Path.Combine(logFolder, fileName);

            if (!File.Exists(filePath))
                File.Create(filePath).Dispose();

            for (int i = 1; i <= NumberOfRetries; i++)
            {
                locker.EnterWriteLock();
                try
                {
                    using FileStream fs = new(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    using StreamWriter writer = new(fs);
                    writer.WriteLine(logText.ToString());
                    writer.Flush();
                    writer.Dispose();
                    fs.Close();

                    break;
                }
                catch (IOException) when (i <= NumberOfRetries)
                {
                    Thread.Sleep(DelayOnRetry);
                }
                finally
                {
                    locker.ExitWriteLock();
                }
            }
        }


        public static void WriteLogFromLogWriteError(string logText)
        {
            string logFolder = Path.Combine(AppSettingsKeys.TextLogPath, "TextWriteErrorLogs");
            Directory.CreateDirectory(logFolder);

            string datetimeStr = DateTime.Now.ToString("ddMMyyyyHH", CultureInfo.InvariantCulture);

            string fileName = datetimeStr + ".txt";
            string filePath = Path.Combine(logFolder, fileName);

            if (!File.Exists(filePath))
                File.Create(filePath).Dispose();

            for (int i = 1; i <= NumberOfRetries; i++)
            {
                locker.EnterWriteLock();
                try
                {
                    using FileStream fs = new(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    using StreamWriter writer = new(fs);
                    writer.WriteLine(logText);
                    writer.Flush();
                    writer.Dispose();
                    fs.Close();

                    break;
                }
                catch (IOException) when (i <= NumberOfRetries)
                {
                    Thread.Sleep(DelayOnRetry);
                }
                finally
                {
                    locker.ExitWriteLock();
                }
            }
        }


        public static void WriteErrorLog(string logText)
        {
            string logFolder = Path.Combine(AppSettingsKeys.TextLogPath, "ErrorLogs");
            Directory.CreateDirectory(logFolder);

            string datetimeStr = DateTime.Now.ToString("ddMMyyyyHH", CultureInfo.InvariantCulture);

            string fileName = datetimeStr + ".txt";
            string filePath = Path.Combine(logFolder, fileName);

            if (!File.Exists(filePath))
                File.Create(filePath).Dispose();

            for (int i = 1; i <= NumberOfRetries; i++)
            {
                locker.EnterWriteLock();
                try
                {
                    using FileStream fs = new(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    using StreamWriter writer = new(fs);
                    writer.WriteLine(logText);
                    writer.Flush();
                    writer.Dispose();
                    fs.Close();

                    break;
                }
                catch (IOException) when (i <= NumberOfRetries)
                {
                    Thread.Sleep(DelayOnRetry);
                }
                finally
                {
                    locker.ExitWriteLock();
                }
            }
        }


        #endregion

    }
}