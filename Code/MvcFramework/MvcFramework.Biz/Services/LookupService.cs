using System.Linq;
using Infrastructure.Core;
using MvcFramework.Biz.Data;
using MvcFramework.Biz.Properties;
using MvcFramework.Biz.Services.Interfaces;
using MvcFramework.Biz.ViewModels;

namespace MvcFramework.Biz.Services
{
    public class LookupService : ILookupService
    {
        private const string LookupCacheKey = "LookupCacheKey";
        private readonly CacheService<LookupModel> _cacheService;
        private readonly IGeneralRepository _repo;

        public LookupService(IGeneralRepository repo) {
            this._repo = repo;
            this._cacheService = new CacheService<LookupModel>();
        }

        public LookupModel Lookups { get { return this._cacheService.GetItem(LookupCacheKey, Settings.Default.LookupCacheAbsoluteExpiration, this.GetLookups); } }

        public void ClearLookupCache() {
            this._cacheService.ClearItem(LookupCacheKey);
        }

        private LookupModel GetLookups() {
            var lookupModel = new LookupModel
            { // Example
                    // Affiliates = this._repo.Db.Affiliates.ToDictionary(x => x.AffiliateId, x => x.Name),
            };

            return lookupModel;
        }
    }
}