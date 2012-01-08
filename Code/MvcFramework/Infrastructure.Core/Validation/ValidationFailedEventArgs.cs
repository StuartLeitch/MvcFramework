using System.Linq;
using System;

namespace Infrastructure.Core.Validation
{
    /// <summary>
    /// Used by the EventBroker to communicate failures to the UI layer.
    /// It causes the controller to ask for ValidationFailures and bind these to the ModelState
    /// </summary>
    public class ValidationFailedEventArgs : EventArgs
    {
        public ValidationFailedEventArgs(string message = null, FlashLevelType severity = FlashLevelType.Error, string key = null)
        {
            this.DisplayMessage = message;
            this.Key = key;
            this.FlashLevel = severity;
        }

        public enum FlashLevelType
        {
            None, 
            Info, 
            Warning, 
            Error
        }

        /// <summary>
        /// This message gets displayed in the dropdown flash in the view.
        /// </summary>
        public string DisplayMessage { get; set; }

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
        ///   Field to which the validation gets applied in MVC
        /// </summary>
        public string Key { get; set; }
    }
}