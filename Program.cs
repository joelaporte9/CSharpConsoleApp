using SubShop;
using System;
using System.Collections.Generic;
using System.Linq;

//Create an application that allows a user to order one or more sandwhiches. 
//The sandwhiches can be either 6" or 12". The 6" sandwhich is $2.00 and the 12" is $3.50. 
//Each sandwhich can include any number of toppings.
//The following toppings are free - lettuce, tomato, cheese, mustard, mayo. 
//The following toppings cost $1.00 each - bacon, avacado, extra meat. 
//Each sandwhich has the option of being toasted in the oven.
//The customer cannot provide a tip as that is cash only. 
//The tax in the state is 7%. 
//The customer will want to know their total so they can get reimbursed.


namespace SubShop
{
    public class Program
    {
        private static ToppingType freeTopping;
        private static ToppingType additionalTopping;
        private static List<Topping> toppings;
        private static List<SubSize> subSize;
        
        const string InputQuit = "Q";

        public static void Main()
        {
            freeTopping = new ToppingType
            {
                Name = "Free",
                Price = 0.0m
            };
            additionalTopping = new ToppingType
            {
                Name = "Additional",
                Price = 1.00m
            };
            toppings = new List<Topping>
            {
                new Topping { Name = "Lettuce", Type = freeTopping, Id = "1" },
                new Topping { Name = "Tomato", Type = freeTopping, Id = "2" },
                new Topping { Name = "Cheese", Type = freeTopping, Id = "3" },
                new Topping { Name = "Mustard", Type = freeTopping, Id = "4" },
                new Topping { Name = "Mayo", Type = freeTopping, Id = "5" },
                new Topping { Name = "Pepper", Type = freeTopping, Id = "6" },

                new Topping { Name = "Bacon", Type = additionalTopping, Id = "7" },
                new Topping { Name = "Avacado", Type = additionalTopping, Id = "8" },
                new Topping { Name = "Extra Meat", Type = additionalTopping, Id = "9" },
            };

            subSize = new List<SubSize>
            {
                new SubSize { Name = "6 Inch", Price = 2m, Id = "6" },
                new SubSize { Name = "12 Inch", Price = 3.50m, Id = "12" },
            };

            WriteIntroduction();

            var order = new Order();
          

            while (true)
            {
                
                var sub = new Sub();
                var size = GetSize();
                sub.Size = size;
               

                Console.WriteLine($"Enter the toppings you would like. Please enter order ID number. ({InputQuit} to quit):");
                while (true)
                {
                    var topping = GetTopping();
                    if (topping == null)
                    {
                        break;
                    }
                    if (sub.Toppings.Any(t => t.Id == topping.Id))
                    {
                        Console.WriteLine($"{topping.Name} is already on the sandwich. Please enter another topping.");
                    }
                    else
                    {
                        sub.Toppings.Add(topping);
                    }
                }
               
                Order.Subs.Add(sub);

                Console.WriteLine("Would you like to add another sandwich to your order (Y/N)?");
                if (!string.Equals(Console.ReadLine(), "Y", StringComparison.OrdinalIgnoreCase))
                {
                    PrintRecipt();
                    order.PrintTotal();
                    break;
                }
            }
        }
      
        public static void PrintRecipt()
        {
            Order.PrintHeader();
            Console.WriteLine("{0,4} {1,15} {2,20} ", "Size", "Topping", "Topping Cost");
            Console.WriteLine("------------------------------------------------------");
            foreach (var items in Order.Subs)
            {
                Console.WriteLine(items.Size.Name);
                foreach (var _topping in items.Toppings)
                {
                    string outputTopping = String.Format("{0,20} {1,20}",  _topping.Name, _topping.Type.Price);
                    Console.WriteLine(outputTopping);
                }
                items.Dump();
            }
        }
        
        public static void WriteIntroduction()
        {
            Console.WriteLine("Size in inches:");
            Console.WriteLine();
            foreach (var size in subSize)
            {
                Console.WriteLine($"{size.Name} - {size.Price:C} - ID = {size.Id}");
            }
            Console.WriteLine("------------------------");
            var toppingType = toppings.GroupBy(t => t.Type);

            foreach (var Type in toppingType)
            {
                Console.WriteLine($"{Type.Key.Name} toppings are {Type.Key.Price:C} each");
                Console.WriteLine();
                foreach (var topping in Type)
                {
                    Console.WriteLine($"{topping.Name} - order ID = {topping.Id}");
                }
                Console.WriteLine("------------------------");
            }
            ;
        }

        public static void GetToasted()
        {
            Sub isToasted = null;

            while (isToasted == null)
            {
                Console.WriteLine("would you like you bread toasted? (Y/N)");
                var toasted = Console.ReadLine();

                if (string.Equals(toasted, "Y", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("toasting...");
                    break;
                }
                else if (string.Equals(toasted, "N", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }
        }

        public static SubSize GetSize()
        {
            SubSize size = null;

            while (size == null)
            {
                Console.WriteLine("Would you like 6 or 12 inch sub?");
                var sizeInput = Console.ReadLine();

                size = subSize.FirstOrDefault(gs => Equals(gs.Id, sizeInput));

                if (size == null)
                {
                    Console.WriteLine("Please enter a valid size");
                }
                else
                {
                    GetToasted();
                    break;
                }
            }
            return size;
        }
        
        public static Topping GetTopping()
        {
            Topping topping = null;

            while (topping == null)
            {
                var toppingInput = Console.ReadLine();

                if (string.Equals(toppingInput, InputQuit, StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }

                topping = toppings.FirstOrDefault(t => string.Equals(t.Id, toppingInput));

                if (topping == null)
                {
                    Console.WriteLine("Please enter a valid topping");
                }
                else
                {
                    break;
                }
            }
            return topping;
        }
    }
}
