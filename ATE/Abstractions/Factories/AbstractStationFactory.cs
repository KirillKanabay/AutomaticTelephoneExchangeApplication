using ATE.Abstractions.Domain.Station;
using ATE.Domain.Station;

namespace ATE.Abstractions.Factories
{
    public abstract class AbstractStationFactory
    {
        public abstract BaseStation CreateStation();
    }
}
