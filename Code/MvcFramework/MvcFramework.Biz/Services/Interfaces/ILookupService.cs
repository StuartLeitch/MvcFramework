using System.Linq;
using MvcFramework.Biz.ViewModels;

namespace TecCvar.Biz.Services.Interfaces
{
    public interface ILookupService
    {
        LookupModel Lookups { get; }

        void ClearLookupCache();
    }
}