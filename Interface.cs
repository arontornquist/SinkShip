using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinkShip
{
    class Interface
    {
        public string AskForString(string output)
        {
            Console.Write(output);
            return Console.ReadLine();
        }
    }
}
