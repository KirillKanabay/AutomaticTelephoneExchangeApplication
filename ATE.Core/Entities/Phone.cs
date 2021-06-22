using ATE.Core.Interfaces;

namespace ATE.Core.Entities
{
    public class Phone : ITerminal
    {
        private readonly Contract _contract;
        public Port Port { get; private set; }
        public Phone(Contract contract)
        {
            _contract = contract;
        }
        
        public void ConnectToPort()
        {
            throw new System.NotImplementedException();
        }

        public void Call()
        {
            
        }
    }
}