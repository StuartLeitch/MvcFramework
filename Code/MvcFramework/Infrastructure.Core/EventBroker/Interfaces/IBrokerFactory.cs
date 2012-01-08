using System.Linq;

namespace Infrastructure.Core.EventBroker.Interfaces
{
    public interface IBrokerFactory
    {
        IEventBroker GetGeneralBroker();
    }
}