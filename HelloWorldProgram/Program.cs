using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Authors: CSTP, MROF, EENG
 * */
namespace HelloWorldProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Only one name please, you entered: {0}", args.Length);
            }
            else if (args.Length == 1)
            {
                Console.WriteLine("Hello " + args[0]);
            }
            else
            {
                
            }
                Console.ReadKey();
        }
    }
}
