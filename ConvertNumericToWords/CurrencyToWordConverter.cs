using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConvertNumericToWords
{
    public class CurrencyToWordConverter
    {
        enum OneToThousandNumbers
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
            Fourty = 40,
            Fifty = 50,
            Sixty = 60,
            Seventy = 70,
            Eighty = 80,
            Ninety = 90,
            Hundred = 100,
            Thousand = 1000
        }


        enum LargeNumbers
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

        public string ConvertWholeNumbers(string number, int digitCount)
        {
            string value = null;
            if (Convert.ToInt32(number) == 0)
            {
                return LargeNumbers.Zero.ToString();
            }

            for (int i = 1; i < 6; i++)
            {
                if (digitCount > 0)
                {
                    int startIndex = digitCount - 3 < 0 ? 0 : digitCount - 3;
                    int length = digitCount - 3 < 0 ? digitCount : 3;
                    value = ConvertNumericHundredsToWords(Convert.ToInt32(number.Substring(startIndex, length)), length) + GetEnumDescription((LargeNumbers)i) + value;
                    digitCount -= length;
                }
            }

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
            return value;
        }

        private string ConvertNumericHundredsToWords(int number, int digitCount)
        {
            string hundreds = null;
            if (digitCount >= 3)
            {
                hundreds = ((OneToThousandNumbers)((number / 100) % 10)).ToString() + " Hundred ";
                digitCount -= 1;
            }

            hundreds += ConvertNumericTensToWords(number % 100, digitCount);

            return hundreds;
        }

        private string ConvertNumericTensToWords(int number, int digitCount)
        {
            if (number == 0)
                return null;
            string Tens = null;
            if (number % 10 == 0)
            {
                Tens += /*" And " +*/ ((OneToThousandNumbers)(number % 100)).ToString();
            }
            else if (digitCount >= 2)
            {
                Tens += /*" And " +*/ ((OneToThousandNumbers)((number / 10) * 10)).ToString() + "-" + ((OneToThousandNumbers)(number % 10)).ToString();
            }
            else
            {
                Tens = ((OneToThousandNumbers)(number % 10)).ToString();
            }

            return Tens;
        }


    }
}
