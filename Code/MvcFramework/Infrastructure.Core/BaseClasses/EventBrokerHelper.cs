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

        /// <summary>
        ///   Publishes Broken Rules and an option DisplayMessage to subscribers (typically BaseController) via EventBroker
        /// </summary>
        /// <param name = "displayMessage">Message that will Flash in the View</param>
        [DebuggerStepThrough]
        public static void RaiseValidationFailedEvent(this IUseEventBroker implementsEventBroker, string displayMessage = null)
        {
            implementsEventBroker.EventBroker.RaiseValidationFailedEvent(displayMessage);
        }

        /// <summary>
        ///   Publishes Broken Rules and an optional DisplayMessage to subscribers (typically BaseController) via GeneralBroker
        /// </summary>
        /// <param name = "displayMessage">Message that will Flash in the View</param>
        /// <param name="severity">Determines the color of message flashed to the user</param>
        public static void RaiseValidationFailedEvent(this IUseEventBroker implementsEventBroker, string displayMessage = null, ValidationFailedEventArgs.FlashLevelType severity = ValidationFailedEventArgs.FlashLevelType.Error)
        {
            implementsEventBroker.EventBroker.RaiseValidationFailedEvent(displayMessage, severity);
        }

        /// <summary>
        ///   Publishes Broken Rule as a specific failure in the ModelStateDictionary to Controller (BaseController) via GeneralBroker
        /// </summary>
        /// <param name="key">Name of the field to display the validation failure against</param>
        /// <param name = "displayMessage">Message that will Flash in the View</param>
        public static void RaiseSpecificValidationFailedEvent(this IUseEventBroker implementsEventBroker, string key, string displayMessage = null)
        {
            implementsEventBroker.EventBroker.RaiseSpecificValidationFailedEvent(key, displayMessage);
        }

    }
}