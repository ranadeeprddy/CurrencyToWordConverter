using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ConvertNumericToWords
{
    public static class Converter
    {
        enum Numbers
        {
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Eleven = 11,
            Twelve = 12,
            Thirteen = 13,
            Fourteen = 14,
            Fifteen = 15,
            Sixteen = 16,
            Seventeen = 17,
            Eighteen = 18,
            Nineteen = 19,
            Twenty = 20,
            Thirty = 30,
            Forty = 40,
            Fifty = 50,
            Sixty = 60,
            Seventy = 70,
            Eighty = 80,
            Ninety = 90,
            Hundred = 100,
            Thousand = 1000
        }


        enum NumberPlaceValues
        {
            [Description("")]
            Zero = 1,
            [Description(" Thousand ")]
            Thousand = 2,
            [Description(" Million ")]
            Million = 3,
            [Description(" Billion ")]
            Billion = 4,
            [Description(" Trillion ")]
            Trillion = 5
        }


        private static string GetEnumDescription(System.Enum value)
        {
            var enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            var descriptionAttribute =
                enumMember == null
                    ? default(DescriptionAttribute)
                    : enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            return
                descriptionAttribute == null
                    ? value.ToString()
                    : descriptionAttribute.Description;
        }

        public static string ConvertWholeNumbers(string currencyNumeric)
        {
            string value = null;
            var isNegative = "";

            if (currencyNumeric.Contains("-"))
            {
                isNegative = "Minus ";
                currencyNumeric = currencyNumeric.Substring(1, currencyNumeric.Length - 1);
            }

            if (!decimal.TryParse(currencyNumeric, out decimal verifiedCurrencyFormat))
                return value;
            else
                currencyNumeric = verifiedCurrencyFormat.ToString("0.00");

            int digitCount = currencyNumeric.IndexOf(".");
            var amount = currencyNumeric.Substring(0, digitCount);
            var pennies = currencyNumeric.Substring(digitCount + 1);

            if (Convert.ToInt32(amount) == 0)
                return NumberPlaceValues.Zero.ToString();

            for (int i = 1; i < 6; i++)
            {
                if (digitCount > 0)

                {
                    int startIndex = digitCount - 3 < 0 ? 0 : digitCount - 3;
                    int length = digitCount - 3 < 0 ? digitCount : 3;
                    if (Convert.ToInt32(amount.Substring(startIndex, length)) > 0)
                        value = ConvertNumericHundredsToWords(Convert.ToInt32(amount.Substring(startIndex, length))) + GetEnumDescription((NumberPlaceValues)i) + value;
                    digitCount -= length;
                }
            }

            return isNegative + value + $" AND {pennies}/100";
        }

        private static string ConvertNumericHundredsToWords(int amount)
        {
            string hundreds = null;
            int length = amount.ToString().Length;
            if (length >= 3 && amount > 0)
                hundreds = ((Numbers)((amount / 100) % 10)).ToString() + " Hundred ";

            hundreds += ConvertNumericTensToWords(amount % 100);

            return hundreds;
        }

        private static string ConvertNumericTensToWords(int amount)
        {
            int length = amount.ToString().Length;
            if (amount == 0)
                return null;
            string Tens = null;

            if (Enum.IsDefined(typeof(Numbers), amount))
            {
                Tens = ((Numbers)amount).ToString();
            }
            else if (amount % 10 == 0)
            {
                Tens += /*" And " +*/ ((Numbers)(amount % 100)).ToString();
            }
            else if (length >= 2 && amount.ToString().Length >= 2)
            {
                Tens += /*" And " +*/ ((Numbers)((amount / 10) * 10)).ToString() + "-" + ((Numbers)(amount % 10)).ToString();
            }
            else
            {
                Tens = ((Numbers)(amount % 10)).ToString();
            }

            return Tens;
        }


        enum Month
        {
            January = 1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }


        public static string ConvertDateToWords(DateTime dateTime)
        {
            string value = null;
            if (dateTime == null)
            {
                return null;
            }

            int dd = dateTime.Day;
            int mm = dateTime.Month;
            int yy = dateTime.Year;

            if ((new[] { 1, 01, 21, 31 }).Contains(dd))
            {
                value += $"{dd}st day of {GetEnumDescription((Month)Convert.ToInt32(mm))},{yy}";
            }
            else if ((new[] { 2, 02, 22 }).Contains(dd))
            {
                value += $"{dd}nd day of {GetEnumDescription((Month)Convert.ToInt32(mm))},{yy}";
            }
            else if ((new[] { 3, 03, 23 }).Contains(dd))
            {
                value += $"{dd}rd day of {GetEnumDescription((Month)Convert.ToInt32(mm))},{yy}";
            }
            else
            {
                value += $"{dd}th day of {GetEnumDescription((Month)Convert.ToInt32(mm))},{yy}";
            }

            return value;
        }


        #region 
        //if (digitCount > 0 && Convert.ToInt32(number) > 0)
        //{
        //    int startIndex = digitCount - 3 < 0 ? 0 : digitCount - 3;
        //    int length = digitCount - 3 < 0 ? digitCount : 3;
        //    value += ConvertNumericHundredsToWords(Convert.ToInt32(number.Substring(startIndex, length)), length);
        //    digitCount -= length;
        //}

        //if (digitCount > 0)
        //{
        //    int startIndex = digitCount - 3 < 0 ? 0 : digitCount - 3;
        //    int length = digitCount - 3 < 0 ? digitCount : 3;
        //    value = ConvertNumericHundredsToWords(Convert.ToInt32(number.Substring(startIndex, length)), length) + " Thousand " + value;
        //    digitCount -= length;
        //}

        //if (digitCount > 0)
        //{
        //    int startIndex = digitCount - 3 < 0 ? 0 : digitCount - 3;
        //    int length = digitCount - 3 < 0 ? digitCount : 3;
        //    value = ConvertNumericHundredsToWords(Convert.ToInt32(number.Substring(startIndex, length)), length) + " Million " + value;
        //    digitCount -= length;
        //}

        //if (digitCount > 0)
        //{
        //    int startIndex = digitCount - 3 < 0 ? 0 : digitCount - 3;
        //    int length = digitCount - 3 < 0 ? digitCount : 3;
        //    value = ConvertNumericHundredsToWords(Convert.ToInt32(number.Substring(startIndex, length)), length) + " Million " + value;
        //    //digitCount -= length;
        //}

        #endregion

    }
}
