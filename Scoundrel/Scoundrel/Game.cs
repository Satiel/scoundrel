using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    /// <summary>
    /// This is the main type of the game
    /// </summary>
    class Game : IDisposable
    {

        public void Dispose()
        {
            listIndex.Clear();
            listIndex = null;
        }
        // Map dimensions
        public const int map_width = 20;
        public const int map_height = 15;

        // Tile Types
        public const int tile_floor = 0;
        public const int tile_wall = 1;
        public const int tile_tree = 2;
        public const int tile_closed_door = 3;
        public const int tile_open_door = 4;

        // Player Position
        public int playerX = 10;
        public int playerY = 10;
        public int saved_playerX = 0;
        public int saved_playerY = 0;

        // Screen buffer variables
        public int[,] map; // array to store passed-in mapArray
        public List<TileType> listIndex = new List<TileType>(); // list to store TileTypes

        // Map declaration

        public int[,] mapArray = new int[15, 20]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 1, 3, 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
	    };

         bool isPassable(int mapX, int mapY)
        {
            // Before we do anything, make sure that the coordinates are valid
            if (mapX < 0 || mapX >= map_width || mapY < 0 || mapY >= map_height)
                return false;

            // Store the value of the tile specified
            int tileValue = mapArray[mapY, mapX];

            // Return if it's passable or not
            return listIndex[tileValue].isPassable;
        }

        public void DrawScreen(int map_width, int map_height, int[,] mapArray)
        {
            // Save the map
            map = mapArray;

            // Add tileTypes to the tileIndex
            listIndex.Add(new TileType('.', ConsoleColor.White, true)); // tile floor
            listIndex.Add(new TileType('#', ConsoleColor.Red, false)); // tile wall
            listIndex.Add(new TileType('T', ConsoleColor.Green, false)); // tree
            listIndex.Add(new TileType('/', ConsoleColor.Magenta, false)); // closed door
            listIndex.Add(new TileType('_', ConsoleColor.Magenta, true)); // open door

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

                // move down one row
                Console.Out.NewLine = "\r\n";
                Console.WriteLine();

            }

        }

        public void OpenDoorCommand()
        {
            // Draw some notifcation to the user

            Console.WriteLine("Which direction? (2, 4, 6, 8)");
            ConsoleKey key = Console.ReadKey().Key;

            // set delta coords for future door coords
            int deltaX = 0;
            int deltaY = 0;

            switch (key)
            {

                // NORT
                case ConsoleKey.NumPad0:
                    // add coordinates to delta variables
                    deltaX = 0;
                    deltaY = -1;
                    break;

                // WEST
                case ConsoleKey.NumPad4:
                    // add coordinates to delta variables
                    deltaX = -1;
                    deltaY = 0;
                    break;

                // EAST
                case ConsoleKey.NumPad6:
                    // add coordinates to delta variables
                    deltaX = 1;
                    deltaY = 0;
                    break;

                // EAST
                case ConsoleKey.NumPad2:
                    // add coordinates to delta variables
                    deltaX = 0;
                    deltaY = 1;
                    break;

                // Not a valid direction
                default:
                    // No direction specified, so abort
                    break;
            }

            if (mapArray[playerY + deltaY, playerX + deltaX] == tile_closed_door)
            {

                mapArray[playerY + deltaY, playerX + deltaX] = tile_open_door;
                Console.Clear();
                DrawScreen(map_width, map_height, mapArray);
            }

        }

        public void Main()
        {
            Console.CursorVisible = false;
            //Console.SetCursorPosition(10, 10);

            // delta movement variables
            int deltaX = 0;
            int deltaY = 0;

            while (true)
            {
                // Draw the map                 
                DrawScreen(map_width, map_height, mapArray);

                // Set the player's new position
                Console.SetCursorPosition(playerX, playerY);

                // Draw the player
                Console.Write('@');

                // Draw the player to the screen

                // Input phase - wait for the player to do something
                var choice = Console.ReadKey();

                // Bool to check if movement was made 
                

                // Process the input
                switch (choice.Key)
                {

                    // Move up
                    case ConsoleKey.UpArrow:
                        // add coordinates to delta variables
                        deltaX = 0;
                        deltaY = -1; 
                        break;

                    // Move left
                    case ConsoleKey.LeftArrow:
                        // add coordinates to delta variables
                        deltaX = -1;
                        deltaY = 0;
                        break;

                    // Move right
                    case ConsoleKey.RightArrow:
                        // add coordinates to delta variables
                        deltaX = 1;
                        deltaY = 0;                        
                        break;

                    // Move down
                    case ConsoleKey.DownArrow:
                        // add coordinates to delta variables
                        deltaX = 0;
                        deltaY = 1;                        
                        break;

                    // Open door
                    case ConsoleKey.O:
                        DrawScreen(map_width, map_height, mapArray);
                        //Console.SetCursorPosition(2, 22);
                        OpenDoorCommand();                        
                        // do stuff
                        break;

                    // Close door
                    case ConsoleKey.C:
                        // do stuff
                        break;

                    // Ignore other keys
                    default:
                        break;
                }

                if (isPassable(playerX + deltaX, playerY + deltaY) )
                {
                    // If allowed, move in that direction
                    playerX += deltaX;
                    playerY += deltaY;
                    
                    //clear the delta variables
                    deltaX = 0;
                    deltaY = 0;
                }              
            }
        }
    }
}
