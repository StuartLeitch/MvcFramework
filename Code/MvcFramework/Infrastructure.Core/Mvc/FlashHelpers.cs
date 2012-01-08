using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Mvc;

// Note: this deliberately uses the MVC namespace 
namespace System.Web.Mvc
{
    [DebuggerStepThrough]
    public static class FlashHelpers
    {
        public const string Error = "error";
        public const string Info = "info";
        public const string Warning = "warning";

        public static string Flash(this HtmlHelper helper)
        {
            var flashEntries =
                    helper.ViewContext.TempData.Where(
                            x => x.Key.StartsWith(Error) || x.Key.StartsWith(Warning) || x.Key.StartsWith(Info)).OrderBy(x => x.Key);

            if (flashEntries.Count() == 0)
            {
                return string.Empty;
            }


            var sb = new StringBuilder();
            sb.AppendLine("<script>");
            sb.AppendLine("$(document).ready(function() {");
            
            foreach (var flashEntry in flashEntries)
            {

                ComposeDetails(helper,sb, flashEntry);

            }

            sb.AppendLine("});");
            sb.AppendLine("</script>");

            return sb.ToString();
        }

        private static string ComposeDetails(HtmlHelper helper, StringBuilder sb, KeyValuePair<string, object> flashEntry)
        {
            string className = null;
            if (flashEntry.Key.StartsWith(Info))
                className = Info;
            if (flashEntry.Key.StartsWith(Warning))
                className = Warning;
            if (flashEntry.Key.StartsWith(Error))
                className = Error;

            if(string.IsNullOrEmpty(className))
                throw new NullReferenceException("className Should never be empty");


            if (!String.IsNullOrEmpty(flashEntry.Value.ToString()))
            {
                sb.AppendFormat(@"$(""<div id='{0}' style='display: none'>{1}<div>"").appendTo('#flash-container');", 
                                   flashEntry.Key, helper.Encode(flashEntry.Value.ToString().Replace(Environment.NewLine, " ")));
                sb.AppendLine();
                sb.AppendFormat("$('#{0}').toggleClass('{1}');", flashEntry.Key, className);
                sb.AppendLine();
                if (className == Info)
                {
                    sb.AppendFormat("$('#{0}').slideDown('slow').delay(4000).slideUp(1000);", flashEntry.Key);
                    sb.AppendLine();
                }
                else
                {
                    sb.AppendFormat("$('#{0}').slideDown('slow');", flashEntry.Key);
                    sb.AppendLine();
                }

                sb.AppendLine("$('#" + flashEntry.Key + "').click(function(){$('#" + flashEntry.Key + "').toggle('highlight')});");
            }

            return sb.ToString();

        }

        public static void FlashError(this Controller controller, string message)
        {

            FlashMessage(controller, Error, message);
        }

        public static void FlashErrorIfNull(this Controller controller, object objectToTest, string message)
        {
            if (objectToTest == null)
            {
                FlashError(controller, message);
            }
        }

        public static void FlashInfo(this Controller controller, string message)
        {
            FlashMessage(controller, Info, message);
        }

        public static void FlashWarning(this Controller controller, string message)
        {
            FlashMessage(controller, Warning, message);
        }

        private static void FlashMessage(Controller controller, string keyPrefix, string message, int number = 1)
        {
            var key = keyPrefix + number;
            if (controller.TempData[key] == null)
            {
                controller.TempData[key] = message;
            }
            else if (number < 4)
            {
                FlashMessage(controller, keyPrefix, message, number + 1);
            }
            else
            {
                controller.TempData[key] = "ADDITIONAL MESSAGES EXIST BUT HAVE BEEN SUPPRESSED.";
            }
        }
    }
}