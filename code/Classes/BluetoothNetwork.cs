using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothSimulator
{
    class BluetoothNetwork
    {
        private static Random randomNumberGenerator = new Random();
        private Piconet piconet = new Piconet(BluetoothNetwork.getRandomNumber());
        private Node selectedNode;
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

        public Node getLastNode()
        {
            return this.piconet.getLastNode();
        }

        public static int getRandomNumber()
        {
            return randomNumberGenerator.Next();
        }
    }
}
