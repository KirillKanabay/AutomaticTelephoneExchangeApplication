using System;
using System.Collections.Generic;
using System.Linq;
using ATE.Args;
using ATE.Entities.ATE;
using ATE.Entities.Company;
using ATE.Entities.Company.Tariff;
using ATE.Enums;

namespace ATE.Entities.Billings
{
    public class BillingSystem : BaseBillingSystem
    {
        public BillingSystem()
        {
            Calls = new List<Call>();
        }
        
        public override void SubscribeToStation(BaseStation station)
        {
            station.CallEndedEvent += OnCallEnded;
        }
        
        private void OnTerminalCall(object sender, CallArgs e)
        {
            // var acc = _billingAccounts.FirstOrDefault(a => a.Contract.PhoneNumber == e.FromNumber);
            // if (acc?.Balance < acc?.Tariff.PricePerMinuteCall)
            // {
            //     throw new ArgumentException("Недостаточно средств для совершения вызова");
            // }
            
        }
        
        private void OnCallEnded(object sender, CallArgs e)
        {
            var call = e.Call;
            var sourceClient = Company.GetClientByPhoneNumber(call.FromNumber);

            Calls.Add(e.Call);

            decimal callCost = CalculateCallCost(call, sourceClient.Contract.Tariff);
            WriteOff(sourceClient, callCost);
        }
        
        public override IEnumerable<Call> GetClientCalls(Client client)
        {
            string clientPhoneNumber = client.PhoneNumber;

            return Calls.Where(c => c.FromNumber == clientPhoneNumber || c.TargetNumber == clientPhoneNumber);
        }

        public override void Deposit(Client client, decimal money)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            
            if (money <= 0)
            {
                throw new ArgumentException("Сумма пополнения счета не может быть меньше или равно нулю");
            }

            client.Balance += money;
        }

        protected override void WriteOff(Client client, decimal money)
        {
            if (money < 0)
            {
                throw new ArgumentException("Сумма списания счета не может быть меньше нуля");
            }
            
            client.Balance -= money;
        }

        protected override decimal CalculateCallCost(Call call, BaseTariff tariff)
        {
            return Convert.ToDecimal(call.DurationInMinutes) * tariff.PricePerMinuteCall;
        }
    }
}