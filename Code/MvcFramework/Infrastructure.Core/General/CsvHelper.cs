using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace Infrastructure.Core.General
{
    public static class CsvHelper
    {
        public static string ToCsvFromProperties<T>(string separator, IEnumerable<T> objectlist)
        {
            var t = typeof(T);
            var properties = t.GetProperties();

            var header = String.Join(separator, properties.Select(f => f.Name).ToArray());

            var sb = new StringBuilder();
            sb.AppendLine(header);

            foreach (var o in objectlist)
                sb.AppendLine(ToCsvProperties(separator, properties, o));

            return sb.ToString();
        }

        public static string ToCsvProperties(string separator, PropertyInfo[] properties, object o)
        {
            var sb = new StringBuilder();

            foreach (var p in properties)
            {
                if (sb.Length > 0)
                    sb.Append(separator);

                var x = p.GetValue(o,null);

                if (x != null)
                    sb.Append("\"" + x.ToString().Replace("\"", "") + "\"");
            }

            return sb.ToString();
        }

        public static string ToCsvFromFields<T>(string separator, IEnumerable<T> objectlist)
        {
            var t = typeof(T);
            var fields = t.GetFields();

            var header = String.Join(separator, fields.Select(f => f.Name).ToArray());

            var sb = new StringBuilder();
            sb.AppendLine(header);

            foreach (var o in objectlist)
                sb.AppendLine(ToCsvFields(separator, fields, o));

            return sb.ToString();
        }

        public static string ToCsvFields(string separator, FieldInfo[] fields, object o)
        {
            var sb = new StringBuilder();

            foreach (var f in fields)
            {
                if (sb.Length > 0)
                    sb.Append(separator);

                var x = f.GetValue(o);

                if (x != null)
                    sb.Append("\"" + x.ToString().Replace("\"", "") + "\"");
            }

            return sb.ToString();
        }

        public static FileContentResult ToCsvFileContentResultFromProperties<T>(string separator, IEnumerable<T> objectlist, string downloadFileName)
        {
            var contents = Encoding.UTF8.GetBytes(ToCsvFromProperties(separator, objectlist));
            return new FileContentResult(contents,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = downloadFileName };  
        }


    }
}
