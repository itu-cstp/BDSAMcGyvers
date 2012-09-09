using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit;
using NUnit.Framework;
using NUnit.Core;
using ConsoleGame;

namespace ConsoleGameTest
{
    [TestClass]
    public class ConsoleGameTest
    {
        [TestMethod]
        public void TestChangeCellState()
        {
            // making a new world size 3x3
            World world = new World(3);
            // zombie stays zombie
            Assert.AreEqual(world.changeCellState(null, new int?[] { null, null, null, null, null, null, null, null, null }), null);
            // 2-3 live neighbors
            Assert.AreEqual(world.changeCellState(1, new int?[] { 1, 1, 0, null, 0, null, 0, 0}), 1);
            Assert.AreEqual(world.changeCellState(1, new int?[] { 1, 1, 1, 0, 0, null, null, 0 }), 1);
            // 4-8 live no zombies
            Assert.AreEqual(world.changeCellState(1, new int?[] { 1, 1, 1, 1, 0, 0, 0, 0 }), 0);
            Assert.AreEqual(world.changeCellState(1, new int?[] { 1, 1, 1, 1, 1, 0, 0, 0 }), 0);
            Assert.AreEqual(world.changeCellState(1, new int?[] { 1, 1, 1, 1, 1, 1, 0, 0 }), 0);
            Assert.AreEqual(world.changeCellState(1, new int?[] { 1, 1, 1, 1, 1, 1, 1, 0 }), 0);
            Assert.AreEqual(world.changeCellState(1, new int?[] { 1, 1, 1, 1, 1, 1, 1, 1 }), 0);
            // "the dead remains the same"
            Assert.AreEqual(world.changeCellState(0, new int?[] { 1, 1, 1, 1, 1, 1, 1, 1 }), 0);
            // come to live with three friends
            Assert.AreEqual(world.changeCellState(0, new int?[] { 1, 1, 1, 0, 0, 0, 0, 0}), 1);

            //the turn to zombie 50 percent approximation.
            int zombiefied = 0;
            for (int i = 0; i < 1000000; i++)
            {

                if (world.changeCellState(1, new int?[]{null, null, null}) == null)
                    zombiefied++;
            }
            Console.Out.WriteLine("Zombies: " + zombiefied);
            Assert.IsTrue(zombiefied < 550000 && zombiefied > 450000); //could be put closer to 50%
            
        }

        [TestMethod]
        public void TestGetNeighbors()
        {
            ConsoleGame.World world = new ConsoleGame.World(3);
            /*
             * [0,0] [0,1] [0,2]
             * [1,0] [1,1] [1,2]
             * [2,0] [2,1] [2,2] 
             */

            // [0,0] should have a neighbourlist of length 3
            Assert.AreEqual(3, world.getNeighbors(0, 0).Count);

            // [0,1] should have a neighbourlist of length 5
            Assert.AreEqual(5, world.getNeighbors(0, 1).Count);

            // [0,2] should have a neighbourlist of length 3
            Assert.AreEqual(3, world.getNeighbors(0, 2).Count);

            // [1,0] should have a neighbourlist of length 5
            Assert.AreEqual(5, world.getNeighbors(1, 0).Count);

            // [1,1] should have a neighbourlist of length 8
            Assert.AreEqual(8, world.getNeighbors(1, 1).Count);

            // [1,2] should have a neighbourlist of length 5
            Assert.AreEqual(5, world.getNeighbors(1, 2).Count);

            // [2,0] should have a neighbourlist of length 3
            Assert.AreEqual(3, world.getNeighbors(2, 0).Count);

            // [2,1] should have a neighbourlist of length 5
            Assert.AreEqual(5, world.getNeighbors(2, 1).Count);

            // [2,2] should have a neighbourlist of length 3
            Assert.AreEqual(3, world.getNeighbors(2, 2).Count);
        }
    }
}