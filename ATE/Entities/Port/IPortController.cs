using ATE.Interfaces.ATE;

namespace ATE.Entities.Port
{
    public interface IPortController
    {
        BasePort GetAvailablePort();
        BasePort GetByPhoneNumber(string phoneNumber);
    }
}
