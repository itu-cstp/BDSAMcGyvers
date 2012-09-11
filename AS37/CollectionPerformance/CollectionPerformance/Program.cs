using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CollectionPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestListInsert();
            //TestDictionaryInsert();
            //TestSortedDictionaryInsert();
            //TestListLookup();
            //TestDictionaryLookup();
            //TestSortedDictionaryLookup();
            TestSortedListLookup();
        }


        static void TestListInsert()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            List<int> list = new List<int>();
            for (int i = 0; i < 10000000000; i++) 
            {
                try
                {
                    list.Add(i);
                    if (i % 20000000 == 0)
                        Console.Out.WriteLine("index: " + i + " Time passed:\t" + watch.Elapsed);
                }
                catch (OutOfMemoryException e) { Console.Out.WriteLine("Out of memory on index: " + i); break; }
            }
            Console.ReadKey();
        }

        static void TestSortedDictionaryInsert()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            SortedDictionary<int, int> list = new SortedDictionary<int, int>();
            for (int i = 0; i < 10000000000; i++)
            {
                try
                {
                    list.Add(i,i);
                    if (i % 2000000 == 0)
                        Console.Out.WriteLine("index: " + i + " Time passed:\t" + watch.Elapsed);
                }
                catch (OutOfMemoryException e) { Console.Out.WriteLine("Out of memory on index: " + i); break; }
            }
            Console.ReadKey();
        }

        static void TestDictionaryInsert()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Dictionary<int, int> list = new Dictionary<int, int>();
            for (int i = 0; i < 10000000000; i++)
            {
                try
                {
                    list.Add(i, i);
                    if (i % 4000000 == 0)
                        Console.Out.WriteLine("index: " + i + " Time passed:\t" + watch.Elapsed);
                }
                catch (OutOfMemoryException e) { Console.Out.WriteLine("Out of memory on index: " + i); break; }
            }
            Console.ReadKey();
        }

        static void TestListLookup()
        {
            int size = 10000000;
            
            List<int> list = new List<int>();
            
            for (int i = 0; i < size; i++)
            {
                try{
                    list.Add(i);
                } catch(OutOfMemoryException e) { Console.Out.WriteLine("Out of memory at index: " + i); break; }
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < size; i++)
            {
                list.Contains(i);
                if (i % 5000 == 0) Console.WriteLine("At index " + i + " time spent: " + watch.Elapsed);
            }
            Console.ReadKey();
        }

        static void TestDictionaryLookup()
        {
            int size = 1000000000;

            Dictionary<int,int> list = new Dictionary<int,int>();

            for (int i = 0; i < size; i++)
            {
                try
                {
                    list.Add(i,i);
                }
                catch (OutOfMemoryException e) { Console.Out.WriteLine("Out of memory at index: " + i); break; }
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < size; i++)
            {
                list.ContainsKey(i);
                if (i % 50000000 == 0) Console.WriteLine("At index " + i + " time spent: " + watch.Elapsed);
            }
            Console.ReadKey();
        }

        static void TestSortedDictionaryLookup()
        {
            int size = 4000000;

            SortedDictionary<int, int> list = new SortedDictionary<int, int>();

            for (int i = 0; i < size; i++)
            {
                try
                {
                    if (i % 50000 == 0) Console.Out.WriteLine("Inserting " + i);
                    list.Add(i, i);
                }
                catch (OutOfMemoryException e) { Console.Out.WriteLine("Out of memory at index: " + i); break; }
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < size; i++)
            {
                list.ContainsKey(i);
                if (i % 200000 == 0) Console.WriteLine("At index " + i + " time spent: " + watch.Elapsed);
            }
            Console.ReadKey();
        }

        static void TestSortedListLookup()
        {
            int size = 10000000;

            List<int> list = new List<int>();

            for (int i = 0; i < size; i++)
            {
                try
                {
                    list.Add(i);
                }
                catch (OutOfMemoryException e) { Console.Out.WriteLine("Out of memory at index: " + i); break; }
            }

            list.Sort();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < size; i++)
            {
                list.BinarySearch(i);
                if (i % 500000 == 0) Console.WriteLine("At index " + i + " time spent: " + watch.Elapsed);
            }
            Console.ReadKey();
        }

    }

}
