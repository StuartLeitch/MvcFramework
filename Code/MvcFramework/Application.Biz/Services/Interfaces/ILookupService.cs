using System.Linq;
using Application.Biz.ViewModels;

namespace Application.Biz.Services.Interfaces
{
    public interface ILookupService
    {
        LookupModel Lookups { get; }

        void ClearLookupCache();
    }
}