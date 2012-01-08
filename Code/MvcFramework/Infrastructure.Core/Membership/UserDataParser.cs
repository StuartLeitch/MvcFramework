using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Infrastructure.Core.Membership
{
    public class UserDataParser 
    {
        private enum UserDataItem
        {
            UserId = 0,
            FriendlyName = 1,
            Email = 2,
        }
        public WrappedUser Decoder(string userData, string[] roles)
        {
            var parsedData = userData.Split('|');

            int userId;
            if (parsedData.Length != 3 || !int.TryParse(parsedData[(int)UserDataItem.UserId], out userId))
                throw new InvalidOperationException("Attempted to get User, but data is corrupted.");

            var wrappedUser = new WrappedUser(userId, parsedData[(int)UserDataItem.FriendlyName], parsedData[(int)UserDataItem.Email], roles);

            return wrappedUser;
        }

        public string Encode(WrappedUser wrappedUser)
        {
            var userData = String.Format("{0}|{1}|{2}", wrappedUser.UserId, wrappedUser.FriendlyName, wrappedUser.Email);
            return userData;
        }

    }
}
