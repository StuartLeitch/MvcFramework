﻿using System.Diagnostics;
using System.Linq;
using System.Security.Principal;

namespace Application.Core.Membership
{
    [DebuggerStepThrough]
    public class WrappedUser : IWrappedUser
    {
        /// <summary>
        /// Use this constructor when passing in an existing framework user.
        /// </summary>
        public WrappedUser(IPrincipal principal)
        {
            this.User = principal;
        }

        public WrappedUser(int userId, string friendlyName, string email, string[] roles)
        {
            this.Email = email;
            this.UserId = userId;
            this.FriendlyName = friendlyName;

            // Use userId for name within GenericIdentity as it will always be unique.
            this.User = new GenericPrincipal(new GenericIdentity(userId.ToString()), roles);
        }

        public string Email { get; set; }

        /// <summary>
        /// Friendly Name
        /// </summary>
        public string FriendlyName { get; set; }

        public IPrincipal User { get; private set; }

        public IIdentity Identity
        {
            get
            {
                return User.Identity;
            }
        }

        public bool IsInRole(string role)
        {
            return this.User.IsInRole(role);
        }

        public int UserId { get; set; }
    }
}