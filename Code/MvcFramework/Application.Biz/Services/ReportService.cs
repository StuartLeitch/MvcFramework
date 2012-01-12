using System.Linq;
using Application.Core.BaseClasses;
using Application.Biz.Data;
using Application.Biz.Services.Interfaces;

namespace Application.Biz.Services
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