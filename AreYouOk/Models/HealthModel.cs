using System;

namespace AreYouOk.Models
{
    public class HealthModel
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public int ElapsedMilliseconds { get; set; }
        public DateTime Timestamp { get; set; }
        public string Url { get; set; }
    }
}