// Number to Words Ref: https://www.c-sharpcorner.com/article/convert-numeric-value-into-words-currency-in-c-sharp/

using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Msi.UtilityKit
{
    public static class NumberUtilities
    {

        public static long GetUniqueNumber()
        {
            var length = 32;
            var bytes = new byte[length];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(bytes);
            var random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return random;
        }

        public static IEnumerable<int> To(this int from, int to)
        {
            if (to >= from)
            {
                for (int i = from; i <= to; i++)
                {
                    yield return i;
                }
            }
            else
            {
                for (int i = from; i >= to; i--)
                {
                    yield return i;
                }
            }
        }

        public static IEnumerable<T> Times<T>(this int num, T toReturn)
        {
            for (int i = 0; i < num; i++)
            {
                yield return toReturn;
            }
        }

        public static IEnumerable<T> Times<T>(this int num, Func<int, T> block)
        {
            for (int i = 0; i < num; i++)
            {
                yield return block(i);
            }
        }

        public static string ToWords(this string number)
        {
            double _number = Convert.ToDouble(number);
            return ToWords(_number);
        }

        public static string ToWords(this double number)
        {
            string word = string.Empty;
            try
            {
                bool beginsZero = false; // tests for 0XX  
                bool isDone = false; // test if already translated  
                string numberString = number.ToString();
                if (number > 0)
                {
                    // test for zero or digit zero in a nuemric  
                    beginsZero = numberString.StartsWith("0");

                    int numberOfDigits = numberString.Length;
                    int pos = 0;  //store digit grouping  
                    String place = string.Empty; // digit grouping name:hundres,thousand,etc...  
                    switch (numberOfDigits)
                    {
                        case 1: // ones' range  
                            word = SingleDigitToWord(number);
                            isDone = true;
                            break;
                        case 2://tens' range  
                            word = DoubleDigitToWords(number);
                            isDone = true;
                            break;
                        case 3: // hundreds' range  
                            pos = (numberOfDigits % 3) + 1;
                            place = " Hundred ";
                            break;
                        case 4: // thousands' range  
                        case 5:
                        case 6:
                            pos = (numberOfDigits % 4) + 1;
                            place = " Thousand ";
                            break;
                        case 7://millions' range  
                        case 8:
                        case 9:
                            pos = (numberOfDigits % 7) + 1;
                            place = " Million ";
                            break;
                        case 10://Billions's range  
                        case 11:
                        case 12:

                            pos = (numberOfDigits % 10) + 1;
                            place = " Billion ";
                            break;
                        // add extra case options for anything above Billion...  
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)  
                        if (numberString.Substring(0, pos) != "0" && numberString.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ToWords(numberString.Substring(0, pos)) + place + ToWords(numberString.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ToWords(numberString.Substring(0, pos)) + ToWords(numberString.Substring(pos));
                        }

                        // check for trailing zeros  
                        // if (beginsZero) word = " and " + word.Trim();  
                    }
                    // ignore digit grouping names  
                    if (word.Trim().Equals(place.Trim())) word = string.Empty;
                }
            }
            catch { }
            return word.Trim();
        }

        public static string DoubleDigitToWords(this string doubleDigit)
        {
            int _doubleDigit = Convert.ToInt32(doubleDigit);
            return DoubleDigitToWords(_doubleDigit);
        }

        public static string DoubleDigitToWords(this double doubleDigit)
        {
            string name = string.Empty;
            string doubleDigitString = doubleDigit.ToString();
            switch (doubleDigit)
            {
                case 10:
                    name = "Ten";
                    break;
                case 11:
                    name = "Eleven";
                    break;
                case 12:
                    name = "Twelve";
                    break;
                case 13:
                    name = "Thirteen";
                    break;
                case 14:
                    name = "Fourteen";
                    break;
                case 15:
                    name = "Fifteen";
                    break;
                case 16:
                    name = "Sixteen";
                    break;
                case 17:
                    name = "Seventeen";
                    break;
                case 18:
                    name = "Eighteen";
                    break;
                case 19:
                    name = "Nineteen";
                    break;
                case 20:
                    name = "Twenty";
                    break;
                case 30:
                    name = "Thirty";
                    break;
                case 40:
                    name = "Fourty";
                    break;
                case 50:
                    name = "Fifty";
                    break;
                case 60:
                    name = "Sixty";
                    break;
                case 70:
                    name = "Seventy";
                    break;
                case 80:
                    name = "Eighty";
                    break;
                case 90:
                    name = "Ninety";
                    break;
                default:
                    if (doubleDigit > 0)
                    {
                        name = SingleDigitToWord(doubleDigitString.Substring(0, 1) + "0") + " " + SingleDigitToWord(doubleDigitString.Substring(1));
                    }
                    break;
            }
            return name;
        }

        public static string SingleDigitToWord(this string singleDigit)
        {
            int _singleDigit = Convert.ToInt32(singleDigit);
            return SingleDigitToWord(_singleDigit);
        }

        public static string SingleDigitToWord(this double singleDigit)
        {
            string name = string.Empty;
            switch (singleDigit)
            {
                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
            return name;
        }

    }
}
