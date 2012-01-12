using System.Linq;
using Application.Core.DependencyInjection;
using Application.Core.EventBroker.Interfaces;

namespace Application.Core.EventBroker
{
    public class BrokerFactory : IBrokerFactory
    {
        public IEventBroker GetGeneralBroker()
        {
            return ManuallyWiredDIContainer.Resolve<IEventBroker>();
        }
    }
}
