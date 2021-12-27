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
        private int lastIndex = 0;
        Channel channel = new Channel();

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
            nodes[lastIndex] = new Node(name);
            lastIndex++;
        }

        public Node[] getNodes()
        {
            return nodes;
        }

        public Node getLastNode()
        {
            return nodes[lastIndex-1];
        }
    }
}
