using ATE.Core.Entities;
using ATE.Entities;
using ATE.Entities.Users;

namespace ATE.Core.Interfaces
{
    public interface IContract
    {
        string PhoneNumber { get; }
        Tariff Tariff { get; }
        User User { get; }
    }
}