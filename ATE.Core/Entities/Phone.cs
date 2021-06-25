using System;
using ATE.Core.Args;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities
{
    public class Phone : BaseTerminal
    {
        public Phone(Contract contract) : base(contract)
        {
        }

        public override void CallTo(string number)
        {
            RaiseCallEvent(number);
        }

        public override void EndCall(Call call)
        {
            RaiseCallEndedEvent(call);
        }

        public override void HandleIncomingCall(Call call)
        {
            RaiseIncomingCallEvent(call);
        }

        public override void AcceptIncomingCall(Call call)
        {
            RaiseCallAcceptedEvent(call);
        }

        public override void RejectIncomingCall(Call call)
        {
            RaiseRejectedEvent(call);
        }
    }
}