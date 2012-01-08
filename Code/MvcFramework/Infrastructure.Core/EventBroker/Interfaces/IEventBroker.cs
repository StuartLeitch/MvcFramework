using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Core.Membership;
using Infrastructure.Core.Validation;

namespace Infrastructure.Core.EventBroker.Interfaces
{
    public interface IEventBroker
    {
        IWrappedUser WrappedUser { get; }
        bool IsValid { get; }

        /// <summary>
        /// Allows anonymous users to perform Create, Read on database tables that require UserId (such as CreateId).
        /// </summary>
        int DatabaseCreateUpdateDeleteUserId { get; }

        event EventBroker.ValidationFailedHandler ValidationFailedEvent;

        /// <summary>
        /// Sends message to the controller which displays in view
        /// </summary>
        void RaiseValidationFailedEvent(string displayMessage = null, 
                    ValidationFailedEventArgs.FlashLevelType severity = ValidationFailedEventArgs.FlashLevelType.Error);

        void SetFunctionToGetWrappedUser(Func<IWrappedUser> functionToGetWrappedUser);

        void AddFailure(ValidationFailure validationRule);

        void AddFailure(params ValidationFailure[] validationRules);

        void AddFailure(IList<ValidationFailure> validationRules);

        void AddFailure(string errorMessage);

        IList<ValidationFailure> GetFailures();

        void ClearFailures();

        void RaiseSpecificValidationFailedEvent(
            string key, string displayMessage, ValidationFailedEventArgs.FlashLevelType severity = ValidationFailedEventArgs.FlashLevelType.Error);
    }
}