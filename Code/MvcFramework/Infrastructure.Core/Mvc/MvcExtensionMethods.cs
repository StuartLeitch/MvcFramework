// Note: this deliberately uses the MVC namespace 
namespace System.Web.Mvc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class MvcExtensionMethods
    {
        // DOCUMENT
        public static IEnumerable<SelectListItem> ToSelectListItems<T>(
            this IEnumerable<T> items, Func<T, string> valueSelector, Func<T, string> nameSelector, Func<T, bool> selected, bool suppressSortingOnText = false)
        {
            if (!suppressSortingOnText)
            {
                items = items.OrderBy(nameSelector);
            }

            return items.Select(item => new SelectListItem { Value = valueSelector(item), Text = nameSelector(item), Selected = selected(item), });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(this IEnumerable<KeyValuePair<int, string>> items, int? selectedKey = null)
        {
            if (selectedKey.HasValue)
            {
                return items.ToSelectListItems(x => x.Key.ToString(), x => x.Value, x => x.Key == selectedKey.Value, true);
            }

            return items.ToSelectListItems(x => x.Key.ToString(), x => x.Value, x => false, true);
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(this IEnumerable<KeyValuePair<string, string>> items, string selectedKey = null)
        {
            if (selectedKey != null)
            {
                return items.ToSelectListItems(x => x.Key.ToString(), x => x.Value, x => x.Key == selectedKey, true);
            }

            return items.ToSelectListItems(x => x.Key.ToString(), x => x.Value, x => false, true);
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(this IEnumerable<KeyValuePair<int, int>> items, int? selectedKey = null)
        {
            if (selectedKey.HasValue)
            {
                return items.ToSelectListItems(x => x.Key.ToString(), x => x.Value.ToString(), x => x.Key == selectedKey.Value, true);
            }

            return items.ToSelectListItems(x => x.Key.ToString(), x => x.Value.ToString(), x => false, true);
        }
    }
}