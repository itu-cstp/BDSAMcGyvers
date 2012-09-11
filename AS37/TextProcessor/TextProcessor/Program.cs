using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BDSA12;
using System.Text.RegularExpressions;

namespace TextProcessor
{
    struct RegMatch : IComparable<RegMatch>
    {
        public int pos;
        public int length;
        public ConsoleColor color;
        public RegMatch(int pos, int length, ConsoleColor color) :this()
        {
            this.pos = pos;
            this.length = length;
            this.color = color;
        }
        public int CompareTo(RegMatch other)
        {
            return pos - other.pos;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            String s = TextFileReader.ReadFile("c:/c#/excersize 2/testFile.txt");
            Console.Out.Write("Please enter search string: ");
            String line = Console.In.ReadLine();
            String[] lineArr = line.Split('+');
            String matchLine = "";
            if (lineArr.Length > 1)
            {
                foreach (String str in lineArr)
                    matchLine += str.Trim() + " ";
            }
            else matchLine = line;
            matchLine = matchLine.Trim();
            matchLine = matchLine.Replace("*", @"\S+");
            MatchCollection matches = Regex.Matches(s, matchLine, RegexOptions.IgnoreCase);
            MatchCollection urls = Regex.Matches(s, @"http\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(/\S*)"); 
            MatchCollection dates = Regex.Matches(s, @"[A-Z]{1}[a-z]{2}, [1-9]{2} [A-Z]{1}[a-z]{2} \d{4} [0-9]{2}:[0-9]{2}:[0-9]{2}");
            RegMatch[] allMatches = new RegMatch[matches.Count + urls.Count + dates.Count];
            int allMatchIndex = 0;
            foreach (Match match in matches)
                allMatches[allMatchIndex++] = new RegMatch(match.Index, match.Length, ConsoleColor.Yellow);
            foreach (Match match in urls)
                allMatches[allMatchIndex++] = new RegMatch(match.Index, match.Length, ConsoleColor.Blue);
            foreach (Match match in dates)
                allMatches[allMatchIndex++] = new RegMatch(match.Index, match.Length, ConsoleColor.Red);
            Array.Sort(allMatches);
            int index = 0;
            foreach (RegMatch match in allMatches)
            {
                Console.Out.Write(s.Substring(index, match.pos - index));
                Console.BackgroundColor = match.color;
                Console.Out.Write(s.Substring(match.pos, match.length));
                Console.ResetColor();
                index = match.pos + match.length;
            }
            Console.Out.Write(s.Substring(index, s.Length - index));
            Console.ReadKey();
        }
    }
}
