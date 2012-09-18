using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BDSA12;


namespace TextProcessor
{
	/// <summary>
	/// Struct used to represent a "hit" / search string in the text file 
	/// </summary>
    public struct RegMatch : IComparable<RegMatch>
    {
		/// <summary>
		/// The position of the hit
		/// </summary>
        public int pos;
		
		/// <summary>
		/// The length of the search match. Eg. word length
		/// </summary>
        public int length;
		
		/// <summary>
		/// The color to change the console background to when indicating the hit
		/// </summary>
        public ConsoleColor color;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="TextProcessor.RegMatch"/> struct.
		/// </summary>
        public RegMatch(int pos, int length, ConsoleColor color) :this()
        {
            this.pos = pos;
            this.length = length;
            this.color = color;
        }
		
		/// <summary>
		/// Compares two structs for ordering
		/// This is useful because we need the matches sequentially to display them properly and because
		/// matches have different characteristics (input, url, date)
		/// </summary>
		/// <returns>
		/// negative, zero or greather than zero
		/// </returns>
		/// <param name='other'>
		/// The other struct to compare against
		/// </param>
        public int CompareTo(RegMatch other)
        {
            return pos - other.pos;
        }
    }

	/// <summary>
	/// The main entry point for our text processing application
	/// </summary>
    public class Program
    {
		/// <summary>
		/// Read Text input from user and formats it as a correct search string 
		/// We have some special considerations. Eg "+" means more keywords
		/// </summary>
		/// <returns>
		/// Search string
		/// </returns>
		public static string getInputString(string line){
            
			// The assignment specifies that we should handle +. 
			// The functionality is obtained by using space as well though
			String[] lineArr = line.Split('+');
			
			// What to match for. Can be more keywords
            String matchLine = "";
            if (lineArr.Length > 1)
            {
                foreach (String str in lineArr)
                    matchLine += str.Trim() + " ";
            }
			// If only one keyword
            else matchLine = line;
            
			// Remove the last space
			matchLine = matchLine.Trim();
			
			// "*" means anything / wildcard
            matchLine = matchLine.Replace("*", @"\S+");
            return matchLine;
		}
		/// <summary>
		/// Finds all matches in the line and returns them in an sorted array with the appropiate
	    /// indexes and formatting specified
		/// </summary>
		/// <returns>
		/// All occurences of matches in the text string 
		/// </returns>
		/// <param name='matchLine'>
		/// Speicific search string to match for
		/// </param>
		/// <param name='textToSearchIn'>
		/// Text to search in.
		/// </param>
		public static RegMatch[] FindAllMatches(string matchLine, string textToSearchIn) {
			
			// Matches our input string
			MatchCollection matches = Regex.Matches(textToSearchIn, matchLine, RegexOptions.IgnoreCase);
            
			// Matches urls
			MatchCollection urls = Regex.Matches(textToSearchIn, @"http\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(/\S*)"); 
            
			// Matches dates in the format specified in the file (narrow)
			MatchCollection dates = Regex.Matches(textToSearchIn, @"[A-Z]{1}[a-z]{2}, [1-9]{2} [A-Z]{1}[a-z]{2} \d{4} [0-9]{2}:[0-9]{2}:[0-9]{2}");
            
			// Collect matches for everything
			RegMatch[] allMatches = new RegMatch[matches.Count + urls.Count + dates.Count];
    		
			/*
			 * Go through the 3 differnet matches and save the structs appropriately
			 */
			// Count how far along we are in our matches
			
			int allMatchIndex = 0;
            foreach (Match match in matches)
                allMatches[allMatchIndex++] = new RegMatch(match.Index, match.Length, ConsoleColor.Yellow);
            foreach (Match match in urls)
                allMatches[allMatchIndex++] = new RegMatch(match.Index, match.Length, ConsoleColor.Blue);
            foreach (Match match in dates)
                allMatches[allMatchIndex++] = new RegMatch(match.Index, match.Length, ConsoleColor.Red);
                        
			// Sort it using our custom function to ensure that they are formatted properly
			Array.Sort(allMatches);
			
			return allMatches;
		}
		
		/// <summary>
		/// Prints the all the matches to the console
		/// </summary>
		/// <param name='allMatches'>
		/// All matches.
		/// </param>
		public static void printOutMatches(RegMatch[] allMatches, string s){

			int index = 0;
	        // Write out the matches with correct formatting
			foreach (RegMatch match in allMatches)
	        {
	            Console.Out.Write(s.Substring(index, match.pos - index));
	            Console.BackgroundColor = match.color;
	            Console.Out.Write(s.Substring(match.pos, match.length));
	            Console.ResetColor();
				
				// Keep track of our next position
	            index = match.pos + match.length;
	        }
			
	        Console.Out.Write(s.Substring(index, s.Length - index));
	        Console.ReadKey();
		}
		
        static void Main(string[] args)
        {
			Console.Out.Write("Please enter search string: ");
			
			// Wait for input
			String line = Console.In.ReadLine();
            if (line.Equals(""))
            {
                Console.Out.WriteLine("Please enter a search string...");
                Console.ReadKey();
                return;
            }
			
			// File to match in

			string filename = "testFile.txt";

			
			// Read the file and place it in string
			string s = TextFileReader.ReadFile(filename);
			
			// Get a search term (or terms)
			string matchLine = getInputString(line);
			
			// Collect all matches in the string
			RegMatch[] allMatches = FindAllMatches(matchLine, s);
			
			printOutMatches(allMatches, s);
        }
    }
}
