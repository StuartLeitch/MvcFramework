using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;

//

namespace Application.Membership
{
    public class EFRoleProvider : System.Web.Security.RoleProvider
    {

        private string _AppName;
        private int _AppID;
        private int _SchemaVersionCheck;
        private string _sqlConnectionString;
        private int _CommandTimeout;
        private bool _UseSP = false;

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

        public override void Initialize(string name, NameValueCollection config)
        {
            //HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, SR.Feature_not_supported_at_this_level);
            if (config == null)
                throw new ArgumentNullException("config");

            if (String.IsNullOrEmpty(name))
                name = "SqlRoleProvider";
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "RoleSqlProvider_description");
            }
            base.Initialize(name, config);

            this._SchemaVersionCheck = 0;

            this._UseSP = SecUtility.GetBooleanValue(config, "useStoredProcedure", false);
            this._CommandTimeout = SecUtility.GetIntValue(config, "commandTimeout", 30, true, 0);

            string temp = config["connectionStringName"];
            if (temp == null || temp.Length < 1)
                throw new ProviderException("Connection_name_not_specified");
            this._sqlConnectionString = temp; //SqlConnectionHelper.GetConnectionString(temp, true, true);
            if (this._sqlConnectionString == null || this._sqlConnectionString.Length < 1)
            {
                throw new ProviderException("Connection_string_not_found, " + temp);
            }

            this._AppName = config["applicationName"];
            if (string.IsNullOrEmpty(this._AppName))
                this._AppName = "/";

            if (this._AppName.Length > 256)
            {
                throw new ProviderException("Provider_application_name_too_long");
            }
            using (var db = this.Data)
                this._AppID = db.GetApplicationID(this._AppName);

            config.Remove("useStoredProcedure");
            config.Remove("connectionStringName");
            config.Remove("applicationName");
            config.Remove("commandTimeout");
            if (config.Count > 0)
            {
                string attribUnrecognized = config.GetKey(0);
                if (!String.IsNullOrEmpty(attribUnrecognized))
                    throw new ProviderException("Provider_unrecognized_attribute, " + attribUnrecognized);
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            SecUtility.CheckArrayParameter(ref roleNames, true, true, true, 256, "roleNames");
            SecUtility.CheckArrayParameter(ref usernames, true, true, true, 256, "usernames");

            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    db.Roles_AddUsersToRoles(this.ApplicationName, string.Join(",", usernames), string.Join(",", roleNames));
                }
                else
                {
                    var joinUsers = "'" + string.Join("','", usernames) + "'";
                    var users = db.Users.Include("Roles").Where("it.Username in { " + joinUsers + " } AND it.Application.ApplicationID = " + this._AppID.ToString()).ToList();

                    var joinRoles = "'" + string.Join("','", roleNames) + "'";
                    var roles = db.Roles.Where("it.RoleName in { " + joinRoles + " } AND it.Application.ApplicationID = " + this._AppID.ToString()).ToList();

                    foreach (var user in users)
                        foreach (var role in roles)
                        {
                            if (!user.Roles.Contains(role))
                                user.Roles.Add(role);
                        }

                    db.SaveChanges();
                }
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

        public override void CreateRole(string roleName)
        {
            SecUtility.CheckParameter(ref roleName, true, true, true, 256, "roleName");

            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    if (db.Roles_CreateRole(this.ApplicationName, roleName).FirstOrDefault() == 0)
                        throw new ProviderException("Provider_role_already_exists, " + roleName);
                }
                else
                {
                    try
                    {
                        var query = from r in db.Roles
                                    where r.RoleName == roleName && r.Application.ApplicationID == this._AppID
                                    select r;
                        if (query.Any())
                            throw new ProviderException("Provider_role_already_exists, " + roleName);

                        db.AddToRoles(new Role()
                        {
                            RoleName = roleName,
                            Application = db.GetApplication(this._AppID)
                        });

                        db.SaveChanges();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            SecUtility.CheckParameter(ref roleName, true, true, true, 256, "roleName");
            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    switch (db.Roles_DeleteRole(this.ApplicationName, roleName, !throwOnPopulatedRole).FirstOrDefault())
                    {
                        case 2:
                            throw new ProviderException("Role_is_not_empty");
                        case 1:
                            return false;
                        case 0:
                            return true;
                    }
                    return false;
                }
                else
                {
                    var query = from r in db.Roles
                                where r.RoleName == roleName && r.Application.ApplicationID == this._AppID
                                select new { r, child = r.Users.Count() };
                    if (query.Any())
                    {
                        var r = query.First();
                        if (r.child > 0 && throwOnPopulatedRole)
                            throw new ProviderException("Role_is_not_empty");

                        db.DeleteObject(r.r);
                        db.SaveChanges();

                        return true;

                    }

                    return false;
                }
            }
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            SecUtility.CheckParameter(ref roleName, true, true, true, 256, "roleName");
            SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 256, "usernameToMatch");

            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    return db.Roles_FindUsersInRole(this.ApplicationName, usernameToMatch, roleName).ToArray();
                }
                else
                {
                    var query = from r in db.Roles
                                from u in r.Users
                                where r.RoleName == roleName && u.Username.Contains(usernameToMatch) && r.Application.ApplicationID == this._AppID
                                select u.Username;

                    return query.ToArray();
                }
            }
        }

        public override string[] GetAllRoles()
        {
            using (var db = this.Data)
            {
                if (this._UseSP)
                    return db.Roles_GetAllRoles(this.ApplicationName).ToArray();
                else
                    return db.Roles.Where(r => r.Application.ApplicationID == this._AppID).Select(r => r.RoleName).ToArray();
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            SecUtility.CheckParameter(ref username, true, false, true, 256, "username");
            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    return db.Roles_GetRolesForUser(this.ApplicationName, username).ToArray();
                }
                else
                {
                    var query = from u in db.Users
                                from r in u.Roles
                                where u.Username == username && u.Application.ApplicationID == this._AppID
                                select r.RoleName;

                    return query.ToArray();
                }
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            SecUtility.CheckParameter(ref roleName, true, true, true, 256, "roleName");
            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    return db.Roles_GetUsersInRole(this.ApplicationName, roleName).ToArray();
                }
                else
                {
                    var query = from u in db.Users
                                from r in db.Roles
                                where r.RoleName == roleName && r.Application.ApplicationID == this._AppID
                                select u.Username;

                    return query.ToArray();
                }
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            SecUtility.CheckParameter(ref roleName, true, true, true, 256, "roleName");
            SecUtility.CheckParameter(ref username, true, false, true, 256, "username");

            if (username.Length < 1)
                return false;

            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    return !string.IsNullOrEmpty(db.Roles_IsUserInRole(this.ApplicationName, roleName, username).FirstOrDefault());
                }
                else
                {
                    var query = from u in db.Users
                                from r in u.Roles
                                where r.RoleName == roleName
                                   && u.Username == username
                                   && r.Application.ApplicationID == this._AppID
                                select u.Username;

                    return query.Any();
                }
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            SecUtility.CheckArrayParameter(ref roleNames, true, true, true, 256, "roleNames");
            SecUtility.CheckArrayParameter(ref usernames, true, true, true, 256, "usernames");

            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    db.Roles_RemoveUsersFromRoles(this.ApplicationName, string.Join(",", usernames), string.Join(",", roleNames));
                }
                else
                {
                    foreach (var username in usernames)
                    {
                        var user = db.Users.Include("Roles").Where(u => u.Username == username && u.Application.ApplicationID == this._AppID).FirstOrDefault();
                        if (user != null)
                            foreach (var rolename in roleNames)
                            {
                                var role = user.Roles.FirstOrDefault(r => r.RoleName == rolename);
                                if (role != null)
                                    user.Roles.Remove(role);
                            }
                    }
                    db.SaveChanges();
                }
            }
        }

        public override bool RoleExists(string roleName)
        {
            SecUtility.CheckParameter(ref roleName, true, true, true, 256, "roleName");
            using (var db = this.Data)
            {
                if (this._UseSP)
                {
                    return !string.IsNullOrEmpty(db.Roles_IsRoleExists(this.ApplicationName, roleName).FirstOrDefault());
                }
                else
                {
                    return db.Roles.Where(r => r.RoleName == roleName && r.Application.ApplicationID == this._AppID).Any();
                }
            }
        }

    }
}
