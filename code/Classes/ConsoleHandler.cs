using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluetoothSimulator.Classes;
using BluetoothSimulator.Classes.enums;
using BluetoothSimulator.Classes.Consts;

namespace BluetoothSimulator.Classes
{
    public static class ConsoleHandler
    {
        public static void printBTLogo()
        {
            String line;
            Console.ForegroundColor = ConsoleColor.Blue;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("./assets/logo.txt");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void printMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose one of the options below:");
            Console.WriteLine("\t1. create a node");
            Console.WriteLine("\t2. select a node");
            Console.WriteLine("\t3. see status");
            Console.WriteLine("\t4. exit");
            string k = Console.ReadLine();
            switch (k)
            {
                case "1":
                    initNodeDialog();
                    break;
                case "2":
                    //
                    break;
                case "3":
                    printStatus();
                    break;
                case "4":
                    Program.running = false;
                    break;
            }
        }

        public static void initNodeDialog()
        {
            Console.Clear();
            Console.Write("Enter new node name: ");
            string name = Console.ReadLine();
            if(Program.bluetoothNetwork.newNode(name) == 1)
            {
                Console.WriteLine("Node created successfully");
                Console.WriteLine("Node name: {0} , node id: {1}", Program.bluetoothNetwork.getLastNode().getName(), Program.bluetoothNetwork.getLastNode().getGUID());//complete here
            } 
            Console.Write("Press any key to return...");
            Console.ReadKey();

        }
        
        public static void printStatus()
        {
            Console.WriteLine("\t#\t|\tnode_name\t|\tnode_id\t|\tmaster/slave\t|\t");
            if (Program.bluetoothNetwork.getNodes() != null && Program.bluetoothNetwork.getNodes().Length != 0)
            {
                Node[] nodes = Program.bluetoothNetwork.getNodes();
                int counter = 0;
                foreach (Node node in nodes)
                {
                    if(node != null)
                    {
                        counter++;
                        Console.WriteLine("\t{0}\t|\t{1}\t|\t{2}\t|\t{3}\t|\t", counter, node.getName(), node.getGUID(), node.getMasterorSlave() ? "master" : "slave");
                    }
                }
            }
            else
            {
                Console.WriteLine("\t-\t|\t----------\t|\t-------\t|\t-------------\t|\t");
            }
            Console.Write("Press any key to return...");
            Console.ReadKey();
        }
    }
}
