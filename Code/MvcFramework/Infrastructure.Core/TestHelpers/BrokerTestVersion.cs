using System.Linq;
using Infrastructure.Core.Membership;

namespace Infrastructure.Core.TestHelpers
{
    public class BrokerTestVersion : EventBroker.EventBroker
    {
        public BrokerTestVersion() {
            this.SetFunctionToGetWrappedUser(() => new WrappedUser(1, "TestUser", "Test@TestUser.com", null));
        }
    }
}