using System.Linq;
using MvcFramework.Biz.ViewModels;

namespace MvcFramework.Biz.Services.Interfaces
{
    public interface ILookupService
    {
        LookupModel Lookups { get; }

        void ClearLookupCache();
    }
}