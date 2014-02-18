using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConsoleApplication1
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application
        /// </summary>
        /// <param name="args"></param>
          static void Main(string[] args)
         {


             using (Game theGame = new Game())
             {
                 theGame.Main();
             }

         }

    }
}
