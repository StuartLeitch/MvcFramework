using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Core.EventBroker
{
    /// <summary>
    /// This is the main mechanism for collecting validation failures and exceptions.
    /// </summary>
    public class ValidationFailure
    {
        /// <summary>
        /// Displays a Flash Message of type Error with displayMessage
        /// </summary>
        /// <param name="displayMessage">Message to display</param>
        public ValidationFailure(string displayMessage)
        {
            this.DisplayMessage = displayMessage;
        }

        public ValidationFailure(string displayMessage, FlashLevelType flashLevel)
        {
            this.DisplayMessage = displayMessage;
            this.FlashLevel = flashLevel;
        }

        /// <summary>
        /// Adds to the MVC ModelState matching the modelStateKey
        /// </summary>
        /// <param name="displayMessage">Message to display next to the matching field</param>
        /// <param name="modelStateKey">Field to display message next to</param>
        public ValidationFailure(string displayMessage, string modelStateKey)
        {
            this.DisplayMessage = displayMessage;
            this.ModelStateKey = modelStateKey;
        }

        /// <summary>
        /// Display 
        /// </summary>
        /// <param name="exception"></param>
        public ValidationFailure(Exception exception)
        {
            this.Exception = exception;
        }

        public ValidationFailure(Exception exception, string displayMessage)
        {
            this.DisplayMessage = displayMessage;
            this.Exception = exception;
        }

        public string DisplayMessage { get; private set; }

        /// <summary>
        /// Setting this will cause the exception message and messages for all inner exceptions to be pumped into the Flash
        /// If set, FlashLevel, DisplayUiWithoutKey, FailureType and ModelStateKey are all ignored.
        /// If DisplayMessage is set, it will display concatenated to the front of the first exception.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Determines the color of the dropdown and if it displays (none - doesn't display)
        /// </summary>
        public FlashLevelType FlashLevel { get; private set; }

        public bool HasMessage
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.DisplayMessage);
            }
        }

        /// <summary>
        /// If set, MVC ModelBinding will attempt to map this to an entity property
        /// and display the message in the property ValidationMessageFor in the view.
        /// </summary>
        public string ModelStateKey { get; private set; }
    }


    public enum FlashLevelType
    {
        None,
        Info,
        Warning,
        Error,
        /// <summary>
        /// Will force display via ModelState and not via Flash
        /// </summary>
        MvcModelState,
    }

}
