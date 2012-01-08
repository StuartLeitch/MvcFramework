using System.Linq;
using System;
using System.Diagnostics;

namespace Infrastructure.Core
{
    /// <summary>
    ///   Helper methods to make it easier to throw exceptions.
    /// </summary>
    [DebuggerStepThrough]
    public static class ThrowHelper
    {
        /// <summary>
        /// Throws an ArgumentOutOfRangeException if negative
        /// </summary>
        /// <param name="argument">int to test</param>
        /// <param name="name">Name of variable (needed to insert in exception)</param>
        public static void ThrowIfNegative(this int argument, string name)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(name);
            }
        }

        /// <summary>
        /// Throws an ArgumentNullException if null and ArgumentOutOfRangeException if negative
        /// </summary>
        /// <param name="argument">int? to test</param>
        /// <param name="name">Name of variable (needed to insert in exception)</param>
        public static void ThrowIfNegative(this int? argument, string name)
        {
            if (!argument.HasValue)
            {
                throw new ArgumentNullException(name);
            }

            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(name);
            }
        }


        /// <summary>
        /// Throws an ArgumentNullException if null and ArgumentOutOfRangeException if default
        /// </summary>
        /// <param name="argument">Struct? to test</param>
        /// <param name="name">Name of variable (needed to insert in exception)</param>
        public static void ThrowIfDefault<T>(this T? argument, string name) where T : struct, IComparable
        {
            if (!argument.HasValue)
            {
                throw new ArgumentNullException(name);
            }

            if (argument.Value.CompareTo(default(T)) == 0)
            {
                throw new ArgumentOutOfRangeException(name);
            }
        }

        /// <summary>
        /// Throws an ArgumentNullException if null and ArgumentOutOfRangeException if default
        /// </summary>
        /// <param name="argument">Struct to test</param>
        /// <param name="name">Name of variable (needed to insert in exception)</param>
        public static void ThrowIfDefault<T>(this T argument, string name) where T : struct, IComparable
        {
            if (argument.CompareTo(default(T)) == 0)
            {
                throw new ArgumentOutOfRangeException(name);
            }
        }



        /// <summary>
        /// Throws an ArgumentOutOfRangeException if not positive
        /// </summary>
        /// <param name="argument">int to test</param>
        /// <param name="name">Name of variable (needed to insert in exception)</param>
        public static void ThrowIfNonPositive(this int argument, string name)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(name);
            }
        }

        /// <summary>
        /// Throws an ArgumentNullException if null and ArgumentOutOfRangeException if not positive
        /// </summary>
        /// <param name="argument">int? to test</param>
        /// <param name="name">Name of variable (needed to insert in exception)</param>
        public static void ThrowIfNonPositive(this int? argument, string name)
        {
            if (!argument.HasValue)
            {
                throw new ArgumentNullException(name);
            }

            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(name);
            }
        }

        /// <summary>
        /// Throws an ArgumentNullException if not positive
        /// </summary>
        /// <param name="argument">object to test</param>
        /// <param name="name">Name of variable (needed to insert in exception)</param>
        public static void ThrowIfNull<T>(this T argument, string name) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Throws a DisplayUserException if null
        /// </summary>
        /// <param name="argument">object to test</param>
        /// <param name="message">Message to be displayed to User</param>
        public static void ThrowUserExIfNull<T>(this T argument, string message) where T : class
        {
            if (argument == null)
            {
                throw new DisplayUserException(message);
            }
        }


        public static void ThrowIfNull<T>(this T? argument, string name) where T : struct
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void ThrowIfNullOrEmpty(this string argument, string name) 
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        ///   Checks to see that one and only one parameter has a value.  Throws an exception if false.
        /// </summary>
        public static void ThrowIfNotXOR<T1,T2>(T1? param1, T2? param2) where T1 : struct where T2 : struct
        {
            if (!(param1.HasValue ^ param2.HasValue))
            {
                throw new ArgumentException("One and only one argument may be populated");
            }
        }
    }
}