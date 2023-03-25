using UnityEngine;

namespace PerelesoqTest.Services.Logging
{
    public class LoggingService : ILoggingService
    {
        public void LogMessage(string message, object sender = null) => 
            Debug.Log(GetString(message, sender));

        public void LogWarning(string message, object sender = null) => 
            Debug.LogWarning(GetString(message, sender));

        public void LogError(string message, object sender = null) => 
            Debug.LogError(GetString(message, sender));

        private string GetString(string message, object sender = null) =>
            $"<b><i><color=#e38d46>{sender ?? nameof(LoggingService)}: </color></i></b> {message}";
    }
}