using ATE.Abstractions.Domain.Port;
using ATE.Abstractions.Domain.Station;
using ATE.Abstractions.Factories;
using ATE.Constants;
using ATE.Domain.Port;
using ATE.Domain.Station;

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
