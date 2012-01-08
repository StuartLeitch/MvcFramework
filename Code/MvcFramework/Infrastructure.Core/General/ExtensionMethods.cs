using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Core
{
    public static class ExtensionMethods
    {
        public static string ConcatEnumerableToString(this IEnumerable<string> source, string delimiter)
        {
            var sb = new StringBuilder();
            foreach (var c in source)
            {
                sb.Append(c);

                if (source.LastOrDefault() != c)
                {
                    sb.Append(delimiter);
                }
            }

            return sb.ToString();
        }

        public static string CreateSlug(this string source)
        {
            source = source.Trim();
            source = Regex.Replace(source, "/[^a-zA-Z 0-9]+/g", "-");
            source = source.Replace(" ", "-");
            source = source.Replace("---", "-");
            source = source.Replace("--", "-");

            return source.ToLower();
        }

        public static string ToDateSlug(this DateTime source)
        {
            return source.ToString("M.dd.yy");
        }

        /// <summary>
        ///   Converts a date to 23:59.99999...
        /// </summary>
        /// <param name = "date"></param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime date)
        {
            return date.Midnight().AddDays(1).Subtract(new TimeSpan(0, 0, 1));
        }

        /// <summary>
        ///   GT Extension Method
        ///   Convert a PascalCase or camel_case formatted string to a space-separated string of words
        /// </summary>
        /// <param name = "source"></param>
        /// <returns></returns>
        public static string HumanizeString(this string source)
        {
            var sb = new StringBuilder();

            var last = char.MinValue;
            foreach (var c in source)
            {
                if (char.IsLower(last) && char.IsUpper(c))
                {
                    sb.Append(' ');
                }

                sb.Append(c);
                last = c;
            }

            return sb.ToString();
        }

        /// <summary>
        ///   Converts a date to midnight by subtracting the time of day
        /// </summary>
        /// <param name = "date"></param>
        /// <returns></returns>
        public static DateTime Midnight(this DateTime date)
        {
            return date.Subtract(date.TimeOfDay);
        }


        /// <summary>
        ///   Truncates a string by a certain number of characters, then appends a set of ellipses at the end. 
        ///   Nothing fancy
        ///   like evaluating the end of the word, will just cut off mid word if falls on count index.
        /// </summary>
        /// <param name = "date"></param>
        /// <returns></returns>
        public static string TruncateWithEllipses(this string source, int count)
        {
            var result = string.Empty;

            if (!string.IsNullOrEmpty(source))
            {
                if (source.Length > count)
                {
                    result = source.Substring(0, count - 4) + "...";
                }
                else
                {
                    result = source;
                }
            }

            return result;
        }
    }
}