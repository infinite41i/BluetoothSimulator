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
    class Piconet
    {
        public Node[] nodes = new Node[7];
        private int id;
        private int nodeCount = 0;
        private Channel channel = new Channel();

        public Piconet(int id)
        {
            this.id = id;
        }

        public int getID()
        {
            return id;
        }

        public void newNode(string name)
        {
            nodes[nodeCount] = new Node(name);
            nodeCount++;
        }

        public Node[] getNodes()
        {
            return nodes;
        }

        public int getNodeCount()
        {
            return nodeCount;
        }

        public Node getLastNode()
        {
            return nodes[nodeCount-1];
        }

        public void sendPacket(Packet packet)
        {
            //
        }
    }
}
