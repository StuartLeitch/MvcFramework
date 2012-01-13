using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Linq;
using System.Data.Objects;

namespace Application.Membership
{
    public class EFMembershipProvider : System.Web.Security.MembershipProvider
    {
        private const int PASSWORD_SIZE = 14;
        private const int SALT_SIZE_IN_BYTES = 16;

        private int _SchemaVersionCheck;
        private bool _UseSP = false;
        private bool _AutoInstall = true;
        private bool _ReturnEFMembershipUser = true;
        private bool _EnablePasswordRetrieval;
        private bool _EnablePasswordReset;
        private bool _RequiresQuestionAndAnswer;
        private bool _RequiresUniqueEmail;
        private int _MaxInvalidPasswordAttempts;
        private int _PasswordAttemptWindow;
        private int _MinRequiredPasswordLength;
        private int _MinRequiredNonalphanumericCharacters;
        private string _PasswordStrengthRegularExpression;
        private int _CommandTimeout;
        private string _AppName;
        private int _AppID;
        private MembershipPasswordFormat _PasswordFormat;
        private string _sqlConnectionString;
        private static bool isCheck = false;
        private string s_HashAlgorithm;
        //This is for .Net 4.0
        private MembershipPasswordCompatibilityMode _LegacyPasswordCompatibilityMode;

        public Entities Data
        {
            get
            {
                return SecUtility.Data(this._sqlConnectionString);
                //var db = new MembershipEntities(ConfigurationManager.ConnectionStrings[_sqlConnectionString].ConnectionString);
                //if (isCheck)
                //return db;

                //return db.GetV
            }
        }

        public User GetDBUser(Entities db, string username)
        {
            if (this._UseSP)
                return db.Membership_GetUserByUsername(username, this.ApplicationName).FirstOrDefault();
            else
                return db.Users.FirstOrDefault(u => u.Username == username && u.Application.ApplicationID == this._AppID);
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (String.IsNullOrEmpty(name))
                name = "SqlMembershipProvider";
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "MembershipSqlProvider_description");
            }
            base.Initialize(name, config);

            this._SchemaVersionCheck = 0;

            this._ReturnEFMembershipUser = SecUtility.GetBooleanValue(config, "returnEFMembershipUser", false);
            this._UseSP = SecUtility.GetBooleanValue(config, "useStoredProcedure", false);
            this._AutoInstall = SecUtility.GetBooleanValue(config, "autoInstall", true);
            this._EnablePasswordRetrieval = SecUtility.GetBooleanValue(config, "enablePasswordRetrieval", false);
            this._EnablePasswordReset = SecUtility.GetBooleanValue(config, "enablePasswordReset", true);
            this._RequiresQuestionAndAnswer = SecUtility.GetBooleanValue(config, "requiresQuestionAndAnswer", true);
            this._RequiresUniqueEmail = SecUtility.GetBooleanValue(config, "requiresUniqueEmail", true);
            this._MaxInvalidPasswordAttempts = SecUtility.GetIntValue(config, "maxInvalidPasswordAttempts", 5, false, 0);
            this._PasswordAttemptWindow = SecUtility.GetIntValue(config, "passwordAttemptWindow", 10, false, 0);
            this._MinRequiredPasswordLength = SecUtility.GetIntValue(config, "minRequiredPasswordLength", 7, false, 128);
            this._MinRequiredNonalphanumericCharacters = SecUtility.GetIntValue(config, "minRequiredNonalphanumericCharacters", 1, true, 128);

            this._PasswordStrengthRegularExpression = config["passwordStrengthRegularExpression"];
            if (this._PasswordStrengthRegularExpression != null)
            {
                this._PasswordStrengthRegularExpression = this._PasswordStrengthRegularExpression.Trim();
                if (this._PasswordStrengthRegularExpression.Length != 0)
                {
                    try
                    {
                        Regex regex = new Regex(this._PasswordStrengthRegularExpression);
                    }
                    catch (ArgumentException e)
                    {
                        throw new ProviderException(e.Message, e);
                    }
                }
            }
            else
            {
                this._PasswordStrengthRegularExpression = string.Empty;
            }
            if (this._MinRequiredNonalphanumericCharacters > this._MinRequiredPasswordLength)
                throw new HttpException("MinRequiredNonalphanumericCharacters_can_not_be_more_than_MinRequiredPasswordLength");

            string temp = config["connectionStringName"];
            if (temp == null || temp.Length < 1)
                throw new ProviderException("Connection_name_not_specified");
            this._sqlConnectionString = temp;
            if (this._sqlConnectionString == null || this._sqlConnectionString.Length < 1)
            {
                throw new ProviderException("Connection_string_not_found");
            }

            this._CommandTimeout = SecUtility.GetIntValue(config, "commandTimeout", 30, true, 0);
            this._AppName = config["applicationName"];

            if (string.IsNullOrEmpty(this._AppName))
                this._AppName = "/";

            if (this._AppName.Length > 256)
            {
                throw new ProviderException("Provider_application_name_too_long");
            }

            using (var db = this.Data)
                this._AppID = db.GetApplicationID(this._AppName);

            string strTemp = config["passwordFormat"];
            if (strTemp == null)
                strTemp = "Hashed";

            switch (strTemp)
            {
                case "Clear":
                    this._PasswordFormat = MembershipPasswordFormat.Clear;
                    break;
                case "Encrypted":
                    this._PasswordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Hashed":
                    this._PasswordFormat = MembershipPasswordFormat.Hashed;
                    break;
                default:
                    throw new ProviderException("Provider_bad_password_format");
            }

            if (this.PasswordFormat == MembershipPasswordFormat.Hashed && this.EnablePasswordRetrieval)
                throw new ProviderException("Provider_can_not_retrieve_hashed_password");

            string passwordCompactMode = config["passwordCompatMode"];
            if (!string.IsNullOrEmpty(passwordCompactMode))
            {
                this._LegacyPasswordCompatibilityMode = (MembershipPasswordCompatibilityMode)Enum.Parse(typeof(MembershipPasswordCompatibilityMode), passwordCompactMode);
            }

            config.Remove("returnEFMembershipUser");
            config.Remove("useStoredProcedure");
            config.Remove("autoInstall");
            config.Remove("returnEFMembershipUser");
            config.Remove("connectionStringName");
            config.Remove("enablePasswordRetrieval");
            config.Remove("enablePasswordReset");
            config.Remove("requiresQuestionAndAnswer");
            config.Remove("applicationName");
            config.Remove("requiresUniqueEmail");
            config.Remove("maxInvalidPasswordAttempts");
            config.Remove("passwordAttemptWindow");
            config.Remove("commandTimeout");
            config.Remove("passwordFormat");
            config.Remove("name");
            config.Remove("minRequiredPasswordLength");
            config.Remove("minRequiredNonalphanumericCharacters");
            config.Remove("passwordStrengthRegularExpression");
            if (config.Count > 0)
            {
                string attribUnrecognized = config.GetKey(0);
                if (!String.IsNullOrEmpty(attribUnrecognized))
                    throw new ProviderException("Provider_unrecognized_attribute, attribUnrecognized");
            }
        }

        public override string ApplicationName
        {
            get
            {
                return this._AppName;
            }
            set
            {
                this._AppName = value;

                using (var db = this.Data)
                    this._AppID = db.GetApplicationID(this._AppName);
            }
        }

        private bool CheckPassword(Entities db, string username, string password, bool updateLastLoginActivityDate, bool failIfNotApproved, out User usr)
        {
            string salt;
            int passwordFormat;
            return this.CheckPassword(db, username, password, updateLastLoginActivityDate, failIfNotApproved, out salt, out passwordFormat, out usr);
        }

        private bool CheckPassword(Entities db, string username, string password, bool updateLastLoginActivityDate, bool failIfNotApproved, out string salt, out int passwordFormat, out User usr)
        {
            var user = this.GetDBUser(db, username);

            usr = user;
            if (user == null)
            {
                salt = null;
                passwordFormat = -1;

                return false;
            }

            var enc = this.EncodePassword(password, user.PasswordFormat, user.PasswordSalt);
            passwordFormat = user.PasswordFormat;
            salt = user.PasswordSalt;
            if (enc == user.Password)
            {
                if (updateLastLoginActivityDate)
                {
                    if (this._UseSP)
                        db.Membership_SetUserLoginDate(user.UserID, DateTime.Now);
                    else
                    {
                        user.LastActivityDate = DateTime.Now;
                        user.LastLoginDate = DateTime.Now;

                        db.SaveChanges();
                    }
                }
                return true;
            }
            else
                return false;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            SecUtility.CheckParameter(ref username, true, true, true, 0x100, "username");
            SecUtility.CheckParameter(ref oldPassword, true, true, false, 0x80, "oldPassword");
            SecUtility.CheckParameter(ref newPassword, true, true, false, 0x80, "newPassword");

            var salt = string.Empty;
            var passwordFormat = 1;

            using (var db = this.Data)
            {
                var user = default(User);
                if (!this.CheckPassword(db, username, oldPassword, false, false, out salt, out passwordFormat, out user))
                {
                    return false;
                }

                if (this._UseSP)
                {
                    var status = db.Membership_ChangePassword(this.ApplicationName,
                                                              username,
                                                              this.EncodePassword(newPassword, passwordFormat, salt),
                                                              salt,
                                                              DateTime.Now).FirstOrDefault();

                    if (status != 0) return false;
                }
                else
                {
                    user.Password = this.EncodePassword(newPassword, passwordFormat, salt);
                    user.LastPasswordChangedDate = DateTime.Now;
                    user.FailedPasswordAnswerAttemptCount = 0;
                    user.FailedPasswordAttemptCount = 0;
                    db.SaveChanges();
                }
            }
            return true;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            using (var db = this.Data)
            {
                var user = default(User);
                string salt; int passwordFormat;
                if (!this.CheckPassword(db, username, password, false, false, out salt, out passwordFormat, out user))
                {
                    return false;
                }

                if (this._UseSP)
                {
                    return db.Membership_ChangePasswordQuestionAndAnswer(this.ApplicationName, username, newPasswordQuestion, newPasswordAnswer).FirstOrDefault() == 0;
                }
                else
                {
                    user.PasswordQuestion = newPasswordQuestion;
                    user.PasswordAnswer = newPasswordAnswer;

                    db.SaveChanges();
                }
                return true;
            }
        }

        public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            if (!ValidateParameter(ref password, true, true, false, 128))
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            var salt = this.GenerateSalt();
            var pass = this.EncodePassword(password, (int)this._PasswordFormat, salt);
            if (pass.Length > 128)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            string encodedPasswordAnswer;
            if (passwordAnswer != null)
            {
                passwordAnswer = passwordAnswer.Trim();
            }

            if (!string.IsNullOrEmpty(passwordAnswer))
            {
                if (passwordAnswer.Length > 128)
                {
                    status = MembershipCreateStatus.InvalidAnswer;
                    return null;
                }
                encodedPasswordAnswer = this.EncodePassword(passwordAnswer.ToLower(CultureInfo.InvariantCulture), (int)this._PasswordFormat, salt);
            }
            else
                encodedPasswordAnswer = passwordAnswer;

            if (!ValidateParameter(ref encodedPasswordAnswer, this.RequiresQuestionAndAnswer, true, false, 128))
            {
                status = MembershipCreateStatus.InvalidAnswer;
                return null;
            }

            if (!ValidateParameter(ref username, true, true, true, 256))
            {
                status = MembershipCreateStatus.InvalidUserName;
                return null;
            }

            if (!ValidateParameter(ref email,
                                               this.RequiresUniqueEmail,
                                               this.RequiresUniqueEmail,
                                               false,
                                               256))
            {
                status = MembershipCreateStatus.InvalidEmail;
                return null;
            }

            if (!ValidateParameter(ref passwordQuestion, this.RequiresQuestionAndAnswer, true, false, 256))
            {
                status = MembershipCreateStatus.InvalidQuestion;
                return null;
            }

            if (providerUserKey != null)
            {
                //if (!(providerUserKey is Guid)) {
                //    status = MembershipCreateStatus.InvalidProviderUserKey;
                //    return null;
                //}
                status = MembershipCreateStatus.InvalidProviderUserKey;
                return null;
            }

            if (password.Length < this.MinRequiredPasswordLength)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            int count = 0;

            for (int i = 0; i < password.Length; i++)
            {
                if (!char.IsLetterOrDigit(password, i))
                {
                    count++;
                }
            }

            if (count < this.MinRequiredNonAlphanumericCharacters)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (this.PasswordStrengthRegularExpression.Length > 0)
            {
                if (!Regex.IsMatch(password, this.PasswordStrengthRegularExpression))
                {
                    status = MembershipCreateStatus.InvalidPassword;
                    return null;
                }
            }


            ValidatePasswordEventArgs e = new ValidatePasswordEventArgs(username, password, true);
            this.OnValidatingPassword(e);

            if (e.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    var userId = new ObjectParameter("UserID", typeof(int));
                    var time = DateTime.UtcNow;
                    var pStatus = new ObjectParameter("RETURN_VALUE", typeof(int));
                    var cStatus = db.Membership_CreateUser(this.ApplicationName,
                                                           username,
                                                           pass,
                                                           salt,
                                                           email,
                                                           passwordQuestion,
                                                           passwordAnswer,
                                                           isApproved,
                                                           false,
                                                           0,
                                                           time,
                                                           this.RequiresUniqueEmail,
                                                           (int)this.PasswordFormat,
                                                           userId).FirstOrDefault();

                    if ((cStatus < 0) || (cStatus > 11))
                    {
                        cStatus = 11;
                    }
                    status = (MembershipCreateStatus)cStatus;
                    if (cStatus != 0)
                    {
                        return null;
                    }

                    return new MembershipUser(this.Name, username, userId.Value, email, passwordQuestion, null, isApproved, false, time, time, time, time, new DateTime(0x6da, 1, 1), null, null, 0);
                }
                else
                {
                    if (this.RequiresUniqueEmail)
                    {
                        if (db.Users.Where(u => u.Email == email && u.Application.ApplicationID == this._AppID).Any())
                        {
                            status = MembershipCreateStatus.DuplicateEmail;
                            return null;
                        }
                    }

                    if (db.Users.Where(u => u.Username == username && u.Application.ApplicationID == this._AppID).Any())
                    {
                        status = MembershipCreateStatus.DuplicateUserName;
                        return null;
                    }

                    var utc = DateTime.UtcNow;
                    var user = new User()
                    {
                        Comment = "",
                        CreateOn = utc,
                        Email = email,
                        FailedPasswordAnswerAttemptCount = 0,
                        FailedPasswordAnswerAttemptWindowStart = utc,
                        FailedPasswordAttemptCount = 0,
                        FailedPasswordAttemptWindowStart = utc,
                        IsAnonymous = false,
                        IsApproved = isApproved,
                        LastActivityDate = utc,
                        LastLockoutDate = utc,
                        LastLoginDate = utc,
                        LastPasswordChangedDate = utc,
                        Password = pass,
                        PasswordAnswer = encodedPasswordAnswer,
                        PasswordFormat = (int)this.PasswordFormat,
                        PasswordQuestion = passwordQuestion,
                        PasswordSalt = salt,
                        TimeZone = 0,
                        Username = username,
                        Application = db.GetApplication(this._AppID)
                    };

                    db.AddToUsers(user);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch
                    {
                        status = MembershipCreateStatus.UserRejected;
                        return null;
                    }

                    status = MembershipCreateStatus.Success;
                    return UserMapper.Map(this.Name, user, this._ReturnEFMembershipUser);
                }
            }

        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            SecUtility.CheckParameter(ref username, true, true, true, 0x100, "username");

            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    return db.Membership_DeleteUser(this.ApplicationName, username, deleteAllRelatedData).FirstOrDefault() == 0;
                }
                else
                {
                    var user = db.Users.Include("Roles").FirstOrDefault(u => u.Username == username && u.Application.ApplicationID == this._AppID);//.Include("Roles").Include("");

                    if (user == null)
                        return false;

                    foreach (var role in user.Roles)
                        user.Roles.Remove(role);

                    db.DeleteObject(user);
                    db.SaveChanges();

                    return true;
                }
            }
        }

        public override bool EnablePasswordReset
        {
            get { return this._EnablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return this._EnablePasswordRetrieval; }
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            SecUtility.CheckParameter(ref emailToMatch, false, false, false, 0x100, "emailToMatch");
            if (pageIndex < 0)
            {
                throw new ArgumentException("PageIndex_bad", "pageIndex");
            }
            if (pageSize < 1)
            {
                throw new ArgumentException("PageSize_bad", "pageSize");
            }

            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    var totalRecord = new ObjectParameter("TotalRecord", typeof(int));
                    var output = db.Membership_FindUsersByEmail(this.ApplicationName, emailToMatch, pageIndex, pageSize, totalRecord);
                    totalRecords = (int)(totalRecord.Value);

                    var col = new MembershipUserCollection();
                    foreach (var item in output)
                        col.Add(UserMapper.Map(this.Name, item, this._ReturnEFMembershipUser));

                    return col;
                }
                else
                {
                    var query = from user in db.Users
                                where user.Email.Contains(emailToMatch) && user.Application.ApplicationID == this._AppID
                                orderby user.UserID
                                select user;

                    totalRecords = query.Count();
                    var col = new MembershipUserCollection();

                    foreach (var item in query.Skip(pageSize * pageIndex).Take(pageSize))
                        col.Add(UserMapper.Map(this.Name, item, this._ReturnEFMembershipUser));

                    return col;
                }
            }
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 0x100, "usernameToMatch");
            if (pageIndex < 0)
            {
                throw new ArgumentException("PageIndex_bad", "pageIndex");
            }
            if (pageSize < 1)
            {
                throw new ArgumentException("PageSize_bad", "pageSize");
            }

            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    var totalRecord = new ObjectParameter("TotalRecord", typeof(int));
                    var output = db.Membership_FindUsersByName(this.ApplicationName, usernameToMatch, pageIndex, pageSize, totalRecord);
                    totalRecords = (int)(totalRecord.Value);

                    var col = new MembershipUserCollection();
                    foreach (var item in output)
                        col.Add(UserMapper.Map(this.Name, item, this._ReturnEFMembershipUser));

                    return col;
                }
                else
                {
                    var query = from user in db.Users
                                where user.Username.Contains(usernameToMatch) && user.Application.ApplicationID == this._AppID
                                orderby user.UserID
                                select user;

                    totalRecords = query.Count();
                    var col = new MembershipUserCollection();

                    foreach (var item in query.Skip(pageSize * pageIndex).Take(pageSize))
                        col.Add(UserMapper.Map(this.Name, item, this._ReturnEFMembershipUser));

                    return col;
                }
            }
        }

        public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            if (pageIndex < 0)
            {
                throw new ArgumentException("PageIndex_bad", "pageIndex");
            }
            if (pageSize < 1)
            {
                throw new ArgumentException("PageSize_bad", "pageSize");
            }
            long num = ((pageIndex * pageSize) + pageSize) - 1L;
            if (num > 0x7fffffffL)
            {
                throw new ArgumentException("PageIndex_PageSize_bad", "pageIndex and pageSize");
            }

            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    var totalRecord = new ObjectParameter("TotalRecord", typeof(int));
                    var output = db.Membership_GetAllUsers(this.ApplicationName, pageIndex, pageSize, totalRecord);

                    var col = new MembershipUserCollection();
                    foreach (var item in output)
                        col.Add(UserMapper.Map(this.Name, item, this._ReturnEFMembershipUser));

                    totalRecords = (int)(totalRecord.Value);
                    return col;
                }
                else
                {
                    totalRecords = db.Users.Count();
                    var col = new MembershipUserCollection();

                    foreach (var item in db.Users.Where(u => u.Application.ApplicationID == this._AppID).OrderBy(o => o.UserID).Skip(pageSize * pageIndex).Take(pageSize))
                        col.Add(UserMapper.Map(this.Name, item, this._ReturnEFMembershipUser));

                    return col;
                }
            }
        }

        public override int GetNumberOfUsersOnline()
        {
            using (var db = this.Data)
                if (this._UseSP)
                    return db.Membership_GetNumberOfUsersOnline(this.ApplicationName, System.Web.Security.Membership.UserIsOnlineTimeWindow, DateTime.UtcNow);
                else
                    return db.Users.Where(u => u.LastActivityDate > DateTime.Now.AddMinutes(System.Web.Security.Membership.UserIsOnlineTimeWindow) && u.Application.ApplicationID == this._AppID).Count();
        }

        public override string GetPassword(string username, string answer)
        {
            using (var db = this.Data)
            {
                var usr = this.GetDBUser(db, username);
                if (db == null)
                    return null;

                if (usr.PasswordAnswer == answer)
                {
                    return this.UnEncodePassword(usr.Password, usr.PasswordFormat);
                }
                else
                    return null;
            }
        }

        public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    try
                    {
                        var user = db.Membership_GetUser(this.ApplicationName, username, DateTime.UtcNow, userIsOnline).FirstOrDefault();
                        if (user == null) return null;

                        return UserMapper.Map(this.Name, user, this._ReturnEFMembershipUser);
                    }
                    catch { return null; }
                }
                else
                {
                    var usr = this.GetDBUser(db, username);
                    if (userIsOnline)
                    {
                        usr.LastActivityDate = DateTime.UtcNow;
                        db.SaveChanges();
                    }
                    return UserMapper.Map(this.Name, usr, this._ReturnEFMembershipUser);
                }
            }
        }

        public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            if (providerUserKey == null)
            {
                throw new ArgumentNullException("providerUserKey");
            }
            if (!(providerUserKey is int))
            {
                throw new ArgumentException("Membership_InvalidProviderUserKey", "providerUserKey");
            }


            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    try
                    {
                        var user = db.Membership_GetUserByProviderKey(this.ApplicationName, (int)providerUserKey, DateTime.UtcNow, userIsOnline).FirstOrDefault();
                        if (user == null) return null;

                        return UserMapper.Map(this.Name, user, this._ReturnEFMembershipUser);
                    }
                    catch { return null; }
                }
                else
                {
                    var uid = (int)providerUserKey;
                    var usr = db.Users.FirstOrDefault(u => u.UserID == uid && u.Application.ApplicationID == this._AppID);
                    if (usr == null) return null;
                    if (userIsOnline)
                    {
                        usr.LastActivityDate = DateTime.UtcNow;
                        db.SaveChanges();
                    }
                    return UserMapper.Map(this.Name, usr, this._ReturnEFMembershipUser);
                }
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            SecUtility.CheckParameter(ref email, false, false, false, 0x100, "email");

            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    var rowCount = new ObjectParameter("RowCount", typeof(int));
                    var result = db.Membership_GetUserNameByEmail(this.ApplicationName, email, rowCount).FirstOrDefault();

                    if ((int)(rowCount.Value) > 1 && this.RequiresUniqueEmail)
                        throw new ProviderException("Membership_more_than_one_user_with_email");

                    return result;
                }
                else
                {
                    var usr = (from u in db.Users where u.Email == email && u.Application.ApplicationID == this._AppID select u.Username).FirstOrDefault();
                    return usr;
                }
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return this._MaxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return this._MinRequiredNonalphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return this._MinRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return this._PasswordAttemptWindow; }
        }

        public override System.Web.Security.MembershipPasswordFormat PasswordFormat
        {
            get { return this._PasswordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return this._PasswordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return this._RequiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return this._RequiresUniqueEmail; }
        }

        public override string ResetPassword(string username, string answer)
        {
            if (!this.EnablePasswordReset)
            {
                throw new NotSupportedException("Not_configured_to_support_password_resets");
            }

            SecUtility.CheckParameter(ref username, true, true, true, 256, "username");

            using (var db = this.Data)
            {
                var user = this.GetDBUser(db, username);
                var passwordAnswer = answer;

                string encodedPasswordAnswer;
                if (passwordAnswer != null)
                {
                    passwordAnswer = passwordAnswer.Trim();
                }
                if (!string.IsNullOrEmpty(passwordAnswer))
                    encodedPasswordAnswer = this.EncodePassword(passwordAnswer.ToLower(CultureInfo.InvariantCulture), user.PasswordFormat, user.PasswordSalt);
                else
                    encodedPasswordAnswer = passwordAnswer;

                SecUtility.CheckParameter(ref encodedPasswordAnswer, this.RequiresQuestionAndAnswer, this.RequiresQuestionAndAnswer, false, 128, "passwordAnswer");
                string newPassword = this.GeneratePassword();

                ValidatePasswordEventArgs e = new ValidatePasswordEventArgs(username, newPassword, false);
                this.OnValidatingPassword(e);

                if (e.Cancel)
                {
                    if (e.FailureInformation != null)
                    {
                        throw e.FailureInformation;
                    }
                    else
                    {
                        throw new ProviderException("Membership_Custom_Password_Validation_Failure");
                    }
                }

                var utc = DateTime.UtcNow;
                if (!answer.Equals(user.PasswordAnswer, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (utc > user.FailedPasswordAnswerAttemptWindowStart.AddMinutes(this.PasswordAttemptWindow))
                    {
                        user.FailedPasswordAnswerAttemptCount = 1;
                    }
                    else
                    {
                        user.FailedPasswordAnswerAttemptCount++;
                    }
                    user.FailedPasswordAnswerAttemptWindowStart = utc;

                    if (user.FailedPasswordAnswerAttemptCount > this.MaxInvalidPasswordAttempts)
                    {
                        user.LastLockoutDate = DateTime.UtcNow;
                        user.Status = (byte)UserStatus.Locked;
                    }

                    db.SaveChanges();
                    return null;
                }

                if (this._UseSP)
                {
                    var result = db.Membership_ResetPassword(this.ApplicationName, username, newPassword,
                                                             this.MaxInvalidPasswordAttempts, this.PasswordAttemptWindow, user.PasswordSalt,
                                                             utc, (int)this.PasswordFormat, passwordAnswer).FirstOrDefault().GetValueOrDefault(0);
                    var num2 = (result != null) ? result : -1;
                    if (num2 != 0)
                    {
                        string exceptionText = this.GetExceptionText(num2);
                        if (this.IsStatusDueToBadPassword(num2))
                        {
                            throw new MembershipPasswordException(exceptionText);
                        }
                        throw new ProviderException(exceptionText);
                    }
                }
                else
                {
                    user.FailedPasswordAnswerAttemptCount = 0;
                    user.FailedPasswordAnswerAttemptWindowStart = new DateTime(1754, 01, 01);

                    user.FailedPasswordAttemptCount = 0;
                    user.FailedPasswordAttemptWindowStart = user.FailedPasswordAnswerAttemptWindowStart;
                    
                    user.Password = this.EncodePassword(newPassword, user.PasswordFormat, user.PasswordSalt);
                    db.SaveChanges();
                }

                return newPassword;
            }

        }

        public override bool UnlockUser(string userName)
        {
            SecUtility.CheckParameter(ref userName, true, true, true, 256, "username");
            try
            {
                using (var db = this.Data)
                {
                    if (this._UseSP)
                    {
                        return db.Membership_UnlockUser(this.ApplicationName, userName).FirstOrDefault() > 0;
                    }
                    else
                    {
                        var user = this.GetDBUser(db, userName);
                        if (user == null)
                            return false;

                        user.Status = (byte)UserStatus.Approved;
                        user.LastLockoutDate = DateTime.UtcNow;

                        db.SaveChanges();
                        return true;
                    }
                }
            }
            catch { return false; }
        }

        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            string temp = user.UserName;
            SecUtility.CheckParameter(ref temp, true, true, true, 256, "UserName");
            temp = user.Email;
            SecUtility.CheckParameter(ref temp,
                                      this.RequiresUniqueEmail,
                                      this.RequiresUniqueEmail,
                                      false,
                                      256,
                                      "Email");
            user.Email = temp;

            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    var firstName = default(string);
                    var lastName = default(string);

                    if (user is MembershipUser)
                    {
                        var muser = (MembershipUser)user;

                        firstName = muser.FirstName;
                        lastName = muser.LastName;
                    }

                    var status = db.Membership_UpdateUser(this.ApplicationName, (int)user.ProviderUserKey, this.RequiresUniqueEmail, user.Email, user.Comment,
                                                          user.IsApproved, user.LastLoginDate, user is MembershipUser, firstName, lastName).FirstOrDefault().GetValueOrDefault(0);
                    if (status != 0)
                    {
                        throw new ProviderException(this.GetExceptionText(status));

                    }
                }
                else
                {
                    var query = from u in db.Users
                                where u.UserID == (int)user.ProviderUserKey && u.Application.ApplicationID == this._AppID
                                select u;

                    var usr = query.FirstOrDefault();
                    if (usr == null)
                        throw new ProviderException(this.GetExceptionText(1));

                    if (this.RequiresUniqueEmail)
                    {
                        var q = from u in db.Users
                                where u.UserID != (int)user.ProviderUserKey
                                   && u.Email == user.Email && u.Application.ApplicationID == this._AppID
                                select u;

                        if (q.Any())
                            throw new ProviderException(this.GetExceptionText(7));
                    }

                    usr.Email = user.Email;
                    usr.Comment = user.Comment;
                    usr.IsApproved = user.IsApproved;
                    usr.LastLoginDate = user.LastLoginDate;
                    if (user is MembershipUser)
                    {
                        var muser = (MembershipUser)user;

                        usr.FirstName = muser.FirstName;
                        usr.LastName = muser.LastName;
                    }

                    db.SaveChanges();
                }
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            using (var db = this.Data)
            {
                var usr = default(User);
                if (ValidateParameter(ref username, true, true, true, 256) &&
                        ValidateParameter(ref password, true, true, false, 128) &&
                        this.CheckPassword(db, username, password, true, true, out usr))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private HashAlgorithm GetHashAlgorithm()
        {
            if (this.s_HashAlgorithm != null)
            {
                return HashAlgorithm.Create(this.s_HashAlgorithm);
            }
            string hashAlgorithmType = System.Web.Security.Membership.HashAlgorithmType;
            if (((this._LegacyPasswordCompatibilityMode == MembershipPasswordCompatibilityMode.Framework20)) && (hashAlgorithmType != "MD5"))
            {
                hashAlgorithmType = "SHA1";
            }
            HashAlgorithm algorithm = HashAlgorithm.Create(hashAlgorithmType);
            if (algorithm == null)
            {
                throw new ConfigurationErrorsException("Invalid_hash_algorithm_type");
            }
            this.s_HashAlgorithm = hashAlgorithmType;
            return algorithm;
        }

        internal string EncodePassword(string pass, int passwordFormat, string salt)
        {

            if (passwordFormat == 0)
            {
                return pass;
            }
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] src = Convert.FromBase64String(salt);
            byte[] inArray = null;
            if (passwordFormat == 1)
            {
                HashAlgorithm hashAlgorithm = this.GetHashAlgorithm();
                if (hashAlgorithm is KeyedHashAlgorithm)
                {
                    KeyedHashAlgorithm algorithm2 = (KeyedHashAlgorithm)hashAlgorithm;
                    if (algorithm2.Key.Length == src.Length)
                    {
                        algorithm2.Key = src;
                    }
                    else if (algorithm2.Key.Length < src.Length)
                    {
                        byte[] dst = new byte[algorithm2.Key.Length];
                        Buffer.BlockCopy(src, 0, dst, 0, dst.Length);
                        algorithm2.Key = dst;
                    }
                    else
                    {
                        int num2;
                        byte[] buffer5 = new byte[algorithm2.Key.Length];
                        for (int i = 0; i < buffer5.Length; i += num2)
                        {
                            num2 = Math.Min(src.Length, buffer5.Length - i);
                            Buffer.BlockCopy(src, 0, buffer5, i, num2);
                        }
                        algorithm2.Key = buffer5;
                    }
                    inArray = algorithm2.ComputeHash(bytes);
                }
                else
                {
                    byte[] buffer6 = new byte[src.Length + bytes.Length];
                    Buffer.BlockCopy(src, 0, buffer6, 0, src.Length);
                    Buffer.BlockCopy(bytes, 0, buffer6, src.Length, bytes.Length);
                    inArray = hashAlgorithm.ComputeHash(buffer6);
                }
            }
            else
            {
                byte[] buffer7 = new byte[src.Length + bytes.Length];
                Buffer.BlockCopy(src, 0, buffer7, 0, src.Length);
                Buffer.BlockCopy(bytes, 0, buffer7, src.Length, bytes.Length);
                inArray = this.EncryptPassword(buffer7, this._LegacyPasswordCompatibilityMode);
            }
            return Convert.ToBase64String(inArray);
        }

        internal string UnEncodePassword(string pass, int passwordFormat)
        {
            switch (passwordFormat)
            {
                case 0:
                    return pass;

                case 1:
                    throw new ProviderException("Provider_can_not_decode_hashed_password");
            }
            byte[] encodedPassword = Convert.FromBase64String(pass);
            byte[] bytes = this.DecryptPassword(encodedPassword);
            if (bytes == null)
            {
                return null;
            }
            return Encoding.Unicode.GetString(bytes, 0x10, bytes.Length - 0x10);

        }

        internal string GenerateSalt()
        {
            byte[] buf = new byte[SALT_SIZE_IN_BYTES];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        public virtual string GeneratePassword()
        {
            return System.Web.Security.Membership.GeneratePassword(
                      this.MinRequiredPasswordLength < PASSWORD_SIZE ? PASSWORD_SIZE : this.MinRequiredPasswordLength,
                      this.MinRequiredNonAlphanumericCharacters);
        }

        internal static bool ValidateParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize)
        {
            if (param == null)
            {
                return !checkForNull;
            }

            param = param.Trim();
            if ((checkIfEmpty && param.Length < 1) ||
                 (maxSize > 0 && param.Length > maxSize) ||
                 (checkForCommas && param.Contains(",")))
            {
                return false;
            }

            return true;
        }

        private string GetExceptionText(int status)
        {
            string key;
            switch (status)
            {
                case 0:
                    return String.Empty;
                case 1:
                    key = "Membership_UserNotFound";
                    break;
                case 2:
                    key = "Membership_WrongPassword";
                    break;
                case 3:
                    key = "Membership_WrongAnswer";
                    break;
                case 4:
                    key = "Membership_InvalidPassword";
                    break;
                case 5:
                    key = "Membership_InvalidQuestion";
                    break;
                case 6:
                    key = "Membership_InvalidAnswer";
                    break;
                case 7:
                    key = "Membership_InvalidEmail";
                    break;
                case 99:
                    key = "Membership_AccountLockOut";
                    break;
                default:
                    key = "Provider_Error";
                    break;
            }

            return key;
        }

        private bool IsStatusDueToBadPassword(int status)
        {
            return (((status >= 2) && (status <= 6)) || (status == 0x63));
        }
    }
}
