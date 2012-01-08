using System;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using MvcFramework.Biz.Reporting;
using MvcFramework.Biz.Services.Interfaces;

namespace MvcFramework.Mvc.WebFormReportHosts
{
    public partial class GeneralReportHost : Page
    {
        private IReportService _reportService;

        public void DisableUnwantedExportFormats()
        {
            foreach (var extension in this.ReportViewer1.LocalReport.ListRenderingExtensions())
            {
                if (extension.Name == "WORD" || extension.Name == "Excel")
                    this.ReflectivelySetVisibilityFalse(extension);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this._reportService = DIFactory.Resolve<IReportService>();
        }

        protected void ReportViewer1_PreRender(object sender, EventArgs e)
        {
            // this.DisableUnwantedExportFormats();
        }

        protected void btnEXAMPLE_Click(object sender, EventArgs e)
        {
            //var model = this._reportService.GetCallLogById(callLogId);
            //this.LoadGenericReport(model, "CallLog", "CallLog");
        }

        private void LoadGenericReport(object model, string reportName, string downloadName)
        {
            this.ReportViewer1.Reset();
            this.ReportViewer1.LocalReport.DataSources.Clear();

            var reportLoader = new ReportHelper();

            this.ReportViewer1.LocalReport.LoadReportDefinition(
                reportLoader.GetReportStreamFromResource(reportName, this.Request.PhysicalApplicationPath));

            this.ReportViewer1.LocalReport.DisplayName = downloadName;

            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource { Name = "Model", Value = model });

            this.ReportViewer1.LocalReport.Refresh();
            this.SetMarginsAndOrientation();

            this.ReportViewer1.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer1.LocalReport.EnableExternalImages = true;
        }

        private void ReflectivelySetVisibilityFalse(RenderingExtension extension)
        {
            var info = extension.GetType().GetField("m_isVisible", BindingFlags.NonPublic | BindingFlags.Instance);
            if (info != null)
            {
                var actualExtension = info.GetValue(extension);
                if (actualExtension != null)
                    info.SetValue(extension, false);
            }

            this.SetMarginsAndOrientation();
        }

        private string ResolveImageRelativePaths(string relativeImagePath)
        {
            return !string.IsNullOrWhiteSpace(relativeImagePath)
                       ? string.Format(
                           "{0}://{1}{2}",
                           this.Request.Url.Scheme,
                           this.Request.Url.Authority,
                           this.Page.ResolveUrl(relativeImagePath))
                       : string.Empty;
        }

        private void SetMarginsAndOrientation()
        {
            var newPageSettings = new PageSettings { Margins = new Margins(25, 25, 35, 35) };
            this.ReportViewer1.SetPageSettings(newPageSettings);
        }
    }
}