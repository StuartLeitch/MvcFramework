using System;
using System.Linq;

namespace Application.Membership
{
    [Serializable]
    public class MembershipUser : System.Web.Security.MembershipUser
    {
        public MembershipUser(string providerName,
                              string name,
                              object providerUserKey,
                              string email,
                              string passwordQuestion,
                              string comment,
                              bool isApproved,
                              bool isLockedOut,
                              DateTime creationDate,
                              DateTime lastLoginDate,
                              DateTime lastActivityDate,
                              DateTime lastPasswordChangedDate,
                              DateTime lastLockoutDate,
                              string firstName,
                              string lastName,
                              int timeZone)
                : base(
                        providerName,
                        name,
                        providerUserKey,
                        email,
                        passwordQuestion,
                        comment,
                        isApproved,
                        isLockedOut,
                        creationDate,
                        lastLoginDate,
                        lastActivityDate,
                        lastPasswordChangedDate,
                        lastLockoutDate)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.TimeZone = timeZone;
        }

        public MembershipUser()
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TimeZone { get; set; }
    }
}