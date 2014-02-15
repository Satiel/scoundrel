using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class ScreenBuffer
    {
        // Tile Types
        public const int tile_floor = 0;
        public const int tile_wall = 1;
        public const int tile_tree = 2;

        public  void DrawScreen(int map_width, int map_height, int [,] mapArray)
        {
            //set cursor position to top left and draw the string
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < map_height; y++)
            {

                for (int x = 0; x < map_width; x++)
                {
                    // check the tile at that location
                    switch (mapArray[y, x])
                    {
                        case tile_floor:
                            // draw a floor
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write('.');
                            break;

                        case tile_wall:
                            // draw a wall
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write('#');
                            break;

                        case tile_tree:
                            // draw a tree
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write('T');
                            break;
                    }
                }

                // move down ond row
                Console.Out.NewLine = "\r\n";
                Console.WriteLine();
            }
        }

        struct TILE_TYPE
        {
            public char character;
            public string colorCode;
            public bool passable;
            
        }


        
    }
}
