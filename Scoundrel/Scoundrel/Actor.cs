using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Actor
    {
        // Map dimensions
        const int MAP_WIDTH = 20;
        const int MAP_HEIGHT = 15;

        // horizontal coordinate of the actor, relative to the level's origin
        protected int posX;

        // veritical coordinate of the actor, relative to the level's origin
        protected int posY;

        // ASCII character code used to draw the actor to the screen
        char displayChar;

        // Color code for the actor
        ConsoleColor colorCode;

        //listIndex
        List<TileType> listIndex = new List<TileType>(); // list to store TileTypes

        // Tell the player about the map he's freaking on, jeez
        protected int[,] map = new int[15, 20];

        public Actor(int[,] levelMap)
        {
            //this.map = levelMap;

            for (int y = 0; y < MAP_HEIGHT; y++)
            {
                for (int x = 0; x < MAP_WIDTH; x++)
                {
                    map[y,x] = levelMap[y,x];
                }
            }


            // Add TileTypes to the tileIndex
            listIndex.Add(new TileType('.', ConsoleColor.White, true)); // tile floor
            listIndex.Add(new TileType('#', ConsoleColor.Red, false)); // tile wall
            listIndex.Add(new TileType('T', ConsoleColor.Green, false)); // tree
            listIndex.Add(new TileType('/', ConsoleColor.Magenta, false)); // closed door
            listIndex.Add(new TileType('_', ConsoleColor.Magenta, true)); // open door
            listIndex.Add(new TileType('I', ConsoleColor.Magenta, false)); // locked door

        }

        // Changes how the actor appears in the game world
        public void setAppearance(char c, ConsoleColor s)
        {
            this.displayChar = c;
            this.colorCode = s;
        }

        // Changes the position of the actor
        public void setPos(int x, int y)
        {
            if (x < 0 || x >= MAP_WIDTH ||
                y < 0 || y >= MAP_HEIGHT)
            {
                return;
            }
            this.posX = x;
            this.posY = y;
        }


        bool isPassable(int mapX, int mapY)
        {
            // Before we do anything, make sure that the coordinates are valid
            if (mapX < 0 || mapX >= MAP_WIDTH || mapY < 0 || mapY >= MAP_HEIGHT)
                return false;

            // Store the value of the tile specified
            int tileValue = this.map[mapY, mapX];

            // Return if it's passable or not
            return listIndex[tileValue].isPassable;
        }


        public void Draw()
        {
            // Skip drawing if the actor's coordinates aren't on the map
            if (this.posX < 0 || this.posX >= MAP_WIDTH ||
                 this.posY < 0 || this.posY >= MAP_HEIGHT)
            {
                return;
            }

            // Draw the actor as it wants to be drawn
            Console.ForegroundColor = this.colorCode;
            Console.SetCursorPosition(posX, posY);
            Console.Write(displayChar);
        }

        public void Update()
        {
            Random random = new Random();
            // Generate a new set of deltas for this actor
            int deltaX = random.Next(-1, 2);
            int deltaY = random.Next(-1, 2);

            if (isPassable(this.posX + deltaX, this.posY + deltaY))
            {

                this.posX += deltaX;
                this.posY += deltaY;
            }
        }

    }
}
