using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class ScreenBuffer
    {

        // The map to be passed in from the main program
        public int[,] map;

        // Function to draw the screen
        public void DrawScreen(int map_width, int map_height, int[,] mapArray)
        {
            // Save the map
            map = mapArray;

            // Setup a list of TileTypes
            List<TileType> listIndex;
            listIndex = new List<TileType>();

            // Add tileTypes to the tileIndex
            listIndex.Add(new TileType('.', ConsoleColor.White, true)); // tile floor
            listIndex.Add(new TileType('#', ConsoleColor.Red, false)); // tile wall
            listIndex.Add(new TileType('T', ConsoleColor.Green, false)); // tree

            //set cursor position to top left and draw the string
            Console.SetCursorPosition(0, 0);

            // First loop through 2D Array
            for (int y = 0; y < map_height; y++)
            {

                // Second loop through 2D Array
                for (int x = 0; x < map_width; x++)
                {

                    // NEW WAY OF DRAWING TILES
                    int type = map[y, x]; // Get the 'type' from the array
                    Console.ForegroundColor = listIndex[type].colorCode; // change the console color
                    Console.Write(listIndex[type].character); // print the character


                }

                // move down ond row
                Console.Out.NewLine = "\r\n";
                Console.WriteLine();



            }


        }
        
    }
}
