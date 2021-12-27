using ATE.Core.Entities;
using ATE.Entities;
using ATE.Entities.Company.Tariff;
using ATE.Entities.Users;

namespace ATE.Core.Interfaces
{
    public interface IContract
    {
        string PhoneNumber { get; }
        EasySayTariff Tariff { get; }
        User User { get; }
    }
}