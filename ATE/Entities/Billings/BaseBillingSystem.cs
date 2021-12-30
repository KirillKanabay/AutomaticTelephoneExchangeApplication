using System;
using System.Collections.Generic;
using ATE.Args;
using ATE.Entities.ATE;
using ATE.Entities.Calls;
using ATE.Entities.Company;
using ATE.Entities.Company.Tariff;
using ATE.Entities.Users;

namespace ATE.Entities.Billings
{
    public abstract class BaseBillingSystem
    {
        public event EventHandler<CallArgs> CallAllowedEvent;
        public event EventHandler<CallCanceledArgs> CallCanceledEvent; 
        public BaseCompany Company { get; set; }
        protected ICollection<CallInformation> Calls { get; set; }
        public abstract void SubscribeToStation(BaseStation station);
        public abstract IEnumerable<CallInformation> GetClientCalls(Client client);
        public abstract void Deposit(Client client, decimal money);
        protected abstract void WriteOff(Client client, decimal money);
        protected abstract decimal CalculateCallPrice(double duration, BaseTariff tariff);

        protected virtual void OnCallAllowedEvent(object sender, CallArgs e)
        {
            CallAllowedEvent?.Invoke(sender, e);
        }
        protected virtual void OnCallCanceledEvent(object sender, CallCanceledArgs e)
        {
            CallCanceledEvent?.Invoke(sender, e);
        }
    }
}
