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
    public static partial class Piconet
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

        //public void sendPacket(Packet packet)
        //{
        //    //
        //}
    }
}
