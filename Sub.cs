using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubShop
{
    public class Sub 
    {
        public SubSize Size { get; set; }
        public List<Topping> Toppings { get; set; } = new List<Topping>();

        public decimal Price => Size.Price + Toppings.Sum(t => t.Type.Price);
    }
}

