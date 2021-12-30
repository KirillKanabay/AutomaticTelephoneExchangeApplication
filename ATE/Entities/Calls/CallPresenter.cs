using System;
using ATE.Abstractions.Factories;
using ATE.Entities.Billings;
using ATE.Entities.Users;
using ATE.Enums;
using ATE.Helpers;

namespace ATE.Entities.Calls
{
    public class CallPresenter : ICallPresenter
    {
        private readonly AbstractCallInformationComparerFactory _callComparerFactory;
        public CallPresenter(AbstractCallInformationComparerFactory callComparerFactory)
        {
            _callComparerFactory = callComparerFactory;
        }

        public void Present(BaseBillingSystem billingSystem, Client client, CallSortType sort = CallSortType.Date)
        {
            var comparer = _callComparerFactory.Create(sort);
            var calls = billingSystem.GetClientCalls(client);
            foreach (var call in calls)
            {
                string typeOfCall = call.Type == CallType.Incoming ? "Incoming" : "Outgoing";
                ConsoleColor consoleColor = call.Type == CallType.Incoming ? ConsoleColor.Blue : ConsoleColor.Green;
                ConsoleEx.WriteLineWithColor($"Date: {call.Date:g}; " +
                                             $"Duration (in minutes): {call.DurationInMinutes:F2} m.; " +
                                             $"Call price: {call.Price:C2}; " + 
                                             $"Destination phone number: {call.DestinationPhoneNumber}; " +
                                             $"Type of call: {typeOfCall}", consoleColor);
            }
        }
    }
}