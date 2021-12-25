using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothSimulator
{
    class Program
    {
        private static bool running = true;
        static void Main(string[] args)
        {
            while (running == true)
            {
                Console.WriteLine("Welcome to Bluetooth Simulator Program!");
                Console.ForegroundColor = ConsoleColor.Blue;
                ConsoleHandler.printBTLogo();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Press any key to start...");
                string k = Console.ReadLine();
                switch (k)
                {
                    case "exit":
                        running = false;
                        break;
                    case "1":
                        //
                        break;
                }
            }
        }
    }
}
