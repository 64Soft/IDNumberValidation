using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNumberValidation
{
    /// <summary>
    /// Defines common extension methods
    /// </summary>
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Adds lead characters to a string
        /// </summary>
        /// <param name="source">Source string</param>
        /// <param name="leadChar">Lead character to add</param>
        /// <param name="maxLength">Maximum length of the total string</param>
        /// <returns></returns>
        public static string AddLeadCharacters(this string source, char leadChar, int maxLength)
        {
            StringBuilder sb = new StringBuilder(source);

            while (sb.Length < maxLength)
            {
                sb.Insert(0, leadChar);
            }

            return sb.ToString();
        }
    }
}
