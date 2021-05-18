using System;
using System.Linq;

namespace TechFlurry.Utils.Extensions.Strings
{
    public static class Extensions
    {
        /// <summary>
        /// Converts a string to first word as uppercase other small
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns></returns>
        public static string ToInitialCapital(this string s)
        {
            try
            {
                var words = s.Split(' ');
                if (words.Length > 1)
                {
                    for (int i = 0; i < words.Length; i++)
                    {
                        words[i] = words[i].ToInitialCapital();
                    }
                    return string.Join(" ", words);
                }
                else
                {
                    string firstLetterCapital = s.Substring(0, 1).ToUpper();
                    string restWordSmall = s.Substring(1).ToLower();
                    return firstLetterCapital + restWordSmall;
                }
            }
            catch (Exception)
            {
                return s;
            }
        }
        public static string GenerateElementId(this Guid guid)
        {
            return guid.ToString().Replace("-", string.Empty);
        }
        public static string GetUntilOrEmpty(this string text, string stopAt)
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }
        /// <summary>
        /// Converts a PascalCase string to and UNDERSCORE_CASE
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToUnderscoreCase(this string s)
        {
            return string.Concat(s.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));
        }
    }
}
