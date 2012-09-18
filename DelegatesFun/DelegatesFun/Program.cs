using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegatesFun
{
    class Program
    {
        static void Main(string[] args)
        {
            MyDelegate<int> del = Program.myMethod;
            Program.doSomething(del);
            MyDelegate<int> del2 = Program.myMethod2;
            Program.doSomething(del2);
        }
        
        public static int myMethod(int i)
        {
            return i * i;
        }

        public static int myMethod2(int i)
        {
            return i * 500000 / 33;
        }

        public static void doSomething(MyDelegate<int> del)
        {
            Console.Out.WriteLine("the delegate returns: "+ del(5));
        }


    }

    delegate int MyDelegate<T>(int o);
    


    
}
