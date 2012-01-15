using System;
using System.Linq;

namespace Application.Core.Membership
{
    public static class UserDataParser
    {
        private enum UserDataItem
        {
            UserId = 0,
            FriendlyName = 1,
            Email = 2,
        }

        public static WrappedUser Decode(string userData, string[] roles) {
            var parsedData = userData.Split('|');

            int userId;
            if (parsedData.Length != 3 || !int.TryParse(parsedData[(int)UserDataItem.UserId], out userId))
                throw new InvalidOperationException("Attempted to get User, but data is corrupted.");

            var wrappedUser = new WrappedUser(userId, parsedData[(int)UserDataItem.FriendlyName], parsedData[(int)UserDataItem.Email], roles);

            return wrappedUser;
        }

        public static string Encode(int userId, string friendlyName, string email) {
            var userData = String.Format("{0}|{1}|{2}", userId, friendlyName, email);
            return userData;
        }
    }
}