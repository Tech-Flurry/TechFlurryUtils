using System;

namespace TechFlurry.Utils.Extensions.Numbers
{
    public static class Extensions
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
        public static string ToNumberedDigits(this int number, int digits = 1)
        {
            if (number == 0)
            {
                return number.ToString();
            }
            var format = string.Empty;
            for (int i = 0; i < digits; i++)
            {
                format += "0";
            }
            return number.ToString(format);
        }
        public static int ToNearestMultiple(this int number, int factor)
        {
            if (number % factor != 0)
            {
                int nearestMultiple = (int)Math.Round((number / (double)factor), MidpointRounding.AwayFromZero) * factor;
                return nearestMultiple;
            }
            else
            {
                return number;
            }
        }
        public static int ToNearestMultiple(this int number, int factor, int endsTo, int startsFrom = 0)
        {
            var multiple = number.ToNearestMultiple(factor);
            return multiple.ToRange(startsFrom, endsTo);
        }
        public static int ToRange(this int number, int startsFrom, int endsTo)
        {
            number = Math.Max(number, startsFrom);
            number = Math.Min(number, endsTo);
            return number;
        }
        public static short ToRange(this short number, short startsFrom, short endsTo)
        {
            number = Math.Max(number, startsFrom);
            number = Math.Min(number, endsTo);
            return number;
        }
        public static long ToRange(this long number, long startsFrom, long endsTo)
        {
            number = Math.Max(number, startsFrom);
            number = Math.Min(number, endsTo);
            return number;
        }
        public static decimal ToRange(this decimal number, decimal startsFrom, decimal endsTo)
        {
            number = Math.Max(number, startsFrom);
            number = Math.Min(number, endsTo);
            return number;
        }
        public static double ToRange(this double number, double startsFrom, double endsTo)
        {
            number = Math.Max(number, startsFrom);
            number = Math.Min(number, endsTo);
            return number;
        }

        public static bool IsNumericType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
