using System.Linq;
using Application.Core.BaseClasses;
using Application.Biz.Data.EntityFramework;
using Ninject;

namespace Application.Biz.Data
{
    public class GeneralRepository : BaseGeneralRepository, IGeneralRepository
    {
        private readonly IDomainContext _domainContext;

        /// New up a GeneralRepository.  Don't do this directly - have Ninject take care of it (conventions depend on it).
        /// </summary>
        /// <param name="domainContext">Uses a contextual binding to identify that we want the version of the 
        /// DomainContext that is scoped to the request (every use with this name will be the same instance 
        /// within the request. Membership/Role/Filter providers have different lifetimes and need transient scope.
        /// https://github.com/ninject/ninject/wiki/Contextual-Binding </param>
        public GeneralRepository([Named("RequestScope")] IDomainContext domainContext)
        {
            this._domainContext = domainContext;
            this.DomainContext = this._domainContext;
        }

        public IDomainContext Db
        {
            get
            {
                return this._domainContext;
            }
        }
    }
}