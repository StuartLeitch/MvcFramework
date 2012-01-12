using System.Linq;
using Application.Core.EventBroker.Interfaces;

namespace Application.Core.TestHelpers
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