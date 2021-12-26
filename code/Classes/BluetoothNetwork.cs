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
        public static int getRandomNumber()
        {
            return randomNumberGenerator.Next();
        }
    }
}
