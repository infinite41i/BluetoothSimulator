﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluetoothSimulator.Classes;
using BluetoothSimulator.Classes.enums;
using BluetoothSimulator.Classes.Consts;

namespace BluetoothSimulator.Classes
{
    partial class Piconet
    {
        public class Node
        {
            private int BTGUID;
            private string BTname;
            private bool master = true;
            private NodeMode mode;

            public Node(string name)
            {
                BTGUID = Program.getRandomNumber();
                BTname = name;
                mode = NodeMode.Standby;
            }

            public int getGUID()
            {
                return BTGUID;
            }

            public string getName()
            {
                return BTname;
            }

            public bool getMasterorSlave()
            {
                return master;
            }

            //public NodeMode getMode()
            //{
            //    return mode;
            //}

            public void connect(int target_id)
            {
                //Program.bluetoothNetwork
            }

            public void recievePacket()
            {
                //
            }
        }
    }
    
}
