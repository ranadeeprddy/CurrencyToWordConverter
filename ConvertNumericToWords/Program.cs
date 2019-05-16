using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertNumericToWords
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                decimal myNum;
                DateTime myDate;
                Console.WriteLine("Enter Number: ");
                var input = Console.ReadLine();
                var isNegative = "";
                if (input.Contains("-"))
                {
                    isNegative = "Minus ";
                    input = input.Substring(1, input.Length - 1);
                }

                if (decimal.TryParse(input, out myNum))
                {
                    input = myNum.ToString("0.00"); ;


                    //var decimalPlace = input.IndexOf(".");
                    //Console.WriteLine(decimalPlace);


                    //var wholeNo = input.Substring(0, decimalPlace);
                    //Console.WriteLine(wholeNo);


                    //var pennies = input.Substring(decimalPlace + 1);
                    //Console.WriteLine(Convert.ToInt32(pennies));


                    Console.WriteLine(isNegative + Converter.ConvertWholeNumbers(input));
                }
                else if (DateTime.TryParse(input, out myDate))
                {
                    Console.WriteLine(Converter.ConvertDateToWords(myDate));
                }
            }
            Console.ReadKey();
        }
    }
}
