using System;

namespace TestApp.Entities
{
    public class ResponseVM
    {
        public Guid QueryId { get; set; } // reportID
        public double Percent { get; set; }
        public Guid Result { get; set; }
    }
}
