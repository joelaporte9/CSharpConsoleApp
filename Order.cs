using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubShop
{
    public class Order
    {
        const decimal TaxPercent = 0.07m;

        public static List<Sub> Subs { get; set; } = new List<Sub>();
        public decimal SubTotal => Subs.Sum(p => p.Price);
        public decimal TaxAmount => decimal.Round(SubTotal * TaxPercent, 2);
        public decimal Total => decimal.Round(SubTotal + TaxAmount, 2);

        public static void PrintHeader()
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("{0,20}", "Big Y");
            string date = String.Format("{0:d} at {0:t}", DateTime.Now);
            Console.WriteLine(date);
            Console.WriteLine("------------------------------------------------------");
        }
        
        public void PrintTotal()
        {
            Console.WriteLine();
            Console.WriteLine($"Number of Sandwiches: {Subs.Count}");
            Console.WriteLine("------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Sub Total:  {SubTotal:C2}");
            Console.WriteLine($"Tax Amount: {TaxAmount:C2}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Total:      {Total:C2}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("------------------------------------------------------");
        }
    }
}
