using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothSimulator
{
    class Program
    {
        public static bool running = true;
        public static BluetoothNetwork bluetoothNetwork = new BluetoothNetwork();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bluetooth Simulator Program!");
            ConsoleHandler.printBTLogo();
            Console.Write("Press any key to start...");
            Console.ReadKey();
            while (running == true)
            {
                ConsoleHandler.printMainMenu();
            }
        }
    }
}
