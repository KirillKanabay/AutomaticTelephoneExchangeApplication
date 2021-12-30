using System;
using System.Collections.Generic;
using System.Linq;
using ATE.Args;
using ATE.Constants;
using ATE.Entities.ATE;
using ATE.Entities.Calls;
using ATE.Entities.Company.Tariff;
using ATE.Entities.Users;
using ATE.Mapper;

namespace ATE.Entities.Billings
{
    public class BillingSystem : BaseBillingSystem
    {
        public BillingSystem()
        {
            Calls = new List<CallInformation>();
        }
        public override void SubscribeToStation(BaseStation station)
        {
            station.CallStartedEvent += OnTerminalCall;
            station.CallEndedEvent += OnCallEnded;
        }
        public override IEnumerable<CallInformation> GetClientCalls(Client client)
        {
           return Calls.Where(c => c.Client.Equals(client));
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
            var sourceClient = Company.GetClientByPhoneNumber(e.SourceNumber);
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
            HandleCall(e);
        }
        protected void HandleCall(CallArgs e)
        {
            var sourceClient = Company.GetClientByPhoneNumber(e.SourceNumber);
            var targetClient = Company.GetClientByPhoneNumber(e.TargetNumber);

            var sourceClientCallInformation = CallMapper.MapToCallInformation(e, sourceClient);
            var targetClientCallInformation = CallMapper.MapToCallInformation(e, targetClient);
            
            decimal sourceClientCallCost = CalculateCallPrice(sourceClientCallInformation.DurationInMinutes,
                sourceClient.Contract.Tariff);
            
            sourceClientCallInformation.Price = sourceClientCallCost;

            Calls.Add(sourceClientCallInformation);
            Calls.Add(targetClientCallInformation);
        }
    }
}