﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace Application.Core.Membership
{
    public interface IWrappedUser : IPrincipal
    {
        string Email { get; set; }
        int UserId { get; set; }
        IPrincipal User { get; }
    }
}