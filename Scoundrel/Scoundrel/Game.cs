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
        const int MAP_WIDTH = 20;
        const int MAP_HEIGHT = 15;

        // Tile Types
        public const int TILE_FLOOR = 0;
          int TILE_WALL = 1;
         const int TILE_TREE = 2;
         const int TILE_CLOSED_DOOR = 3;
         const int TILE_OPEN_DOOR = 4;

        // Item Types
         const int ITEM_NONE = 0;
         const int ITEM_POTION = 1;
         const int ITEM_ROCK = 2;

        // Player Position
         int playerX = 10;
         int playerY = 10;
         int saved_playerX = 0;
         int saved_playerY = 0;

        // Player inventory
         int[] inventory = new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
         const int INVENTORY_SLOTS = 10;

        // Screen buffer variables
         int[,] map; // array to store passed-in mapArray
         List<TileType> listIndex = new List<TileType>(); // list to store TileTypes
         List<ItemType> itemIndex = new List<ItemType>(); // list to store ItemTypes

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

        // Item map, overload on top of the world map
        public int[,] itemArray = new int[15,20]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
	    };


         bool isPassable(int mapX, int mapY)
        {
            // Before we do anything, make sure that the coordinates are valid
            if (mapX < 0 || mapX >= MAP_WIDTH || mapY < 0 || mapY >= MAP_HEIGHT)
                return false;

            // Store the value of the tile specified
            int tileValue = mapArray[mapY, mapX];

            // Return if it's passable or not
            return listIndex[tileValue].isPassable;
        }

        public void DrawScreen(int MAP_WIDTH, int MAP_HEIGHT, int[,] mapArray)
        {
            // Save the map
            map = mapArray;

            // Add TileTypes to the tileIndex
            listIndex.Add(new TileType('.', ConsoleColor.White, true)); // tile floor
            listIndex.Add(new TileType('#', ConsoleColor.Red, false)); // tile wall
            listIndex.Add(new TileType('T', ConsoleColor.Green, false)); // tree
            listIndex.Add(new TileType('/', ConsoleColor.Magenta, false)); // closed door
            listIndex.Add(new TileType('_', ConsoleColor.Magenta, true)); // open door

            // Add ItemTypes to the ItemIndex
            itemIndex.Add(new ItemType(' ', ConsoleColor.Gray, "EMPTY")); // (0) ITEM_NONE (unused inventory slot)
            itemIndex.Add(new ItemType((char)176, ConsoleColor.Cyan, "Potion")); // (1) ITEM_POTION
            itemIndex.Add(new ItemType('*', ConsoleColor.DarkGray, "Rock")); // (2) ITEM_ROCK

            //set cursor position to top left and draw the string
            Console.SetCursorPosition(0, 0);

            // First loop through 2D Array
            for (int y = 0; y < MAP_HEIGHT; y++)
            {

                // Second loop through 2D Array
                for (int x = 0; x < MAP_WIDTH; x++)
                {
                    // check to see if there is an item present at this location
                    if (itemArray[y, x] != ITEM_NONE)
                    {
                        // Draw the item instead of the tile
                        int itemType = itemArray[y, x]; // get the 'type' from the item array
                        Console.ForegroundColor = itemIndex[itemType].colorCode; // change the console color
                        Console.Write(itemIndex[itemType].character); // print the character
                    }
                    else
                    {
                        // NEW WAY OF DRAWING TILES
                        int type = map[y, x]; // Get the 'type' from the map array
                        Console.ForegroundColor = listIndex[type].colorCode; // change the console color
                        Console.Write(listIndex[type].character); // print the character
                    }

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

            if (mapArray[playerY + deltaY, playerX + deltaX] == TILE_CLOSED_DOOR)
            {

                mapArray[playerY + deltaY, playerX + deltaX] = TILE_OPEN_DOOR;
                Console.Clear();
                DrawScreen(MAP_WIDTH, MAP_HEIGHT, mapArray);
            }

        }

        // CREATE CLOSE DOOR FUNCTION

        // display the player's inventory
        public void ShowInventory()
        {
            
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(MAP_WIDTH + 2, 1);
            Console.WriteLine("INVENTORY");
            Console.SetCursorPosition(MAP_WIDTH + 2, 2);
            Console.WriteLine("----------");

            for (int i = 0; i < INVENTORY_SLOTS; i++)
            {
                // grab the item type being store in this inventory slot
                int itemType = inventory[i];

                // Draw the items name to the console
                Console.SetCursorPosition(MAP_WIDTH + 2, 3 + i);
                Console.Write((char)((int)'A' + i));
                Console.Write(": " + itemIndex[itemType].name);
            }
        }

        public void GetCommand()
        {
            // First check to see if there's actually an item present underneath the player
            if (itemArray[playerY, playerX] == ITEM_NONE)
            {
                // Complain that there isn't an item here

                // Abore the rest of the command
                return;
            }

            // Iterate through the inventory, checking for the first available slot
            for (int i = 0; i < INVENTORY_SLOTS; i++)
            {
                // Found an open slot?
                if (inventory[i] == ITEM_NONE)
                {
                    // Move the item to the slot and remove it from the world
                    inventory[i] = itemArray[playerY, playerX];
                    itemArray[playerY, playerX] = ITEM_NONE;
                    return;
                }
            }
            // Do stuff

            // If execution gets here, it means that there are no open slots available.
            // Complain about it
        }

        public void DropCommand()
        {
            char a = 'a';
            // Ask the user which inventory slot they're trying to drop
            Console.SetCursorPosition(2, MAP_HEIGHT);
            Console.WriteLine("Drop from which inventory slot?");
            var choice = Console.ReadKey();

            

            // Convert the key press into an inventory slot
            int slot = choice.KeyChar - (int)a;

            // Verify that this is a valid slot
            if (slot < 0 || slot >= INVENTORY_SLOTS)
            {
                // Complain to the user
                Console.SetCursorPosition(2, MAP_HEIGHT + 3);
                Console.WriteLine("Invalid slot");
            }
                // This is a valid slot, PLACE IT!
            else if (inventory[slot] == ITEM_NONE)
            {
                // Complain to the user
                Console.SetCursorPosition(2, MAP_HEIGHT + 3);
                Console.WriteLine("No item present");
            }

            else if (itemArray[playerY, playerX] != ITEM_NONE)
            {

            }

            else
            {

                // Place the item on the ground
                itemArray[playerY, playerX] = inventory[slot];
                inventory[slot] = ITEM_NONE;
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
                DrawScreen(MAP_WIDTH, MAP_HEIGHT, mapArray);

                // Draw the inventory
                ShowInventory();

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
                        DrawScreen(MAP_WIDTH, MAP_HEIGHT, mapArray);
                        //Console.SetCursorPosition(2, 22);
                        OpenDoorCommand();                        
                        // do stuff
                        break;

                    // Close door
                    case ConsoleKey.C:
                        // do stuff
                        break;

                    // Get item
                    case ConsoleKey.G:
                        // grab item
                        GetCommand();
                        break;

                    case ConsoleKey.D:
                        // drop item
                        DropCommand();
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
