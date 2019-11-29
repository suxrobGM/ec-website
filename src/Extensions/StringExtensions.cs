using System;
using System.Linq;

namespace EC_Website.Extensions
{
    public static class StringExtensions
    {
        public static string ToUpperPascalCase(this string input, bool ignoreSpaces = true)
        {
            var sentences = input.Split();
            var output = "";

            foreach (var sentence in sentences)
            {
                if (ignoreSpaces)
                {
                    output += sentence.ToUpperFirstLetter();
                }
                else
                {
                    output += " " + sentence.ToUpperFirstLetter();
                }               
            }

            return output;
        }

        public static string ToUpperFirstLetter(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input.Substring(1),
            };
        }
    }
}
