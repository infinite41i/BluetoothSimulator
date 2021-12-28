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
                    selectNodeDialog();
                    break;
                case "3":
                    printStatus();
                    break;
                case "4":
                    Program.running = false;
                    break;
            }
        }

        //option 1
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

        //option 2
        public static void selectNodeDialog()
        {
            if (Program.bluetoothNetwork.getNodeCount() != 0)
            {
                Node[] nodes = Program.bluetoothNetwork.getNodes();
                int counter = 0;
                foreach (Node node in nodes)
                {
                    if (node != null)
                    {
                        counter++;
                        Console.WriteLine("{0}: {1} - {2}", counter, node.getName(), node.getGUID());
                    }
                }
                Console.Write("Choose one of the above nodes: ");
                int k = int.Parse(Console.ReadLine());
                if(k>0 && k<=counter)
                    Program.bluetoothNetwork.setSelectedNode(nodes[counter - 1]);
                else
                {
                    Console.WriteLine("Selected node does not exist!");
                    Console.Write("Press any key to return...");
                    Console.ReadKey();
                }
                while (Program.bluetoothNetwork.getSelectedNode() != null)
                {
                    selectedNodeOptionsDialog();
                }
                
            }
            else
            {
                Console.WriteLine("No node exists!");
                Console.Write("Press any key to return...");
                Console.ReadKey();
            }
        }

        //print what can be done with the selected node
        public static void selectedNodeOptionsDialog()
        {
            Console.Clear();
            Console.WriteLine("Selected node: {0}", Program.bluetoothNetwork.getSelectedNode().getName());
            Console.WriteLine("\tChoose one of the options below:");
            Console.WriteLine("\t 1. scan and connect");
            Console.WriteLine("\t 2. send a message");
            Console.WriteLine("\t 3. disconnect");
            Console.WriteLine("\t 4. unselect node");
            string k = Console.ReadLine();
            switch (k)
            {
                case "1":
                    scan();
                    break;
                case "2":
                    //
                    break;
                case "3":
                    //
                    break;
                case "4":
                    Program.bluetoothNetwork.setSelectedNode(null);
                    break;
            }

            //Console.WriteLine("Press any key to continue...");
            //Console.ReadKey();
        }

        //scan
        public static void scan()
        {
            //print near networks (every node in the same piconet is considered near.)
            Node[] nodes = Program.bluetoothNetwork.getNodes();
            int counter = 0;
            foreach (Node node in nodes)
            {
                if (node != null)
                {
                    counter++;
                    if (Program.bluetoothNetwork.getSelectedNode().getGUID() != nodes[counter - 1].getGUID())
                        Console.WriteLine("{0}: {1} - {2}", counter, node.getName(), node.getGUID());
                }
            }
            Console.Write("Choose one of the above nodes: ");
            int k;
            try
            {
                k = int.Parse(Console.ReadLine());
            }
            catch
            {
                k = -1;
            }
            if (k > 0 && k <= counter)
            {
                if(Program.bluetoothNetwork.getSelectedNode().getGUID() == nodes[k - 1].getGUID())
                {
                    Console.WriteLine("Can't connect to self.");
                }
                //connect to node[counter - 1]
            }
            else
            {
                Console.WriteLine("Selection not valid!");
            }
            Console.Write("Press any key to return...");
            Console.ReadKey();    
        }

        //connect to selected target
        public static void connect(int target_id)
        {
            //Program.bluetoothNetwork.getSelectedNode().
        }
        
        //option 3
        public static void printStatus()
        {
            Console.WriteLine("\t#\t|\tnode_name\t|\tnode_id\t|\tmaster/slave\t|\t");
            if (Program.bluetoothNetwork.getNodeCount() != 0)
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
