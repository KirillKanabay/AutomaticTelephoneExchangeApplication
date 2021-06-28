using System;
using ATE.Core.Args;
using ATE.Core.Entities.ATE;
using ATE.Core.Enums;
using ATE.Core.Interfaces.Billings;

namespace ATE.Core.Entities.Billings
{
    public struct CallInformation
    {
        public string ClientPhoneNumber { get; set; }
        public string DestinationPhoneNumber { get; set; }
        public decimal Price { get; set; }
        public double Duration { get; set; }
        public DateTime CallDate { get; set; }
        public CallType CallType { get; set; }
    }
}