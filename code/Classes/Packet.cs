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
    class Packet
    {
        private int sender_id;
        private int reciever_id;
        private PacketTypes packetType;
        private string message;

        public Packet(int sender_id, int reciever_id, PacketTypes packetType, string message)
        {
            this.sender_id = sender_id;
            this.reciever_id = reciever_id;
            this.packetType = packetType;
            this.message = message;
        }
    }
}
