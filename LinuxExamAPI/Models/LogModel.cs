using System.Text.Json.Serialization;

namespace LinuxExamAPI.Models
{
    public class LogModel
    {
        public long logId { get; set; } = DateTime.Now.Ticks;

        public int isSuccess { get; set; }
        public string errorMessage { get; set; }
        public string methodName { get; set; } = string.Empty;
        public string requestBody { get; set; } = string.Empty;
        public string responseBody { get; set; } = string.Empty;
        public DateTime apiStartTime { get; set; } = DateTime.Now;
        public DateTime apiEndTime { get; set; }
        public double totalApiTimeInS { get; set; }

        [JsonIgnore]
        public string lan { get; set; }
    }
}
