using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Infrastructure.Core.EventBroker.Interfaces;
using Infrastructure.Core.Validation;
using Ninject;

namespace Infrastructure.Core.BaseClasses
{
    public class BaseService : IUseEventBroker
    {
        private IEventBroker _eventBroker;

        public IEventBroker EventBroker
        {
            get
            {
                return this._eventBroker;
            }
        }
       
        /// <summary>
        ///   Ninject will take care of setting this.  
        ///   When Testing, set using BrokerFactoryConcrete or BrokerFactoryMock from BaseTest
        /// </summary>
        [Inject]
        public void SetEventBrokerFactory(IBrokerFactory brokerFactory)
        {
            this._eventBroker = brokerFactory.GetGeneralBroker();
        }

    }
}
