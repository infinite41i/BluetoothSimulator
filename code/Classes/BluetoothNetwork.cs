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
    class BluetoothNetwork
    {
        private static Random randomNumberGenerator = new Random();
        private Piconet piconet = new Piconet(BluetoothNetwork.getRandomNumber());
        private Node selectedNode = null;
        public int getPiconetID()
        {
            return this.piconet.getID();
        }

        public int newNode(string name)
        {
            try
            {
                this.piconet.newNode(name);
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return 0;
            }
            
        }

        public Node[] getNodes()
        {
            return this.piconet.getNodes();
        }

        public int getNodeCount()
        {
            return this.piconet.getNodeCount();
        }

        public Node getLastNode()
        {
            return this.piconet.getLastNode();
        }

        public Node getSelectedNode()
        {
            return this.selectedNode;
        }

        public void setSelectedNode(Node node)
        {
            this.selectedNode = node;
        }

        public static int getRandomNumber()
        {
            return randomNumberGenerator.Next();
        }
    }
}
