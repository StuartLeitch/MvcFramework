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

        public delegate void ValidationFailedHandler(object sender, ValidationFailedEventArgs validationFailedEventArgs);

        
        public event ValidationFailedHandler ValidationFailedEvent;

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
                this.RaiseValidationFailedEvent("Insufficient Privileges!");
                return false;
            }

            return true;
        }


        /// <summary>
        ///   Is publicly visible so that BaseRepository can use this to validate adapter.
        /// </summary>
        public ICollection<ValidationFailure> ValidationFailures
        {
            get
            {
                return this._validationFailures;
            }
        }

        public void AddFailure(ValidationFailure validationRule)
        {
            this._validationFailures.Add(validationRule);
        }

        public void AddFailure(string errorMessage)
        {
            this._validationFailures.Add(new ValidationFailure { Message = errorMessage });
        }

        public void AddFailure(params ValidationFailure[] validationRules)
        {
            foreach (var validationRule in validationRules)
            {
                this._validationFailures.Add(validationRule);
            }
        }

        public void AddFailure(IList<ValidationFailure> validationRules)
        {
            foreach (var validationRule in validationRules)
            {
                this._validationFailures.Add(validationRule);
            }
        }

        public void ClearFailures()
        {
            this._validationFailures.Clear();
        }

        public IList<ValidationFailure> GetFailures()
        {
            return this._validationFailures.ToList();
        }

        public virtual void RaiseSpecificValidationFailedEvent(
            string key,
            string displayMessage = null,
            ValidationFailedEventArgs.FlashLevelType severity = ValidationFailedEventArgs.FlashLevelType.Error)
        {
            // It is possible that there are no subscribers if the repository is not run from a controller implementing IUseGeneralBroker.
            if (this.ValidationFailedEvent != null)
            {
                this.ValidationFailedEvent(this, new ValidationFailedEventArgs(displayMessage, severity, key));
            }
            else
            {
                // IMPROVE: Log
            }
        }

        public virtual void RaiseValidationFailedEvent(
            string displayMessage = null,
            ValidationFailedEventArgs.FlashLevelType severity = ValidationFailedEventArgs.FlashLevelType.Error)
        {
            // It is possible that there are no subscribers if the repository is not run from a controller implementing IUseGeneralBroker.
            if (this.ValidationFailedEvent != null)
            {
                this.ValidationFailedEvent(this, new ValidationFailedEventArgs(displayMessage, severity));
            }
            else
            {
                // IMPROVE: Log
            }
        }

        public void SetFunctionToGetWrappedUser(Func<IWrappedUser> functionToGetWrappedUser)
        {
            this._functionToGetWrappedUser = functionToGetWrappedUser;
        }
    }
}