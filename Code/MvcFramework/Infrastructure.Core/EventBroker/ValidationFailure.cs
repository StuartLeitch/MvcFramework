using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Core.EventBroker
{
    /// <summary>
    /// This is the main mechanism for collecting validation failures and exceptions.
    /// </summary>
    public class ValidationFailure
    {
        public ValidationFailure()
        {
            this.DisplayUiWithoutKey = false;
        }
        public ErrorType ErrorType { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public bool DisplayUiWithoutKey { get; set; }

        /// <summary>
        /// If set, MVC ModelBinding will attempt to map this to an entity property
        /// and display the message in the property ValidationMessageFor in the view.
        /// </summary>
        public string ModelStateKey { get; set; }
    }

    public enum ErrorType
    {
        Concurrency,
        EntityFrameworkValidation,
        Unexpected,
    }
}
