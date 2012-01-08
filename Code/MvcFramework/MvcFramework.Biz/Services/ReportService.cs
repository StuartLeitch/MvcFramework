using System.Linq;
using Infrastructure.Core.BaseClasses;
using MvcFramework.Biz.Data;
using MvcFramework.Biz.Services.Interfaces;

namespace MvcFramework.Biz.Services
{
    public class ReportService : BaseService, IReportService
    {
        private readonly IGeneralRepository _repo;

        public ReportService(IGeneralRepository repo)
        {
            this._repo = repo;
        }
    }
}