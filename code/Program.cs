using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluetoothSimulator.Classes;
using BluetoothSimulator.Classes.enums;
using BluetoothSimulator.Classes.Consts;

namespace BluetoothSimulator
{
    public static class Program
    {
        private static bool running = true;
        private static Random random = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bluetooth Simulator Program!");
            BTConsole.printBTLogo();
            Console.Write("Press any key to start...");
            Console.ReadKey();
            while (running == true)
            {
                BTConsole.printMainMenu();
            }
        }

        public static void stop()
        {
            running = false;
        }

        public static int getRandomNumber()
        {
            return random.Next();
        }
    }
}
