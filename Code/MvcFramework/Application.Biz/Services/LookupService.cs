using System.Linq;
using Application.Core;
using Application.Biz.Data;
using Application.Biz.Properties;
using Application.Biz.Services.Interfaces;
using Application.Biz.ViewModels;

namespace Application.Biz.Services
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