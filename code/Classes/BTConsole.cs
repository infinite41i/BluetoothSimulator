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
    public static class BTConsole
    {
        private static int selectedNodeID = -1;
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
                    Program.stop();
                    break;
            }
        }
        
        //option 1
        private static void initNodeDialog()
        {
            Console.Clear();
            Console.Write("Enter new node name: ");
            string name = Console.ReadLine();
            int newNodeID = Piconet.newNode(name);
            if (newNodeID != -1)
            {
                Console.WriteLine("\tNode created successfully");
                Console.WriteLine("\tNode name: {0} , node id: {1}", Piconet.getNameByID(newNodeID), newNodeID);
            } 
            Console.Write("Press any key to return...");
            Console.ReadKey();

        }

        //option 2
        private static void selectNodeDialog()
        {
            selectedNodeID = promptNode();
            while(selectedNodeID != -1)
            {
                selectedNodeOptionsDialog();
            }
        }

        //print what can be done with the selected node
        private static void selectedNodeOptionsDialog()
        {
            Console.Clear();
            Console.WriteLine("Selected node: {0} - ID: {1}" ,Piconet.getNameByID(selectedNodeID) , selectedNodeID);
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
                    selectedNodeID = -1;
                    break;
            }
        }

        //scan
        private static void scan()
        {
            //print near networks (every node in the same piconet is considered near.)
            int targetNodeID = promptNode(selectedNodeID);
            Console.WriteLine(Piconet.getNameByID(targetNodeID));
            Console.ReadKey();
        }

        //connect to selected target
        private static void connect(int targetID)
        {
            //Program.bluetoothNetwork.getSelectedNode().
        }

        private static int promptNode(int excludeID = 0)
        {
            int[] IDs = Piconet.getNodeIDs();
            string[] names = Piconet.getNodeNames();
            int nodeCount = Piconet.getNodeCount();
            int excludeIndex = -1;
            
            if(nodeCount > 0)
            {
                //print nodes list
                Console.WriteLine("List of available nodes:");
                for (int counter = 0, excludedCounter = 0; counter < nodeCount; counter++)
                {
                    if (IDs[counter] == excludeID)
                    {
                        excludeIndex = counter;
                    }
                    else
                    {
                        Console.WriteLine("\t{0}. {1} : {2}", excludedCounter+1, names[counter], IDs[counter]);
                        excludedCounter++;
                    }
                }
                Console.Write("Choose one of the above nodes: ");
                int k;
                try
                {
                    k = int.Parse(Console.ReadLine());
                    k = k - 1;//node indexes are shown starting from 1 not 0
                }
                catch
                {
                    k = -1;
                }
                if(k >= 0 && k <nodeCount)
                {
                    if(excludeID != 0)
                    {
                        if(k < excludeIndex)
                        {
                            return IDs[k];
                        }
                        else
                        {
                            return IDs[k + 1];
                        }
                    }
                    else
                    {
                        return IDs[k];
                    }
                }
                else
                {
                    Console.WriteLine("Selection not valid!");
                    Console.Write("Press any key to return...");
                    Console.ReadKey();
                    return -1;
                }
            }
            else
            {
                Console.WriteLine("No node exists!");
                Console.Write("Press any key to return...");
                Console.ReadKey();
                return -1;
            }

        }
        
        //option 3
        private static void printStatus()
        {
            Console.WriteLine("\t#\t|\tnode_name\t|\tnode_id\t|\tmaster/slave\t|\t");
            int[] IDs = Piconet.getNodeIDs();
            string[] names = Piconet.getNodeNames();
            int nodeCount = Piconet.getNodeCount();
            if (nodeCount != 0)
            {
                for(int counter = 0; counter < nodeCount; counter++)
                {
                    Console.WriteLine("\t{0}\t|\t{1}\t|\t{2}\t|\t{3}\t|\t", counter, names[counter], IDs[counter], Piconet.getMasterOrSlave(IDs[counter]) ? "master" : "slave");
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
