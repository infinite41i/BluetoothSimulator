using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothSimulator
{
    class Node
    {
        private int BTGUID;
        private string BTname;
        private bool master = false;
        private NodeMode mode;

        public Node(string name)
        {
            BTGUID = BluetoothNetwork.getRandomNumber();
            BTname = name;
            mode = NodeMode.Standby;
        }

        public void recievePacket()
        {
            //
        }
    }
}
