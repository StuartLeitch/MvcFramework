using System.Linq;
using System.Security.Principal;
using Infrastructure.Core.Membership;
using Infrastructure.Core.Validation;
using System;

namespace Infrastructure.Core.TestHelpers
{
    public class BrokerTestVersion : EventBroker.EventBroker
    {
        public BrokerTestVersion()
        {
            this.SetFunctionToGetWrappedUser(() => new WrappedUser(1, "TestUser", "Test@TestUser.com", null));
        }
        public override void RaiseValidationFailedEvent(
            string displayMessage = null, ValidationFailedEventArgs.FlashLevelType severity = ValidationFailedEventArgs.FlashLevelType.Error)
        {
            this.ValidationEventHasFired = true;

            Console.WriteLine("RaiseValidationFailedEvent fired");

            foreach (var validationRule in this.GetFailures().Where(x => !string.IsNullOrWhiteSpace(x.Message)))
            {
                Console.WriteLine(validationRule.Message);
            }

            TestValidationHelper.Write(this.GetFailures());

        }

        public bool ValidationEventHasFired { get; set; }
    }
}