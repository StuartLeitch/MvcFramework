using System.IO;
using System.Linq;
using System.Reflection;

namespace MvcFramework.Biz.Reporting
{
    public class ReportHelper
    {
        /// <summary>
        ///   Gets
        /// </summary>
        /// <param name = "reportName">Name of the report without the extension</param>
        /// <param name = "physicalApplicationPath">Get from this.Request.PhysicalApplicationPath</param>
        /// <returns>A stream that loads: reportViewerName.LocalReport.LoadReportDefinition(stream); </returns>
        public Stream GetReportStreamFromResource(string reportName, string physicalApplicationPath)
        {
            var assembly = Assembly.LoadFrom(physicalApplicationPath + @"\bin\MvcFramework.Biz.dll");
            var stream = assembly.GetManifestResourceStream("MvcFramework.Biz.Reporting." + reportName + ".rdlc");

            return stream;
        }
    }
}