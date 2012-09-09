using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDSA12;

namespace GUIGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GOFRunner.Run(new World(10));
        }
    }
}
