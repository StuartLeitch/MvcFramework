using System.Linq;

namespace Application.Core.EventBroker.Interfaces
{
    public interface IBrokerFactory
    {
        IEventBroker GetGeneralBroker();
    }
}