using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            World world = new World(20);
            world.display();
            
            while (Console.ReadKey().Key != System.ConsoleKey.Q)
            {
                Console.Clear();
                world.NextDay();
                world.display();
            }
        }
    }
}
