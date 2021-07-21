using System;
using System.Linq;

namespace ExtensionNS
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string text)
        {
            if (!String.IsNullOrEmpty(text))
                return text.First().ToString().ToUpper() + text[1..];
            else
                throw new ArgumentNullException($"{nameof(text)} null or empty!");
        }

        public static bool HasSpecialChar(this string text)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{};'<>_,";
            foreach (var item in specialChar)
            {
                if (text.Contains(item)) return true;
            }

            return false;
        }

    }
}
