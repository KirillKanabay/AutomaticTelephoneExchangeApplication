namespace ATE.Abstractions.Domain.Port
{
    public interface IPortController
    {
        BasePort GetAvailablePort();
        BasePort GetByPhoneNumber(string phoneNumber);
    }
}
