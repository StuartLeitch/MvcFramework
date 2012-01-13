using System.Linq;

namespace Application.Membership {

    public enum UserStatus : byte {
        Approved = 1,
        Locked = 2,
        Expire = 3
    }

}
