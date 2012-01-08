﻿using System.Linq;
using Infrastructure.Core;
using TecCvar.Biz.Services.Interfaces;
using MvcFramework.Biz.Data;
using MvcFramework.Biz.Properties;
using MvcFramework.Biz.ViewModels;

namespace TecCvar.Biz.Services
{

    public class LookupService : ILookupService
    {
        #region "Regular"

        private const string LookupCacheKey = "LookupCacheKey";
        private readonly CacheService<LookupModel> _cacheService;
        private readonly IGeneralRepository _repo;

        public LookupService(IGeneralRepository repo)
        {
            this._repo = repo;
            this._cacheService = new CacheService<LookupModel>();
        }

        public LookupModel Lookups
        {
            get
            {
                return this._cacheService.GetItem(
                    LookupCacheKey,
                    Settings.Default.LookupCacheAbsoluteExpiration,
                    GetLookups);
            }
        }

        public void ClearLookupCache()
        {
            this._cacheService.ClearItem(LookupCacheKey);
        }

        #endregion

        private LookupModel GetLookups()
        {
            var lookupModel = new LookupModel
                {
                    // Example
                   // Affiliates = this._repo.Db.Affiliates.ToDictionary(x => x.AffiliateId, x => x.Name),
                };

            return lookupModel;
        }
    }
}