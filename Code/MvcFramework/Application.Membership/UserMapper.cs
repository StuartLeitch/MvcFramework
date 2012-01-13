using System.Linq;

namespace Application.Membership {
    public static class UserMapper {

        public static System.Web.Security.MembershipUser Map(string pname, User user, bool EFMembership) {

            if (EFMembership)
                return new MembershipUser(pname, user.Username, user.UserID, user.Email, user.PasswordQuestion, user.Comment, user.IsApproved,
                                          user.Status == 2, user.CreateOn, user.LastLoginDate, user.LastActivityDate, user.LastPasswordChangedDate,
                                          user.LastLockoutDate, user.FirstName, user.LastName, user.TimeZone.GetValueOrDefault(0));
            else
                return new System.Web.Security.MembershipUser(pname, user.Username, user.UserID, user.Email, user.PasswordQuestion, user.Comment, user.IsApproved,
                                                              user.Status == 2, user.CreateOn, user.LastLoginDate, user.LastActivityDate, user.LastPasswordChangedDate,
                                                              user.LastLockoutDate);
        }

    }
}
