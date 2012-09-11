using System;
/*
 * Authors: CSTP, MROF, EENG
  */
namespace BDSA12{

    class Program{
	
	static bool IsPowerOf(int a, int b){
	    int c = a/b;
        if (a%b!= 0) {
          return false;
        }
        if (c == 1)
        {
            return true;
        }
        else
        {
            return IsPowerOf(c, b);
        }
	}
	
	static void Main(string[] args){
	    //MODIFY THIS SECTION TO USE args PARAMETERS
        if (args.Length < 2) {
            Console.Out.WriteLine("please enter two numbers");
            Console.ReadKey();
            return;
        }
        int a = int.Parse(args[0]), b = int.Parse(args[1]);
	    Console.Out.WriteLine(IsPowerOf(a,b));
        Console.ReadKey();
	}     
    }   
}
