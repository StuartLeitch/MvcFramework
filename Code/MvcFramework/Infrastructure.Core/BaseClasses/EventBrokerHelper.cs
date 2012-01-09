using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Infrastructure.Core.EventBroker;
using Infrastructure.Core.EventBroker.Interfaces;
using Infrastructure.Core.Membership;
using Infrastructure.Core.Validation;

namespace Infrastructure.Core.BaseClasses
{
    public static class EventBrokerHelper
    {
        [DebuggerStepThrough]
        public static void AddFailure(this IUseEventBroker implementsEventBroker, string errorMessage)
        {
            implementsEventBroker.EventBroker.AddFailure();
        }


        [DebuggerStepThrough]
        public static void AddFailure(this IUseEventBroker implementsEventBroker, ValidationFailure validationRule)
        {
            implementsEventBroker.EventBroker.AddFailure(validationRule);
        }

        [DebuggerStepThrough]
        public static void AddFailure(this IUseEventBroker implementsEventBroker, params ValidationFailure[] validationRules)
        {
            implementsEventBroker.EventBroker.AddFailure(validationRules);
        }

        [DebuggerStepThrough]
        public static void AddFailure(this IUseEventBroker implementsEventBroker, IList<ValidationFailure> validationRules)
        {
            implementsEventBroker.EventBroker.AddFailure(validationRules);
        }


        [DebuggerStepThrough]
        public static IWrappedUser GetUser(this IUseEventBroker implementsEventBroker)
        {
            return implementsEventBroker.EventBroker.WrappedUser;
        }
    }
}