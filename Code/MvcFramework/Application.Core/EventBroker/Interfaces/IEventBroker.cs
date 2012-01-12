using System;
using System.Collections.Generic;
using System.Linq;
using Application.Core.Membership;
using Application.Core.Validation;

namespace Application.Core.EventBroker.Interfaces
{
    public interface IEventBroker
    {
        IWrappedUser WrappedUser { get; }
        bool IsValid { get; }

        /// <summary>
        /// Allows anonymous users to perform Create, Read on database tables that require UserId (such as CreateId).
        /// </summary>
        int DatabaseCreateUpdateDeleteUserId { get; }

        ICollection<ValidationFailure> ValidationFailures { get; }

        void SetFunctionToGetWrappedUser(Func<IWrappedUser> functionToGetWrappedUser);

        void AddFailure(ValidationFailure validationFailure);

        void AddFailure(params ValidationFailure[] validationFailures);

        void AddFailure(IList<ValidationFailure> validationFailures);

        void AddFailure(string displayMessage);

        void ClearFailures();
    }
}