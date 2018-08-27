using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Msi.UtilityKit
{
    public static class StringUtilities
    {

        public static String RemoveSpecialChars(this string value)
        {
            var newValue = value;
            //Checks for last character is special charact
            var regexItem = new Regex("[^a-zA-Z0-9_.]+");
            //remove last character if its special
            if (regexItem.IsMatch(value[value.Length - 1].ToString()))
            {
                newValue = value.Remove(value.Length - 1);
            }
            string replaceStr = Regex.Replace(newValue, "[^a-zA-Z0-9_]+", "_");

            return replaceStr;

        }

        public static string ToUpperFirstLetter(this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        public static string Replace(this string str, char item, Func<char> character)
        {
            StringBuilder builder = new StringBuilder(str.Length);

            foreach (char c in str.ToCharArray())
            {
                builder.Append(c == item ? character() : c);
            }

            return builder.ToString();
        }

        public static string Numerify(this string numberString)
        {
            return numberString.Replace('#', () => new Random().Next(10).ToString().ToCharArray()[0]);
        }

        public static string Letterify(this string letterString)
        {
            return letterString.Replace('?', () => 'a'.To('z').Rand());
        }

        public static string Bothify(this string str)
        {
            return Letterify(Numerify(str));
        }

        public static IEnumerable<char> To(this char from, char to)
        {
            for (char i = from; i <= to; i++)
            {
                yield return i;
            }
        }

    }
}
