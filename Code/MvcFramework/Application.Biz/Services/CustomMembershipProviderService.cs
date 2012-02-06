using System.Linq;
using Application.Biz.Data;
using Application.Biz.Data.EntityFramework;
using Application.Biz.Services.Interfaces;
using Application.Core.BaseClasses;
using Ninject;

namespace Application.Biz.Services
{
    /// <summary>
    /// Because the lifetime of this object exceeds the request lifetime, it is excluded
    /// from participating in the conventions the rest of the site uses.
    /// Note it does not use IGeneralRepository or inherit from ServiceBase.
    /// Note: Keep this class contained to functionality required by CustomMembershipProvider
    /// </summary>
    public class CustomMembershipProviderService : ICustomMembershipProviderService
    {
        private readonly IDomainContext _domainContext;

        /// <summary>
        /// Because Membership Providers outlive the request lifetime, don't have Ninject dispose of 
        /// the DomainContext.  Also, 
        /// </summary>
        /// <param name="domainContext"></param>
        public CustomMembershipProviderService([Named("Transient")] IDomainContext domainContext)
        {
            this._domainContext = domainContext;
        }

        public bool ChangePassword(string name, string oldPassword, string newPassword)
        {
            var user = this._domainContext.People.SingleOrDefault(x => x.UserName == name && x.Password == oldPassword);
            if (user == null)
            {
                return false;
            }

            user.Password = newPassword;

            return this._domainContext.SaveChanges() != 0;
        }

        public bool IsCurrentMember() {
            // TODO Stuart: Move to other class (because of different DbContext)
            // TODO Stuart: Implement
            return false;
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (this._domainContext.People.Any(x => x.UserName == userName))
                return MembershipCreateStatus.DuplicateUserName;

            if (this._domainContext.People.Any(x => x.EmailAddress == email))
                return MembershipCreateStatus.DuplicateEmail;

            var user = new Person { UserName = userName, Password = password };
            this._domainContext.People.Add(user);

            this._domainContext.SaveChanges();

            return MembershipCreateStatus.Success;
        }

        public Person GetUser(string userName)
        {
            return this._domainContext.People.SingleOrDefault(x => x.UserName == userName);
        }

        public bool ValidateUser(string userName, string password)
        {
            return this._domainContext.People.Any(x => x.UserName == userName && x.Password == password);
        }

    }
}