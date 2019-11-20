using System;

namespace AreYouOk.Database.Models
{
    public class HealthModel
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public int ElapsedMilliseconds { get; set; }
        public DateTime Timestamp { get; set; }
    }
}