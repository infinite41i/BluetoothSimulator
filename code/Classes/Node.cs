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
    partial class Devices
    {
        public class Node
        {
            private int BTGUID;
            private string BTname;
            private bool master = false;
            private NodeMode mode = NodeMode.Standby;
            private int piconet_id = -1;
            private int[] piconet_nodes = new int[BTConsts.maxDevicesInPiconet];
            private int piconet_node_index = 0; //node index and node count
            private int master_id = -1;
            private Queue<Packet> sendLog = new Queue<Packet>(BTConsts.logCapacity);//keep 10 last packets
            private Queue<Packet> recieveLog = new Queue<Packet>(BTConsts.logCapacity);//keep 10 last packets

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

            public int getPiconetID()
            {
                return piconet_id;
            }

            public int[] getPiconetNodes()
            {
                if (master)
                    return piconet_nodes;
                else if (master_id != -1)
                {
                    int master_id = Devices.getMasterID(BTGUID);
                    Node master_node = Devices.getNodeByID(master_id);
                    return master_node.getPiconetNodes();
                }
                else
                    return null;
            }

            public int getPiconetNodeCount()
            {
                if (master)
                    return piconet_node_index;
                else if (master_id != -1)
                {
                    int master_id = Devices.getMasterID(BTGUID);
                    Node master_node = Devices.getNodeByID(master_id);
                    return master_node.piconet_node_index;
                }
                else return 0;
            }

            public int connect(int target_id)
            {
                if(piconet_id == -1)
                {
                    int creation_success = createPiconet();
                    if (creation_success == -1)
                        return -1;//error in creating piconet
                }
                if(master == true)
                {
                    int connection_success = addNodeToPiconet(target_id);
                    if (connection_success == -1)
                        return -2;//error in connecting the node
                    else return 1;//success
                }
                else
                {
                    return -3;//selected node is not a master
                }
            }

            private int createPiconet()
            {
                try
                {
                    piconet_id = Program.getRandomNumber();//set piconet ID
                    piconet_nodes[0] = BTGUID;//add self to piconet
                    piconet_node_index = 1;//increment piconet nodes count/index
                    master = true; master_id = BTGUID;//add self as master
                    Console.WriteLine("\tpiconet_id of master = {0}", getPiconetID());
                    return 1;
                }
                catch
                {
                    return -1;
                }
                
            }

            private int addNodeToPiconet(int targetNodeID)
            {
                Node target_node = Devices.getNodeByID(targetNodeID);
                int target_node_piconet_id = target_node.getPiconetID();
                if (target_node_piconet_id == -1)
                {
                    //add target node to piconet list of starter node
                    piconet_nodes[piconet_node_index] = target_node.getGUID();
                    piconet_node_index++;

                    //set target node as it has a piconet
                    target_node.piconet_id = piconet_id;

                    //set target node's master_id
                    target_node.master_id = master_id;
                    Console.WriteLine("\tpiconet_id of target = {0}", target_node.getPiconetID());
                    return 1;//connection successful
                }
                else
                {
                    return -1; //node is in a network or a problem occured
                }
            }

            public int getMasterID()
            {
                return master_id;
            }

            public int sendPacket(int sender_id, int reciever_id, string message)
            {
                Packet packet = new Packet(sender_id, reciever_id, message);
                Node reciever = Devices.getNodeByID(reciever_id);
                int reciever_piconet = Devices.getNodeByID(packet.getRecieverID()).piconet_id;
                if (piconet_id == -1)
                {
                    return -1;//device is not in any piconet
                }
                else if (reciever_piconet == piconet_id) //two devices are in same piconet
                {
                    sendLog.Enqueue(packet);
                    reciever.recieveLog.Enqueue(packet);
                    return 0;
                }
                else if (reciever_piconet == -1)
                {
                    return -2;//second node is not in a piconet
                }
                else
                {
                    return -3;//second node is not in the same piconet with first node
                }
                

            }

            public void printSentMessages()
            {
                Queue<Packet> packetsCopy = new Queue<Packet>(sendLog.ToArray());

                Console.WriteLine("Sent Messages:");
                int counter = 1;
                foreach(Packet packet in packetsCopy)
                {
                    Console.WriteLine("{0}: to '{1}' / time: {2} / message: '{3}'", counter, packet.getRecieverID(), packet.getDateTime(), packet.getMessage());
                    counter++;
                }
            }

            public void printRecievedMessages()
            {
                Queue<Packet> packetsCopy = new Queue<Packet>(recieveLog.ToArray());

                Console.WriteLine("Recieved Messages:");
                int counter = 1;
                foreach (Packet packet in packetsCopy)
                {
                    Console.WriteLine("{0}: from '{1}' / time: {2} / message: '{3}'", counter, Devices.getNameByID(packet.getRecieverID()), packet.getDateTime(), packet.getMessage());
                    counter++;
                }
            }
        }
    }
    
}
