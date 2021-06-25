using ATE.Core.Entities;

namespace ATE.Core.Interfaces
{
    public interface IContract
    {
        string PhoneNumber { get; }
        Tariff Tariff { get; }
        User User { get; }
    }
}