using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace Infrastructure.Core.Membership
{
    public interface IWrappedUser
    {
        string Email { get; set; }
        int UserId { get; set; }
        IPrincipal User { get; }
        IIdentity Identity { get; }
        bool IsInRole(string role);

    }
}