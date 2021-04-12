using System;

namespace TechFlurry.Utils.Extentions.Numbers
{
    public static class Extentions
    {
        /// <summary>
        /// Convert an integer to a numeric word
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToWords(this int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + Math.Abs(number).ToWords();

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += (number / 1000000).ToWords() + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += (number / 1000).ToWords() + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += (number / 100).ToWords() + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
        /// <summary>
        /// Returns position string i.e 1st, 2nd, 3rd, 4th,....,nth
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToPosition(this int number)
        {
            number = Math.Abs(number);
            var position = number != 0 ? number + "th" : "zero";
            if (number == 1 || number % 10 == 1)
            {
                position = number + "st";
            }
            else if (number == 2 || number % 10 == 2)
            {
                position = number + "nd";
            }
            else if (number == 3 || number % 10 == 3)
            {
                position = number + "rd";
            }
            return position;
        }
        public static decimal ToDoubleRoundedFigures(this decimal number)
        {
            return decimal.Round(number, 2);
        }
        public static decimal ToDoubleRoundedFigures(this double number)
        {
            return decimal.Round(Convert.ToDecimal(number), 2);
        }
        public static string ToCurrenyFormat(this decimal number)
        {
            return string.Format("{0:#,#.00}", number);
        }
        public static string ToCurrenyFormat(this double number)
        {
            return string.Format("{0:#,#.00}", Convert.ToDecimal(number));
        }
    }
}
