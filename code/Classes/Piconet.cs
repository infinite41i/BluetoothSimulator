using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothSimulator
{
    class Piconet
    {
        public Node[] nodes;
        private int nodeCount = 0;
        Channel channel = new Channel();

        public void newNode(string name)
        {
            nodes[nodeCount] = new Node(name);
        }
    }
}
