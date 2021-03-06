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
        private string message;
        private DateTime datetime;

        public Packet(int sender_id, int reciever_id, string message)
        {
            this.sender_id = sender_id;
            this.reciever_id = reciever_id;
            this.message = message;
            this.datetime = DateTime.Now;
        }

        public int getSenderID()
        {
            return sender_id;
        }

        public int getRecieverID()
        {
            return reciever_id;
        }

        public string getMessage()
        {
            return message;
        }

        public string getDateTime()
        {
            return datetime.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}
