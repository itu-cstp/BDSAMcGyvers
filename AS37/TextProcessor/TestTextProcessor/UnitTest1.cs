﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextProcessor;
using TestTextProcessor;
using BDSA12;

namespace TestTextProcessor
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void TestGetInputString()
        {
            string input = "man";
            Assert.AreEqual(Program.getInputString(input), input);

            string input1 = "";
            Assert.AreEqual(Program.getInputString(input1), input1);

            string input2 = "MAN";
            Assert.AreEqual(Program.getInputString(input2), input2);

            string input3 = "123456";
            Assert.AreEqual(Program.getInputString(input3), input3);
        }

        [TestMethod]
        public void TestFindAllMatches()
        {
            string filename = "onlytext.txt";

            string s = TextFileReader.ReadFile(filename);

            RegMatch[] matches = Program.FindAllMatches("man", s);

            // This is man on the pos 5
            Assert.AreEqual(matches[1].pos, 5);
            Assert.AreEqual(matches.Length, 1);

            foreach (RegMatch match in matches)
            {
                Assert.AreNotEqual(match.color, ConsoleColor.Blue);
                Assert.AreNotEqual(match.color, ConsoleColor.Red);
            }

            string filename2 = "textWithoutLinks.txt";

            string s2 = TextFileReader.ReadFile(filename2);

            RegMatch[] matches2 = Program.FindAllMatches("", s2);

            // This is the first occuring link
            Assert.AreEqual(matches2[0].pos, 50);
            Assert.AreEqual(matches2.Length, 2);

            foreach (RegMatch match in matches2)
            {
                Assert.AreNotEqual(match.color, ConsoleColor.Yellow);
                Assert.AreNotEqual(match.color, ConsoleColor.Red);
            }


            string filename3 = "textWithoutDates.txt";

            string s3 = TextFileReader.ReadFile(filename3);

            RegMatch[] matches3 = Program.FindAllMatches("", s3);

            // This is the first occuring date
            Assert.AreEqual(matches3[0].pos, 50);
            Assert.AreEqual(matches3.Length, 2);

            foreach (RegMatch match in matches3)
            {
                Assert.AreNotEqual(match.color, ConsoleColor.Yellow);
                Assert.AreNotEqual(match.color, ConsoleColor.Blue);
            }

            string filename4 = "textWithoutDates.txt";

            string s4 = TextFileReader.ReadFile(filename4);

            RegMatch[] matches4 = Program.FindAllMatches("man", s4);


            Assert.AreEqual(matches4.Length, 10); // ish
        }

    }
}
