using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Screen
    {
        // The different phases of the screen
        public enum Phase
        {
            Start,
            Play,
            Credits, 
            Pause
        }

        // Get or set the current phase of the screen
        public Phase CurrentPhase { get; set; }

        // Default constructor
        public Screen()
        {
            // Do stuff
        }

    }
}
