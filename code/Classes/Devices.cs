using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluetoothSimulator.Classes;
using BluetoothSimulator.Classes.enums;
using BluetoothSimulator.Classes.Consts;

namespace BluetoothSimulator.Classes
{
    public static partial class Devices
    {
        private static Node[] nodes = new Node[BTConsts.maxDevices];
        private static int nodeCount = 0;
        private static int selectedNode = -1;

        public static int newNode(string name)
        {
            try
            {
                nodes[nodeCount] = new Node(name);
                nodeCount++;
                return nodes[nodeCount - 1].getGUID();
            }
            catch
            {
                Console.WriteLine("Error creating new node!");
                return -1;
            }
            
        }

        public static int getSelectedNodeID()
        {
            return selectedNode;
        }

        public static void setSelectedNode(int selected_node_id)
        {
            selectedNode = selected_node_id;
        }

        private static Node getNodeByID(int node_id)
        {
            foreach (Node node in nodes)
            {
                if (node.getGUID() == node_id)
                {
                    return node;
                }
            }
            return null;
        }

        public static string getNameByID(int node_id)
        {
            foreach(Node node in nodes)
            {
                if(node.getGUID() == node_id)
                {
                    return node.getName();
                }
            }
            return null;
        }

        public static int getNodeCount()
        {
            return nodeCount;
        }

        public static int[] getNodeIDs()
        {
            int[] IDs = new int[BTConsts.maxDevices];
            int counter = 0;
            foreach(Node node in nodes)
            {
                if(node != null)
                {
                    IDs[counter] = node.getGUID();
                }
                else
                {
                    IDs[counter] = -1;
                }
                counter++;
            }
            return IDs;
        }

        public static string[] getNodeNames()
        {
            string[] names = new string[BTConsts.maxDevices];
            int counter = 0;
            foreach (Node node in nodes)
            {
                if (node != null)
                {
                    names[counter] = node.getName();
                }
                else
                {
                    names[counter] = null;
                }
                counter++;
            }
            return names;
        }

        public static bool getMasterOrSlave(int node_id)
        {
            return getNodeByID(node_id).getMasterorSlave();
        }

        public static void connect(int targetNodeID)
        {
            if(selectedNode == -1)
            {
                Console.WriteLine("Select a node first");
                return;
            }
            Node starter_node = Devices.getNodeByID(selectedNode);
            int connection_success = starter_node.connect(targetNodeID);
            switch (connection_success)
            {
                case 1:
                    Console.WriteLine("Connection Successful!");
                    break;
                case -1:
                    Console.WriteLine("Error in creating piconet!");
                    break;
                case -2:
                    Console.WriteLine("Error in connecting the node: node is in a network or a problem occured.");
                    break;
                case -3:
                    Console.WriteLine("Selected node is in a network but is not a master.");
                    break;
            }
            return;
        }

        public static int getPiconetID(int node_id)
        {
            return Devices.getNodeByID(node_id).getPiconetID();
        }

        public static int[] getPiconetNodesByNodeID(int nodeID)
        {
            return getNodeByID(nodeID).getPiconetNodes();
        }

        public static int getPiconetNodeCountByNodeID(int node_id)
        {
            return getNodeByID(node_id).getPiconetNodeCount();
        }

        public static int getMasterID(int node_id)
        {
            return getNodeByID(node_id).getMasterID();
        }

        public static int sendPacket(int sender_id, int reciever_id, string message)
        {
            Node sender = Devices.getNodeByID(sender_id);
            return sender.sendPacket(sender_id, reciever_id, message);
        }

        public static void printSentMessages(int node_id)
        {
            Devices.getNodeByID(node_id).printSentMessages();
        }

        public static void printRecievedMessages(int node_id)
        {
            Devices.getNodeByID(node_id).printRecievedMessages();
        }
    }
}
