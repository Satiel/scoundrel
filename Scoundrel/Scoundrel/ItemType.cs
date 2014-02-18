using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class ItemType
    {
        public char character { get; set; } // which character to use
        public ConsoleColor colorCode { get; set; } // color to use
        public string name { get; set; } // name of the item type

        public ItemType(char c, ConsoleColor s, string n)
        {
            // assign variables from what is passed in
            character = c;
            colorCode = s;
            name = n;
        }

    }
}
