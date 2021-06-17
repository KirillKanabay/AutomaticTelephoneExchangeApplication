using ATE.Core.Interfaces;

namespace ATE.Core.Entities
{
    public class PhoneNumber : BaseEntity, IPhoneNumber
    {
        public PhoneNumber(Contract contract, string number)
        {

        }
        
        public string Number { get; }
        public int ContractId { get; }
        public Contract Contract { get; }
    }
}
