using System.Linq;
using Infrastructure.Core.DependencyInjection;
using Infrastructure.Core.EventBroker.Interfaces;

namespace Infrastructure.Core.EventBroker
{
    public class BrokerFactory : IBrokerFactory
    {
        public IEventBroker GetGeneralBroker()
        {
            return ManuallyWiredDIContainer.Resolve<IEventBroker>();
        }
    }
}
