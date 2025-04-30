namespace LinuxExamAPI.Models
{
    public class ApiSummaryLog
    {
        public int isSuccess { get; set; }
        public string methodName { get; set; }
        public double totalApiTimeInS { get; set; }
        public string errorMessage { get; set; }
        public DateTime apiStartTime { get; set; }
        public DateTime apiEndTime { get; set; }
        public long logId { get; set; }

        public ApiSummaryLog(LogModel log)
        {

            isSuccess = log.isSuccess;
            methodName = log.methodName;
            errorMessage = log.errorMessage;
            apiStartTime = log.apiStartTime;
            apiEndTime = log.apiEndTime;
            totalApiTimeInS = (log.apiEndTime - log.apiStartTime).TotalSeconds;
            logId = log.logId;
        }
    }
}
