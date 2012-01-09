using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Infrastructure.Core.EventBroker.Interfaces;
using Infrastructure.Core.Membership;
using Infrastructure.Core.Validation;

namespace Infrastructure.Core.EventBroker
{
    /// <summary>
    ///   This is a class that is instantiated by Ninject with a lifetime scoped to the request.
    ///   This means that classes throughout the application can access this class to communicate while remaining decoupled.
    /// </summary>
    public class EventBroker : IEventBroker
    {
        /// <summary>
        ///   This should be the only instance of a validator per request.
        /// </summary>
        private readonly ICollection<ValidationFailure> _validationFailures;

        private Func<IWrappedUser> _functionToGetWrappedUser;

        public EventBroker()
        {
            this._validationFailures = new Collection<ValidationFailure>();
        }

        public bool IsValid
        {
            get
            {
                return !this._validationFailures.Any();
            }
        }

        /// <summary>
        ///   Gets the user that has been setup in the BaseSecureController
        /// </summary>
        public IWrappedUser WrappedUser
        {
            get
            {
                try
                {
                    return this._functionToGetWrappedUser.Invoke();
                }
                catch (Exception ex)
                {
                    throw new NullReferenceException(
                        "Failed to get user from EventBroker.  Most likely reason is " +
                        "that the controller you are using does not inherit form BaseSecureController.  ", ex);
                }
            }
        }

        /// <summary>
        /// Gets the UserId for the currently logged in user, but defaults to 
        /// the anonymous user if the user is not logged in.
        /// Allows anonymous users to perform Create, Read on database tables that require UserId (such as CreateId).
        /// This is what the repository should call to get the UserId
        /// </summary>
        public int DatabaseCreateUpdateDeleteUserId
        {
            get
            {
                if (this._functionToGetWrappedUser != null)
                {
                    return this._functionToGetWrappedUser.Invoke().UserId;
                }

                return Properties.Settings.Default.AnonomousUserId;
            }
        }

        /// <summary>
        ///   Checks if user is in role.  If not, creates a Validation Failure and bubbles so it will flash message on site.
        /// </summary>
        [DebuggerStepThrough]
        public bool UserIsInRoleBubbleFailure(string role)
        {
            if (!this.WrappedUser.IsInRole(role))
            {
                this.AddFailure("You need to be in the user role " + role + " to access this functionality");
                return false;
            }

            return true;
        }


        public ICollection<ValidationFailure> ValidationFailures
        {
            get
            {
                return this._validationFailures;
            }
        }

        public void AddFailure(ValidationFailure validationFailure)
        {
            this._validationFailures.Add(validationFailure);
        }

        public void AddFailure(string displayMessage)
        {
            this._validationFailures.Add(new ValidationFailure(displayMessage));
        }

        public void AddFailure(params ValidationFailure[] validationFailures)
        {
            foreach (var validationRule in validationFailures)
            {
                this._validationFailures.Add(validationRule);
            }
        }

        public void AddFailure(IList<ValidationFailure> validationFailures)
        {
            foreach (var validationRule in validationFailures)
            {
                this._validationFailures.Add(validationRule);
            }
        }

        public void ClearFailures()
        {
            this._validationFailures.Clear();
        }

        public void SetFunctionToGetWrappedUser(Func<IWrappedUser> functionToGetWrappedUser)
        {
            this._functionToGetWrappedUser = functionToGetWrappedUser;
        }
    }
}