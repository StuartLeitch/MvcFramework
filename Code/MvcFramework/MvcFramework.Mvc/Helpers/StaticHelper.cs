using System.Linq;
using MvcFramework.Biz.Services.Interfaces;
using MvcFramework.Biz.ViewModels;

namespace MvcFramework.Mvc.Helpers
{
    /// <summary>
    ///   Designed to be used within Razor views (for convenience - saves some plumbing). (Review if value offsets increased concept count).
    /// </summary>
    public static class StaticHelper
    {
        public static LookupModel Lookups
        {
            get
            {
                {
                    if (MockLookupModel != null)
                        return MockLookupModel;

                    var lookupService = DIFactory.Resolve<ILookupService>();
                    return lookupService.Lookups;
                }
            }
        }

        /// <summary>
        ///   Set this in testing to mock out the lookup helper.
        /// </summary>
        public static LookupModel MockLookupModel { get; set; }
    }
}