using System.Collections.Generic;
using ATE.Entities.ATE;
using ATE.Entities.Company;
using ATE.Entities.Company.Tariff;

namespace ATE.Entities.Billings
{
    public abstract class BaseBillingSystem
    {
        public BaseCompany Company { get; set; }
        protected ICollection<Call> Calls { get; set; }
        public abstract void SubscribeToStation(BaseStation station);
        public abstract IEnumerable<Call> GetClientCalls(Client client);
        public abstract void Deposit(Client client, decimal money);
        protected abstract void WriteOff(Client client, decimal money);
        protected abstract decimal CalculateCallCost(Call call, BaseTariff tariff);
    }
}
