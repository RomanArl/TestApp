using System;

namespace TestApp.Entities
{
    public class RequestVM
    {

        public Guid UserId { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
