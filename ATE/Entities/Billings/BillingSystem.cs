using System;
using System.Collections.Generic;
using System.Linq;
using ATE.Args;
using ATE.Constants;
using ATE.Entities.ATE;
using ATE.Entities.Company;
using ATE.Entities.Company.Tariff;

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
            station.CallStartedEvent += OnTerminalCall;
            station.CallEndedEvent += OnCallEnded;
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
        protected override decimal CalculateCallPrice(double duration, BaseTariff tariff)
        {
            decimal price = 0;

            if (duration <= 1)
            {
                price = tariff.PricePerMinuteCall;
            }
            else
            {
                price = Convert.ToDecimal(duration) * tariff.PricePerMinuteCall;
            }

            return price;
        }
        private void OnTerminalCall(object sender, CallArgs e)
        {
            var sourceClient = Company.GetClientByPhoneNumber(e.FromNumber);
            var callCostInOneMinute = CalculateCallPrice(DataConstants.OneMinute, sourceClient.Contract.Tariff);

            if (sourceClient.Balance < callCostInOneMinute)
            {
                OnCallCanceledEvent(this, new CallCanceledArgs()
                {
                    Message = DataConstants.InsufficientFundsError,
                    SourcePhoneNumber = sourceClient.PhoneNumber,
                });
            }
            else
            {
                OnCallAllowedEvent(sender, e);
            }
        }
        private void OnCallEnded(object sender, CallArgs e)
        {
            var call = new Call()
            {
                FromNumber = e.FromNumber,
                TargetNumber = e.TargetNumber,
                Date = e.Date,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
            };

            var sourceClient = Company.GetClientByPhoneNumber(call.FromNumber);

            Calls.Add(call);

            decimal callCost = CalculateCallPrice(call.DurationInMinutes, sourceClient.Contract.Tariff);
            WriteOff(sourceClient, callCost);
        }
    }
}