using System;

namespace TestApp.Entities
{
    public class Report
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
