using System.Linq;
using Infrastructure.Core.EventBroker.Interfaces;

namespace Infrastructure.Core.TestHelpers
{
    public class BrokerTestFactory : IBrokerFactory
    {
        private readonly IEventBroker _generalBrokerTestVerion = new BrokerTestVersion();

        public IEventBroker GetGeneralBroker()
        {
            return this._generalBrokerTestVerion;
        }
    }
}