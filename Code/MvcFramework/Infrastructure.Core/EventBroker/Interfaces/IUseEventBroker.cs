using System.Linq;
using Ninject;

namespace Infrastructure.Core.EventBroker.Interfaces
{
    public interface IUseEventBroker
    {
        /// <summary>
        ///   Ninject will take care of setting this.
        ///   When Testing, set using BrokerFactoryConcrete or BrokerFactoryMock from BaseTest
        /// </summary>
        [Inject]
        void SetEventBrokerFactory(IBrokerFactory brokerFactory);

        IEventBroker EventBroker { get; }
    }
}