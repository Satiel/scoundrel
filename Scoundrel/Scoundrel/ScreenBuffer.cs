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
            listIndex.Add(new TileType('.', ConsoleColor.White)); // tile floor
            listIndex.Add(new TileType('#', ConsoleColor.Red)); // tile wall
            listIndex.Add(new TileType('T', ConsoleColor.Green)); // tree
        

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


                    // check the tile at that location
                    /**
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
                    } **/
                }

                // move down ond row
                Console.Out.NewLine = "\r\n";
                Console.WriteLine();
            }



        }



        
    }
}
