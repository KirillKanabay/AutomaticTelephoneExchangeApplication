using System;

namespace ATE.Core.Entities.ATE
{
    public readonly struct Call
    {
        public readonly Guid Id;
        public readonly string FromNumber;
        public readonly string TargetNumber;

        public Call(string fromNumber, string targetNumber)
        {
            Id = Guid.NewGuid();
            FromNumber = fromNumber;
            TargetNumber = targetNumber;
        }
    }
}