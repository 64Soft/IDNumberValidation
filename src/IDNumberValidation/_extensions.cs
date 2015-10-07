using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IDNumberValidation
{
    /// <summary>
    /// Defines common extension methods
    /// </summary>
    public static class Extensions
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

        public static string ToNumericOnly(this string s)
        {
            return Regex.Replace(s, "\\D", "");
        }

        public static string ToAlphaNumericOnly(this string s)
        {
            return Regex.Replace(s, "\\W", "");
        }

        public static string StripAlphasFromBeginning(this string s)
        {
            return Regex.Replace(s, "^\\D+", "");
        }

        public static string TrimStart(this string target, string trimString)
        {
            string result = target;
            while (result.StartsWith(trimString))
            {
                result = result.Substring(trimString.Length);
            }

            return result;
        }

        public static string TrimEnd(this string target, string trimString)
        {
            string result = target;
            while (result.EndsWith(trimString))
            {
                result = result.Substring(0, result.Length - trimString.Length);
            }

            return result;
        }
    }
}
