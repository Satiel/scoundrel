using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    /// <summary>
    /// This is the main type of the game
    /// </summary>
    class Game : IDisposable
    {


        int keySlot = 0;
        public void Dispose()
        {
            listIndex.Clear();
            listIndex = null;
        }
        // Map dimensions
        const int MAP_WIDTH = 20;
        const int MAP_HEIGHT = 15;

        // Tile Types
        const int TILE_FLOOR = 0;
        const int TILE_WALL = 1;
        const int TILE_TREE = 2;
        const int TILE_CLOSED_DOOR = 3;
        const int TILE_OPEN_DOOR = 4;
        const int TILE_LOCKED_DOOR = 5;
        const int TILE_EXIT = 6;

        // Item Types
        const int ITEM_NONE = 0;
        const int ITEM_POTION = 1;
        const int ITEM_ROCK = 2;
        const int ITEM_KEY = 3;
        const int ITEM_AXE = 4;

        // Player Position
        int playerX = 10;
        int playerY = 10;

        // Player previous position for HEALTH CALCULATION
        int previousPlayerX = 10;
        int previousPlayerY = 10;

        // Player progress
        int currentLevel = 1;

        // Player health WITH THE DEFAULT POISON WEAKNESS
        int health = 100;

        // Player inventory
        int[] inventory = new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        const int INVENTORY_SLOTS = 10;

        // Screen buffer variables
        int[,] map; // array to store passed-in levelOneArray
        List<TileType> listIndex = new List<TileType>(); // list to store TileTypes
        List<ItemType> itemIndex = new List<ItemType>(); // list to store ItemTypes

        // Map declaration
        /** OLD MAP

        public int[,] levelOneArray = new int[15, 20]
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
	    }; **/

        // Map as read from the text document
        public int[,] levelOneArray = new int[15, 20];

        // Item map, overload on top of the world map
        /*public int[,] itemArray = new int[15,20]
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
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
	    }; */

        public int[,] itemArray = new int[15, 20];

        // Screen variables
        Screen screen = null;

        public void readLevelItems()
        {
            StreamReader sr = new StreamReader("item" + currentLevel + ".txt");

            for (int y = 0; y < MAP_HEIGHT; y++)
            {

                for (int x = 0; x < MAP_WIDTH; x++)
                {
                    int currentChar;

                    currentChar = sr.Read();


                    currentChar -= 48;

                    switch (currentChar)
                    {
                        case 0:
                            // Add 0 to array
                            itemArray[y, x] = 0;
                            break;
                        case 1:
                            itemArray[y, x] = 1;
                            break;
                        case 2:
                            itemArray[y, x] = 2;
                            break;

                        case 3:
                            itemArray[y, x] = 3;
                            break;

                        case 4:
                            itemArray[y, x] = 4;
                            break;

                        case 5:
                            itemArray[y, x] = 5;
                            break;

                        case 6:
                            itemArray[y, x] = 6;
                            break;

                        default:
                            break;
                    }
                }

                sr.ReadLine();
            }
        }


        public void readLevelTiles()
        {
            
            StreamReader sr = new StreamReader("level" + currentLevel +  ".txt");

                // Read the current character


                for (int y = 0; y < MAP_HEIGHT; y++)
                {

                    for (int x = 0; x < MAP_WIDTH; x++)
                    {
                        int currentChar;

                        currentChar = sr.Read();


                        currentChar -= 48;

                       switch (currentChar)
                        {
                            case 0:
                            // Add 0 to array
                                levelOneArray[y, x] = 0;
                                break;
                           case 1:
                               levelOneArray[y,x] = 1;
                               break;
                           case 2:
                               levelOneArray[y, x] = 2;
                               break;

                           case 3:
                               levelOneArray[y, x] = 3;
                               break;

                           case 4:
                               levelOneArray[y, x] = 4;
                               break; 

                           case 5:
                               levelOneArray[y, x] = 5;
                               break;

                           case 6:
                               levelOneArray[y, x] = 6;
                               break;

                            default:
                                break;
                        }
                    }

                    sr.ReadLine();
                }

            

        }

         bool isPassable(int mapX, int mapY)
        {
            // Before we do anything, make sure that the coordinates are valid
            if (mapX < 0 || mapX >= MAP_WIDTH || mapY < 0 || mapY >= MAP_HEIGHT)
                return false;

            // Store the value of the tile specified
            int tileValue = levelOneArray[mapY, mapX];

            // Return if it's passable or not
            return listIndex[tileValue].isPassable;
        }

        public void DrawScreen(int MAP_WIDTH, int MAP_HEIGHT, int[,] levelOneArray)
        {
            // Save the map
            map = levelOneArray;

            // Add TileTypes to the tileIndex
            listIndex.Add(new TileType('.', ConsoleColor.White, true)); // tile floor
            listIndex.Add(new TileType('#', ConsoleColor.Red, false)); // tile wall
            listIndex.Add(new TileType('T', ConsoleColor.Green, false)); // tree
            listIndex.Add(new TileType('/', ConsoleColor.Magenta, false)); // closed door
            listIndex.Add(new TileType('_', ConsoleColor.Magenta, true)); // open door
            listIndex.Add(new TileType('I', ConsoleColor.Magenta, false)); // locked door
            listIndex.Add(new TileType('X', ConsoleColor.Blue, false)); // room exit

            // Add ItemTypes to the ItemIndex
            itemIndex.Add(new ItemType(' ', ConsoleColor.Gray, "EMPTY")); // (0) ITEM_NONE (unused inventory slot)
            itemIndex.Add(new ItemType((char)176, ConsoleColor.Cyan, "Potion")); // (1) ITEM_POTION
            itemIndex.Add(new ItemType('*', ConsoleColor.DarkGray, "Rock")); // (2) ITEM_ROCK
            itemIndex.Add(new ItemType((char)12, ConsoleColor.Yellow, "Key")); // (3) ITEM_KEY
            itemIndex.Add(new ItemType('(', ConsoleColor.Red, "Pickaxe")); // (4) ITEM_AXE

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

        public void DrawStartMenu()
        {
            Console.SetCursorPosition(10, 10);
            Console.Write("Welcome to ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Scoundrel,");
            Console.WriteLine();
            Console.SetCursorPosition(10, 11);
            Console.ResetColor();
            Console.WriteLine("the game of thievery and death.");
            Console.SetCursorPosition(10, 13);
            Console.WriteLine("(P)lay new game");
            

            // Let the user type
            Console.SetCursorPosition(10, 15);
            ConsoleKey smKey = Console.ReadKey().Key;
            
            // Change the screen based on the key input
            switch (smKey)
            {
                // Clear the screen and start the game
                case ConsoleKey.P:
                    // Play the game
                    Console.Clear();
                    screen.CurrentPhase = Screen.Phase.Play;
                    break;

                case ConsoleKey.Escape:
                    // Quit the game
                    Environment.Exit(0);
                    break;

                default:
                    // Draw the start screen again until a valid option is chosen
                    DrawStartMenu();
                    break;
            }
                    
        }

        public void DrawPauseMenu()
        {
            Console.SetCursorPosition(10, 10);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Game is paused. Press (P)lay to resume game.");
            


            // Let the user type
            ConsoleKey smKey = Console.ReadKey().Key;

            // Change the screen based on the key input
            switch (smKey)
            {
                // Clear the screen and start the game
                case ConsoleKey.P:
                    // Play the game
                    Console.Clear();
                    screen.CurrentPhase = Screen.Phase.Play;
                    break;

                case ConsoleKey.Escape:
                    // Quit the game
                    Environment.Exit(0);
                    break;

                default:
                    // Draw the start screen again until a valid option is chosen
                    DrawPauseMenu();
                    break;
            }
        }

        public void OpenDoorCommand()
        {
            // Draw some notifcation to the user
            Console.SetCursorPosition(0, 17);
            Console.WriteLine("Which direction? (Up, Down, Left, Right)");
            ConsoleKey key = Console.ReadKey().Key;

            // set delta coords for future door coords
            int deltaX = 0;
            int deltaY = 0;

            switch (key)
            {

                // NORT
                case ConsoleKey.UpArrow:
                    
                    // add coordinates to delta variables
                    deltaX = 0;
                    deltaY = -1;
                    break;

                // WEST
                case ConsoleKey.LeftArrow:
                    // add coordinates to delta variables
                    deltaX = -1;
                    deltaY = 0;
                    break;

                // EAST
                case ConsoleKey.RightArrow:
                    // add coordinates to delta variables
                    deltaX = 1;
                    deltaY = 0;
                    break;

                // EAST
                case ConsoleKey.DownArrow:
                    // add coordinates to delta variables
                    deltaX = 0;
                    deltaY = 1;
                    break;

                // Not a valid direction
                default:
                    // No direction specified, so abort
                    break;
            }

            if (levelOneArray[playerY + deltaY, playerX + deltaX] == TILE_CLOSED_DOOR)
            {

                levelOneArray[playerY + deltaY, playerX + deltaX] = TILE_OPEN_DOOR;
                Console.Clear();
                DrawScreen(MAP_WIDTH, MAP_HEIGHT, levelOneArray);
            }

            else
            {
                if (levelOneArray[playerY + deltaY, playerX + deltaX] == TILE_LOCKED_DOOR)
                {
                    //DrawScreen(MAP_WIDTH, MAP_HEIGHT, levelOneArray);
                    Console.WriteLine("The door is locked! Find a key!");

                    // Wait 2 seconds and then delete the message
                    System.Threading.Thread.Sleep(2500);
                    ClearText();
                }

                else
                {
                    //DrawScreen(MAP_WIDTH, MAP_HEIGHT, levelOneArray);
                    Console.WriteLine("There isn't a closed door here!");

                    // Wait 2 seconds and then delete the message
                    System.Threading.Thread.Sleep(2500);
                    ClearText();
                }


            }

        }

        // CREATE CLOSE DOOR FUNCTION
        public void CloseDoorCommand()
        {
            // Draw some notifcation to the user
            Console.SetCursorPosition(0, 17);
            Console.WriteLine("Which direction? (Up, Down, Left, Right)");
            ConsoleKey key = Console.ReadKey().Key;

            // set delta coords for future door coords
            int deltaX = 0;
            int deltaY = 0;

            switch (key)
            {

                // NORT
                case ConsoleKey.UpArrow:
                    // add coordinates to delta variables
                    deltaX = 0;
                    deltaY = -1;
                    break;

                // WEST
                case ConsoleKey.LeftArrow:
                    // add coordinates to delta variables
                    deltaX = -1;
                    deltaY = 0;
                    break;

                // EAST
                case ConsoleKey.RightArrow:
                    // add coordinates to delta variables
                    deltaX = 1;
                    deltaY = 0;
                    break;

                // EAST
                case ConsoleKey.DownArrow:
                    // add coordinates to delta variables
                    deltaX = 0;
                    deltaY = 1;
                    break;

                // Not a valid direction
                default:
                    // No direction specified, so abort
                    break;
            }

            if (levelOneArray[playerY + deltaY, playerX + deltaX] == TILE_OPEN_DOOR)
            {

                levelOneArray[playerY + deltaY, playerX + deltaX] = TILE_CLOSED_DOOR;
                Console.Clear();
                DrawScreen(MAP_WIDTH, MAP_HEIGHT, levelOneArray);
            }

            else

            {
                //DrawScreen(MAP_WIDTH, MAP_HEIGHT, levelOneArray);
                Console.WriteLine("There isn't an open door here!");

                // Wait 2 seconds and then delete the message
                System.Threading.Thread.Sleep(2500);
                ClearText();


            }

        }


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

                    ClearText();

                    return;
                }
            }
            // Do stuff

            // If execution gets here, it means that there are no open slots available.
            // Complain about it
        }

        public void DropCommand()
        {
            /** COMMANDS TO FIX INVENTORY TEXT ISSUES **/

            ClearText();

            char a = 'a';
            // Ask the user which inventory slot they're trying to drop


            // THIS IS A TEMPORARY FIX FOR THE ISSUE WHERE
            // PLAYER INPUT GETS DISPLAYED ON THE SCREEN
            // BY THE PLAYER ICON WHEN INITIALLY TYPING - FIND BETTER FIX FOR THIS
            Console.CursorVisible = true;
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
                // There is already an item here
                Console.SetCursorPosition(2, MAP_HEIGHT + 3);
                Console.WriteLine("There is already an item here");
            }

            else
            {

                // Place the item on the ground
                itemArray[playerY, playerX] = inventory[slot];
                Console.Clear();
                inventory[slot] = ITEM_NONE;
            }

            // Wait 2 seconds and then delete the message
            System.Threading.Thread.Sleep(2500);
            ClearText();

        }

        public void UseItemCommand()
        {
            /** COMMANDS TO FIX INVENTORY TEXT ISSUES **/

            ClearText();

            char a = 'a';
            // Ask the user which inventory slot they're trying to drop


            // THIS IS A TEMPORARY FIX FOR THE ISSUE WHERE
            // PLAYER INPUT GETS DISPLAYED ON THE SCREEN
            // BY THE PLAYER ICON WHEN INITIALLY TYPING - FIND BETTER FIX FOR THIS
            Console.CursorVisible = true;
            Console.SetCursorPosition(2, MAP_HEIGHT);
            Console.WriteLine("Use which item?");
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

            switch (inventory[slot])
            {
                // Empty inventory slot
                case ITEM_NONE:
                    // Complain to the user - nothing is here
                    Console.SetCursorPosition(2, MAP_HEIGHT + 3);
                    Console.WriteLine("No item to use");


                    break;

                case ITEM_POTION:
                    // Give a message to the user
                    
                    inventory[slot] = ITEM_NONE;
                    ClearText();
                    Console.SetCursorPosition(2, MAP_HEIGHT + 3);
                    Console.WriteLine("You drank the potion. *burp!*");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(2, MAP_HEIGHT + 4);
                    Console.WriteLine("Your health went up by 50");
                    health += 50;

                    // Wait 2 seconds and then delete the message
                    System.Threading.Thread.Sleep(2500);
                    ClearText();

                    // Remove the item from the inventory
                    
                    
                    //ClearText();


                    break;

                case ITEM_AXE:
                    // Use the pickaxe
                    UsePickaxe();
                    break;

                case ITEM_KEY:
                    // Use the key
                    keySlot = slot;
                    UseKey();
                    break;

                // We don't know what the item is
                default:
                    // Complain to the user
                    Console.SetCursorPosition(2, MAP_HEIGHT + 3);
                    Console.WriteLine("Don't know how to use this item!");

                    // Wait 2 seconds and then delete the message
                    System.Threading.Thread.Sleep(2500);
                    ClearText();
                    break;
            }

        }

        public void UsePickaxe()
        {

            // Ask the user which direction he wants to axe in
            Console.SetCursorPosition(2, MAP_HEIGHT + 3);
            Console.WriteLine("Use Pickaxe: which direction?");

            // Get the user input for the direction
            ConsoleKey key = Console.ReadKey().Key;
            int deltaX = 0;
            int deltaY = 0;

            // Compute which tile he user specified
            switch (key)
            {
                // SOUTH
                case ConsoleKey.DownArrow:
                    deltaX = 0;
                    deltaY = 1;
                    break;

                // WEST
                case ConsoleKey.LeftArrow:
                    deltaX = -1;
                    deltaY = 0;
                    break;
                
                // EAST
                case ConsoleKey.RightArrow:
                    deltaX = 1;
                    deltaY = 0;
                    break;

                // NORTH
                case ConsoleKey.UpArrow:
                    deltaX = 0;
                    deltaY = -1;
                    break;

                // Not a valid direction
                default:
                    // no direction specified, so abort
                    ClearText();
                    break;
            }

            if (playerX + deltaX < 0 || playerX + deltaX >= MAP_WIDTH ||
    playerY + deltaY < 0 || playerY + deltaY >= MAP_HEIGHT)
            {
                // Let the user know everything went well
                Console.SetCursorPosition(2, MAP_HEIGHT + 4);
                Console.WriteLine("Can't use that item here!");

                // Wait 2 seconds and then delete the message
                System.Threading.Thread.Sleep(2500);
                ClearText();
                return;
            }

            if (levelOneArray[playerY + deltaY, playerX + deltaX] != TILE_WALL)
            {
                // Complain to the user
                ClearText();
                Console.SetCursorPosition(2, MAP_HEIGHT + 3);
                Console.WriteLine("Can't use that item here!");
                // Wait 2 seconds and then delete the message
                System.Threading.Thread.Sleep(2500);
                ClearText();
               
                return;
            }

            // change the selected tile from a wall to a rock floor
            levelOneArray[playerY + deltaY, playerX + deltaX] = TILE_FLOOR;

            // place a rock there to simulate rubble
            itemArray[playerY + deltaY, playerX + deltaX] = ITEM_ROCK;

            // Let the user know the wall has been smashed, along with his health
            Console.SetCursorPosition(2, MAP_HEIGHT + 4);
            Console.WriteLine("You smash the stone to pieces.");
            Console.SetCursorPosition(2, MAP_HEIGHT + 6);
            Console.WriteLine("Use the axe has drained you of 20 health!");
            health -= 20;

            // Wait 2 seconds and then delete the message
            System.Threading.Thread.Sleep(2500);
            ClearText();
        }

        public void UseKey()
        {
            // Ask the user which direction he wants to axe in
            Console.SetCursorPosition(2, MAP_HEIGHT + 3);
            Console.WriteLine("Use key: which direction?");

            // Get the user input for the direction
            ConsoleKey key = Console.ReadKey().Key;
            int deltaX = 0;
            int deltaY = 0;

            // Compute which tile he user specified
            switch (key)
            {
                // SOUTH
                case ConsoleKey.DownArrow:
                    deltaX = 0;
                    deltaY = 1;
                    break;

                // WEST
                case ConsoleKey.LeftArrow:
                    deltaX = -1;
                    deltaY = 0;
                    break;

                // EAST
                case ConsoleKey.RightArrow:
                    deltaX = 1;
                    deltaY = 0;
                    break;

                // NORTH
                case ConsoleKey.UpArrow:
                    deltaX = 0;
                    deltaY = -1;
                    break;

                // Not a valid direction
                default:
                    // no direction specified, so abort
                    ClearText();
                    break;
            }

            // If outside of map, complain
            if (playerX + deltaX < 0 || playerX + deltaX >= MAP_WIDTH ||
                playerY + deltaY < 0 || playerY + deltaY >= MAP_HEIGHT)
                
            {
                // Let the user know everything went well
                Console.SetCursorPosition(2, MAP_HEIGHT + 4);
                Console.WriteLine("Nothing to use the key on!");
                return;
            }

            // If there's a locked door, unlock it
            if (levelOneArray[playerY + deltaY, playerX + deltaX] == TILE_LOCKED_DOOR)
            {
                // unlock the door
                levelOneArray[playerY + deltaY, playerX + deltaX] = TILE_CLOSED_DOOR;

                // Let the user know everything went well
                inventory[keySlot] = ITEM_NONE;
                ClearText();
                Console.SetCursorPosition(2, MAP_HEIGHT + 3);
                Console.WriteLine("You unlock the door with the key!");
                Console.SetCursorPosition(2, MAP_HEIGHT + 4);
                Console.WriteLine("The key mysteriously vanished...");
        

            }

            // lock an unlocked door
            else if (levelOneArray[playerY + deltaY, playerX + deltaX] == TILE_CLOSED_DOOR)
            {
                // lock the door
                levelOneArray[playerY + deltaY, playerX + deltaX] = TILE_LOCKED_DOOR;

                // Let the user know everything went well
                Console.SetCursorPosition(2, MAP_HEIGHT + 4);
                Console.WriteLine("You lock the door with the key.");

            }
            // catch when the user tries to lock an open door, warn them
            else if (levelOneArray[playerY + deltaY, playerX + deltaX] == TILE_OPEN_DOOR)
            {
                // warn the user
                Console.SetCursorPosition(2, MAP_HEIGHT + 4);
                Console.WriteLine("Try closing the door first");

            }

            // don't know what this tile type is
            else
            {
                // Complain to the user
                Console.SetCursorPosition(2, MAP_HEIGHT + 4);
                Console.WriteLine("Nothing to use the key on!");
            }

            // Wait 2 seconds and then delete the message
            System.Threading.Thread.Sleep(2500);
            ClearText();
        }

        public void SwitchRooms()
        {
            // Ask the user which direction he wants to axe in
            Console.SetCursorPosition(2, MAP_HEIGHT + 3);
            Console.WriteLine("Use exit: which direction?");

            // Get the user input for the direction
            ConsoleKey key = Console.ReadKey().Key;
            int deltaX = 0;
            int deltaY = 0;

            // Compute which tile he user specified
            switch (key)
            {
                // SOUTH
                case ConsoleKey.DownArrow:
                    deltaX = 0;
                    deltaY = 1;
                    break;

                // WEST
                case ConsoleKey.LeftArrow:
                    deltaX = -1;
                    deltaY = 0;
                    break;

                // EAST
                case ConsoleKey.RightArrow:
                    deltaX = 1;
                    deltaY = 0;
                    break;

                // NORTH
                case ConsoleKey.UpArrow:
                    deltaX = 0;
                    deltaY = -1;
                    break;

                // Not a valid direction
                default:
                    // no direction specified, so abort
                    ClearText();
                    break;
            }

            if (playerX + deltaX < 0 || playerX + deltaX >= MAP_WIDTH ||
                 playerY + deltaY < 0 || playerY + deltaY >= MAP_HEIGHT)
            {
                // Let the user know everything went well
                Console.SetCursorPosition(2, MAP_HEIGHT + 4);
                Console.WriteLine("There isn't an exit here!");

                // Wait 2 seconds and then delete the message
                System.Threading.Thread.Sleep(2500);
                ClearText();
                return;
            }

            if (levelOneArray[playerY + deltaY, playerX + deltaX] != TILE_EXIT)
            {
                // complain about there not being an exit there
                Console.SetCursorPosition(2, MAP_HEIGHT + 4);
                Console.WriteLine("There isn't an exit here.");

                // Wait 2 seconds and then delete the message
                System.Threading.Thread.Sleep(2500);
                ClearText();
                return;
            }

            ClearText();

            // Change the current level
            currentLevel++;

            // Import the new level
            readLevelTiles();
            readLevelItems();

        }

    
        /// <summary>
        /// Function to clear the on-screen text (instead of Console.Clear())
        /// to ensure that text being overwritten can't be seen in the background
        /// if the previous strings were longer than the new ones
        /// i.e. if you write 'axe' where 'potion' was, you shouldn't be able to see 'axeion'
        /// </summary>
        public void ClearText()
        {
                        /** COMMANDS TO FIX INVENTORY TEXT ISSUES **/

            Console.Clear();
            DrawScreen(MAP_WIDTH, MAP_HEIGHT, levelOneArray);
            // Draw the inventory
            ShowInventory();

            // Set the player's new position
            Console.SetCursorPosition(playerX, playerY);

            // Draw the player
            Console.Write('@');
        }

        public void Update()
        {
            // First check of the game, see if the screen has been initialized
            // If it hasn't, let's give the player the Start menu

            if (screen == null)
            {
                // Show the start menu
                screen = new Screen();

                // Set the phase of the screen to the Start screen
                screen.CurrentPhase = Screen.Phase.Start;
            }
        }

        public void Main()
        {
            // At the start of the game, read the current level's tiles and items text documents
            readLevelTiles();
            readLevelItems();

            // delta movement variables
            int deltaX = 0;
            int deltaY = 0;

            // Create an NPC
            //Actor npc = new Actor(levelOneArray);
            //npc.setAppearance('@', ConsoleColor.Red);
            //npc.setPos(3, 3);

            while (true)
            {
                if (currentLevel == 5)
                {
                    Console.SetCursorPosition(2, MAP_HEIGHT + 4);
                    Console.WriteLine("Happy Anniversary, babe. I love you.");
                }

                if (health <= 0)
                {
                    // Let the user know that they lost
                    // Ask if they would like to play again
                    Console.SetCursorPosition(2, MAP_HEIGHT + 4);
                    Console.WriteLine("You have died, you riff-raff. Try again? (Y/N)");

                    ConsoleKey key = ConsoleKey.D;
                    while (key != ConsoleKey.Y || key != ConsoleKey.N)
                    {
                        key = Console.ReadKey().Key;
                        switch (key)
                        {
                            case ConsoleKey.Y:
                                // Reset the level, inventory, player position and health
                                // and start a new game
                                currentLevel = 1;
                                inventory = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                                ClearText();
                                playerX = 10;
                                playerY = 10;
                                health = 100;
                                Main();
                                break;

                            case ConsoleKey.N:
                                // Exit the game
                                Environment.Exit(0);
                                break;
                            default:
                                // Continue to complain until the player picks yes or no
                                Console.WriteLine("You have died, you riff-raff. Try again? (Y/N)");
                                break;
                        }
                    }
                }

                // Run the Update method
                Update();

                // Check if the player is on the start screen
                if (screen.CurrentPhase == Screen.Phase.Start)
                {
                    // Draw the start screen, exit until the player chooses another screen
                    DrawStartMenu();

                }

                else if (screen.CurrentPhase == Screen.Phase.Play)
                {
                    
                    Console.CursorVisible = false;
                    // Draw the map                 
                    DrawScreen(MAP_WIDTH, MAP_HEIGHT, levelOneArray);

                    // Draw the inventory
                    ShowInventory();

                    // Draw the player's health
                    // This needs to be moved to the drawScreen function
                    Console.SetCursorPosition(MAP_WIDTH + 20, 1);
                    Console.Write("Health: " + health.ToString() + ' ' + ' ');
                    Console.SetCursorPosition(MAP_WIDTH + 20, 2);
                    Console.WriteLine("------------");

                    // Draw the controls of the game
                    Console.SetCursorPosition(MAP_WIDTH + 20, 5);
                    Console.WriteLine("Controls:");
                    Console.SetCursorPosition(MAP_WIDTH + 20, 6);
                    Console.WriteLine("------------");
                    Console.SetCursorPosition(MAP_WIDTH + 20, 8);
                    Console.WriteLine("(O)pen Door");
                    Console.SetCursorPosition(MAP_WIDTH + 20, 9);
                    Console.WriteLine("(G)rab Item");
                    Console.SetCursorPosition(MAP_WIDTH + 20, 10);
                    Console.WriteLine("(D)rop Item");
                    Console.SetCursorPosition(MAP_WIDTH + 20, 11);
                    Console.WriteLine("(U)se Item");
                    Console.SetCursorPosition(MAP_WIDTH + 20, 12);
                    Console.WriteLine("(M)enu");
                    Console.SetCursorPosition(MAP_WIDTH + 20, 13);
                    Console.WriteLine("(S)witch Rooms (at an exit)");
                    Console.SetCursorPosition(MAP_WIDTH + 20, 14);
                    Console.WriteLine("(Esc)ape - exit game");

                    // Set the player's new position
                    Console.SetCursorPosition(playerX, playerY);

                    // Draw the player
                    Console.Write('@');

                    // Draw the npc
                    //npc.Update();
                    //npc.Draw();

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
                            // Draw the screen
                            //DrawScreen(MAP_WIDTH, MAP_HEIGHT, levelOneArray);

                            // Set the cursor to the player so we can draw him
                            //Console.SetCursorPosition(playerX, playerY);

                            // Draw the player
                            //Console.Write('@');
                             
                            // Open the door
                            OpenDoorCommand();
                            break;

                        // Close door
                        case ConsoleKey.C:
                            // Draw the screen
                            DrawScreen(MAP_WIDTH, MAP_HEIGHT, levelOneArray);

                            // Set the player's new position
                            Console.SetCursorPosition(playerX, playerY);

                            // Draw the player
                            Console.Write('@');

                            // Close the door
                            CloseDoorCommand();

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

                        case ConsoleKey.U:
                            // use item
                            UseItemCommand();

                            break;

                        case ConsoleKey.M:
                            // pause menu
                            screen.CurrentPhase = Screen.Phase.Pause;
                            break;

                        case ConsoleKey.Escape:
                            // Quit the game
                            Environment.Exit(0);
                            break;

                        case ConsoleKey.S:
                            // Switch rooms
                            SwitchRooms();
                            break;

                        default:
                            break;
                    }



                    if (isPassable(playerX + deltaX, playerY + deltaY))
                    {
                        previousPlayerX = playerX;
                        previousPlayerY = playerY;


                        // If allowed, move in that direction
                        playerX += deltaX;
                        playerY += deltaY;

                        
                        // FOR THE POISON WEAKNESS, HEALTH GOES DOWN BY 1
                        if (previousPlayerX == playerX && previousPlayerY == playerY)
                        {

                        }

                        else
                        {
                            health--;
                        }

                    }

                    //clear the delta variables
                    deltaX = 0;
                    deltaY = 0;


                }

                else if (screen.CurrentPhase == Screen.Phase.Pause)
                {
                    // Clear the play menu, draw the pause menu until play is hit again
                    Console.Clear();
                    DrawPauseMenu();
                }
            }
        }
    }
}
