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
            int newNodeID = Devices.newNode(name);
            if (newNodeID != -1)
            {
                Console.WriteLine("\tNode created successfully");
                Console.WriteLine("\tNode name: {0} , node id: {1}", Devices.getNameByID(newNodeID), newNodeID);
            } 
            Console.Write("Press any key to return...");
            Console.ReadKey();

        }

        //option 2
        private static void selectNodeDialog()
        {
            Devices.setSelectedNode(promptNode());
            while(Devices.getSelectedNodeID() != -1)
            {
                selectedNodeOptionsDialog();
            }
        }

        //print what can be done with the selected node
        private static void selectedNodeOptionsDialog()
        {
            Console.Clear();
            Console.WriteLine("Selected node: {0} - ID: {1}" ,Devices.getNameByID(Devices.getSelectedNodeID()) , Devices.getSelectedNodeID());
            Console.WriteLine("\tChoose one of the options below:");
            Console.WriteLine("\t 1. scan and connect");
            Console.WriteLine("\t 2. send a message");
            Console.WriteLine("\t 3. see sent messages log");
            Console.WriteLine("\t 4. see recieved messages log");
            //Console.WriteLine("\t 5. disconnect");
            Console.WriteLine("\t 5. see piconet status");
            Console.WriteLine("\t 6. unselect node");
            string k = Console.ReadLine();
            switch (k)
            {
                case "1":
                    scan();
                    break;
                case "2":
                    sendMessageDialog();
                    break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Devices.printSentMessages(Devices.getSelectedNodeID());
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "4":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Devices.printRecievedMessages(Devices.getSelectedNodeID());
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    break;
                //case "5":
                //    //disconnect
                //    break;
                case "5":
                    printPiconetStatus();
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "6":
                    Devices.setSelectedNode(-1);
                    break;
            }
        }

        //scan
        private static void scan()
        {
            //print near networks (every node in the same piconet is considered near.)
            int targetNodeID = promptNode(Devices.getSelectedNodeID());
            Console.WriteLine("Connecting to {0}", Devices.getNameByID(targetNodeID));
            connect(targetNodeID);
            Console.Write("Press any key to return...");
            Console.ReadKey();
        }

        //connect to selected target
        private static void connect(int targetID)
        {
            Devices.connect(targetID);
        }

        private static int promptNode(int excludeID = 0)
        {
            int[] IDs = Devices.getNodeIDs();
            string[] names = Devices.getNodeNames();
            int nodeCount = Devices.getNodeCount();
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
        
        private static void sendMessageDialog()
        {
            int target_node = promptPiconetNodes();
            if(target_node != -1)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Enter your message: ");
                Console.ForegroundColor = ConsoleColor.White;
                string message = Console.ReadLine();
                int sent = sendMessage(target_node, message);
                if(sent == 0)
                    Console.WriteLine("Message sent!");
                else
                    Console.WriteLine("ERROR! Message was not sent!");
            }
            else
            {
                Console.WriteLine("Invalid choice!");
            }
            Console.Write("Press any key to return...");
            Console.ReadKey();
        }

        private static int sendMessage(int target_node, string message)
        {
            return Devices.sendPacket(Devices.getSelectedNodeID(), target_node, message);
        }

        private static void printPiconetStatus()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            int selected_node_id = Devices.getSelectedNodeID();
            int piconet_id = Devices.getPiconetID(selected_node_id);
            Console.WriteLine("Piconet ID: {0}", piconet_id);
            Console.WriteLine("\t#\t|\tname\t|\tnode id\t|\tmaster");
            int piconet_node_count = Devices.getPiconetNodeCountByNodeID(selected_node_id);
            int[] piconet_nodes = Devices.getPiconetNodesByNodeID(selected_node_id);
            for(int node_index = 0; node_index<piconet_node_count; node_index++)
            {
                int node_id = piconet_nodes[node_index];
                string node_name = Devices.getNameByID(node_id);
                bool node_is_master = Devices.getMasterOrSlave(node_id);
                Console.WriteLine("\t{0}\t|\t{1}\t|\t{2}\t|\t{3}", node_index, node_name, node_id, node_is_master ? "*" : " ");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static int promptPiconetNodes()
        {
            int exclude_index = -1;
            Console.ForegroundColor = ConsoleColor.Red;
            int selected_node_id = Devices.getSelectedNodeID();
            int piconet_id = Devices.getPiconetID(selected_node_id);
            Console.WriteLine("\t#\t|\tname\t|\tnode id\t|");
            int piconet_node_count = Devices.getPiconetNodeCountByNodeID(selected_node_id);
            int[] piconet_nodes = Devices.getPiconetNodesByNodeID(selected_node_id);
            for (int counter = 0, excluded_counter = 0; counter < piconet_node_count; counter++)
            {
                int node_id = piconet_nodes[counter];
                if (node_id == selected_node_id)
                {
                    exclude_index = counter;
                }
                else
                {
                    string node_name = Devices.getNameByID(node_id);
                    bool node_is_master = Devices.getMasterOrSlave(node_id);
                    Console.WriteLine("\t{0}\t|\t{1}\t|\t{2}\t|", excluded_counter, node_name, node_id);
                    excluded_counter++;
                }
                
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Select one of the above nodes: ");
            int target_candidate;
            try
            {
                target_candidate = int.Parse(Console.ReadLine());
            }
            catch
            {
                target_candidate = -1;
            }
            if (target_candidate >= 0 && target_candidate < piconet_node_count-1)
            {
                if (target_candidate < exclude_index)
                    return piconet_nodes[target_candidate];
                else
                    return piconet_nodes[target_candidate + 1];
            }
            else
            {
                return -1;
            }
        }

        //option 3
        private static void printStatus()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t#\t|\tname\t|\tnode_id\t|\tmaster/slave\t|\t");
            int[] IDs = Devices.getNodeIDs();
            string[] names = Devices.getNodeNames();
            int nodeCount = Devices.getNodeCount();
            if (nodeCount != 0)
            {
                for(int counter = 0; counter < nodeCount; counter++)
                {
                    Console.WriteLine("\t{0}\t|\t{1}\t|\t{2}\t|\t{3}\t|\t", counter, names[counter], IDs[counter], Devices.getMasterOrSlave(IDs[counter]) ? "master" : "slave");
                }
            }
            else
            {
                Console.WriteLine("\t-\t|\t----------\t|\t-------\t|\t-------------\t|\t");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Press any key to return...");
            Console.ReadKey();
        }
    }
}
