using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        // Map dimensions
        public const int map_width = 20;
        public const int map_height = 15;

        // Tile Types
        public const int tile_floor = 0;
        public const int tile_wall = 1;

        // Player Position
        public static int playerX = 10;
        public static int playerY = 10;

        // Map declaration

        public static int[,] mapArray = new int[15, 20]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
	    };

        // Function prototypes
        static public  void DrawMap()
        {
            // draw loop
            for (int y = 0; y < map_height; y++)
            {


                for (int x = 0; x < map_width; x++)
                {
                    // check the tile at that location
                    switch (mapArray[y,x])
                    {
                        case 0:
                            // draw a floor
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write('.');
                            break;

                        case 1:
                            // draw a wall
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write('#');
                            break;
                    }
                }

                // move down ond row
                Console.Out.NewLine = "\r\n";
                Console.WriteLine();
            }



        }

         static bool  isPassable(int mapX, int mapY)
        {
            // Before we do anything, make sure that the coordinates are valid
            if (mapX < 0 || mapX >= map_width || mapY < 0 || mapY >= map_height)
                return false;

            // Store the value of the tile specified
            int tileValue = mapArray[mapY, mapX];

            // Return true if it's passable
            if (tileValue == tile_floor)
                return true;

            // If execution gets here, it's not passsable
            return false;
        }

         static void Main(string[] args)
         {
             Console.CursorVisible = false;
             //Console.SetCursorPosition(10, 10);
             

             while (true)
             {
                 // Clear hte console
                 Console.Clear();

                 // Draw the map
                 DrawMap();

                 // Set the player's new position
                 Console.SetCursorPosition(playerX, playerY);

                 // Draw the player
                 Console.Write('@');

                 // Draw the player to the screen

                 // Input phase - wait for the player to do something
                 var choice = Console.ReadKey();

                 // Process the input
                 switch (choice.Key)
                 {

                     // Move up
                     case ConsoleKey.UpArrow:
                         // Check if the player can go up
                         if (isPassable(playerX, playerY-1))
                         {
                             // Move the player up
                             playerY--;
                         }
                         break;

                     // Move left
                     case ConsoleKey.LeftArrow:
                         // Check if the player can go left
                         if (isPassable(playerX - 1, playerY))
                         {
                             // Move the player left
                             playerX--;
                         }
                         break;

                     // Move right
                     case ConsoleKey.RightArrow:
                         // Check if the player can go right
                         if (isPassable(playerX + 1, playerY))
                         {
                             // Move the player right
                             playerX++;
                         }

                         break;

                     // Move down
                     case ConsoleKey.DownArrow:
                         // Check if the player can go down
                         if (isPassable(playerX, playerY + 1))
                         {
                             // Move the player down
                             playerY++;
                         }
                         break;

                     // Ignore other keys
                     default:
                         break;
                 }

             }
         }

    }
}
