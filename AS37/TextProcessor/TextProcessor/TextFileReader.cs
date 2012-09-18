using System;
using System.IO;

namespace BDSA12
{
  /// <summary>
  ///   Utility class for reading text files into a single string.
  /// </summary>
  public static class TextFileReader
  {
    /// <summary>
    ///   Reads a text file and returns its content as a string.
    /// </summary>
    /// <param name="filename">The file name to be read, including the path.</param>
    /// <returns>A string representing the content of the file</returns>
    /// <example>
    /// <code><pre>
    /// public static void Main()
    /// {
    ///   string content = TextFileReader.ReadFile("TextFileReader.cs");
    ///   string[] lines = content.Split( new char[]{'\\n'});
    ///   foreach(string line in lines)
    ///	  {
    ///	    Console.Out.WriteLine(line);
    ///	  }
    /// }
    /// </pre></code>
    /// </example>
    public static string ReadFile(string filename)
    {
      try
        {
	  using (StreamReader sr = new StreamReader(filename))
            {  //This reads the entire file
	       return sr.ReadToEnd();
            }
        }
      catch (Exception e)
        {//Might happen if the file is not text or non existent
	  Console.WriteLine("The file could not be read:");
	  Console.WriteLine(e.Message);
	  throw e;
        } 
    }
  }
}