using System.Linq;
using Application.Core.Membership;

namespace Application.Core.TestHelpers
{
    public class BrokerTestVersion : EventBroker.EventBroker
    {
        public BrokerTestVersion() {
            this.SetFunctionToGetWrappedUser(() => new WrappedUser(1, "TestUser", "Test@TestUser.com", null));
        }
    }
}