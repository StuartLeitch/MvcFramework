using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Application.Core.EventBroker;
using Application.Core.EventBroker.Interfaces;
using Application.Core.Membership;
using Application.Core.Validation;

namespace Application.Core.BaseClasses
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