using System.Linq;
using Application.Core.BaseClasses;
using Application.Biz.Data.EntityFramework;

namespace Application.Biz.Data
{
    public class GeneralRepository : BaseGeneralRepository, IGeneralRepository
    {
        private readonly IDomainContext _domainContext;

        public GeneralRepository(IDomainContext domainContext)
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