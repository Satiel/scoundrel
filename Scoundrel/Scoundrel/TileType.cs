using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class TileType 
    {
        // tile properties
        public char character { get; set; }
        public ConsoleColor colorCode { get; set; }

        public TileType(char c, ConsoleColor s)
        {
            character = c;
            colorCode = s;
        }
    }
}
