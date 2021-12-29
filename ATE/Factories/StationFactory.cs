using ATE.Abstractions.Factories;
using ATE.Constants;
using ATE.Entities.ATE;
using ATE.Entities.Port;

namespace ATE.Factories
{
    public class StationFactory : AbstractStationFactory
    {
        public override BaseStation CreateStation()
        {
            IPortController portController = new PortController(DataConstants.DefaultPortCount);
            BaseStation station = new Station(portController);

            return station;
        }
    }
}
