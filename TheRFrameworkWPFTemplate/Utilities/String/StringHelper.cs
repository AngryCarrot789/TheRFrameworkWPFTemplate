using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TheRFramework.Utilities.String
{
    public static class StringHelper
    {
        /// <summary>
        ///     Extracts the values (between the text of a and b) within value.
        ///     It returns the first occourance of the values of a and b.
        /// <code>
        ///     Example: "do you have permissions, do you?".Between("you", "ion"); returns " have permiss";
        /// </code>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string Between(this string value, string a, string b)
        {
            try
            {
                int posA = value.IndexOf(a);
                int posB = value.LastIndexOf(b);

                if (posA == -1) return "";
                if (posB == -1) return "";

                int posAFull = posA + a.Length;
                if (posAFull >= posB) return "";

                return value.Substring(posAFull, posB - posAFull);
            }
            catch { return ""; }
        }

        /// <summary>
        /// Gets the text before <see cref="beforeThis"/> within <see cref="value"/>
        /// <code>
        ///     Example: "hi there lol".Before("ere"); returns "hi th";
        /// </code>
        /// </summary>
        public static string Before(this string value, string beforeThis)
        {
            try
            {
                int posBefore = value.IndexOf(beforeThis);

                if (posBefore == -1)
                    return "";

                return value.Substring(0, posBefore);
            }
            catch { return ""; }
        }

        /// <summary>
        /// Gets the text after <see cref="afterThis"/> within <see cref="value"/>
        /// <code>
        ///     Example: "hi there lol hehe".After("he"); returns "re lol hehe";
        /// </code>
        /// </summary>
        public static string After(this string value, string afterThis)
        {
            try
            {
                int posAfter = value.LastIndexOf(afterThis);

                if (posAfter == -1)
                    return "";

                int posAfterFull = posAfter + afterThis.Length;

                if (posAfterFull >= value.Length)
                    return "";

                return value.Substring(posAfterFull);
            }
            catch { return ""; }
        }

        /// <summary>
        /// Returns true if the text is null or empty
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        /// <summary>
        /// Repeats <see cref="value"/>, <see cref="n"/> number of times
        /// </summary>
        /// <param name="value">The value to be repeated</param>
        /// <param name="n">The number of times to repeat the value</param>
        /// <returns>A new string with the repeated text</returns>
        public static string Repeat(this string value, int n)
        {
            string newValue = "";
            for(int i = 0; i < n; i++)
            {
                newValue += value;
            }
            return newValue;
        }
    }
}
