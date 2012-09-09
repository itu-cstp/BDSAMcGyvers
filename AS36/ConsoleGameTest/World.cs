using System;
using NUnit.Framework;
using ConsoleGame;

namespace ConsoleGame
{

	[TestFixture]
	class WorldTest
	{
		private World world;
		
		[SetUp]
		public void Init(){
			world = new World(3);
		}
		
		[Test]
		/// <summary>
		/// Tests that the size is properly set by the constructor
		/// </summary>
		public void testSize(){
			// Check that the size property exists
			Assert.AreEqual(world.Size, 3);
		}
		
		[Test]
		/// <summary>
		/// Tests ChangeCellState method.
		/// </summary>
		public void TestChangeCellState()
        {   /**
             * TESTS BEING RUN/EXPECTANCY TABLE:
                // zombie from the start
                input: (null, zombies[]) expected output: null

                //surviving with two or three friends
                input (1, 2live no zombies[]) expected output: 1
                input (1, 3live no zombies[]) expected output: 1

                // dead with 4-8 friends
                input (1, 4live no zombies[]) expected output: 0
                input (1, 5live no zombies[]) expected output: 0
                input (1, 6live no zombies[]) expected output: 0
                input (1, 7live no zombies[]) expected output: 0
                input (1, 8live no zombies[]) expected output: 0

                // dead from the beginning
                input (0, zombies[]) expected output: 0

                //raised from the dead
                input (0, 3live[]) expected output: 1

                // 50 percent chance for turning "ghouly"
                input (1, zombies[]) expected output from 1000000 rounds = close to 50%
                This test might fail because its impossible to guarantee a firm result
                when using a random number generator. Adding more rounds should eliminate
                large fluctuations but you can't be sure.
                
             * **/

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
		
		[Test]
		/// <summary>
		/// Tests the getNeighbors method.
		/// </summary>
		public void TestGetNeighbors()
        {
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
