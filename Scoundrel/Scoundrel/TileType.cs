using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class TileType 
    {
        // tile properties
        public char character { get; set; } // which character to use
        public ConsoleColor colorCode { get; set; } // which color to use
        public bool isPassable { get; set; } // is it passable or not

        


        public TileType(char c, ConsoleColor s, bool passable)
        {
            character = c;
            colorCode = s;
            isPassable = passable;

        }
    }
}
