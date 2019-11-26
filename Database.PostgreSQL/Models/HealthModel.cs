using System;
using System.Collections.Generic;
using System.Text;

namespace Database.PostgreSQL.Models
{
    public class HealthModel
    {
        public int StatusCode { get; set; }
        public int ElapsedMilliseconds { get; set; }
        public DateTime Timestamp { get; set; }
        public string Url { get; set; }
        public bool Success { get; set; }
    }
}
