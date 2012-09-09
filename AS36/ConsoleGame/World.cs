using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleGame 
{
    /// <summary>
    /// World simulates a world in a matrix. Each cell of the matrix represents a life, 
    /// which can be alive, dead or a zombie.
    /// </summary>
    public class World 
    {
        /// <summary>
        /// Size is the size of the matrix (length & width)
        /// </summary>
        public uint Size { get; private set; }

        /// <summary>
        /// This is the public matrix. It can't be modified from the outside, it's getter returns a 
        /// value from the internal representation, called Matrix.
        /// </summary>
        /// <param name="col">Number of columns</param>
        /// <param name="row">Number of rows</param>
        /// <returns>int? The state of the cell in that position</returns>
        public int? this[uint col, uint row] {
            get
            {
                return Matrix[col,row];
            }
        }

        /// <summary>
        /// The internal representation of the matrix, which can be updated by internal methods. 
        /// </summary>
        private int?[,] Matrix 
        {
            get;
            set;
        }

        /// <summary>
        /// Contructor for the game world, creates a matrix and populates it with a random mix of live,
        /// dead, or zombie cells.
        /// </summary>
        /// <param name="size">The size of the matrix, length and width</param>
        public World(uint size)
        {
            Size = size;
            this.Matrix = new int?[Size, Size];
            fillMatrix();
        }

        /// <summary>
        /// Populates the matrix with a random mix of live, dead, or zombie cells.
        /// </summary>
        public void fillMatrix() {
            Random rand = new Random();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++) {
                    int? num = rand.Next(3);
                    Matrix[i, j] = (num == 2 ? null : num);
                } 
            }
        }

        /// <summary>
        /// Updates the matrix to the next step, changing the states of cells according 
        /// to the specified rules.
        /// </summary>
        public void NextDay()
        {
            // updates each cell, saving the cell in an array, not changing the original matrix
            Cell[] cells = getMatrixAsArray();
            // resets the matrix with the new states
            ChangeMatrix(cells);
        }

        /// <summary>
        /// Retrieves the current matrix, and makes changes according to the game rules.
        /// </summary>
        /// <returns>The set of updates cells</returns>
        private Cell[] getMatrixAsArray()
        {
            Cell[] arrayOfCells = new Cell[Size * Size];
            int count = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    // get the state of the cell being updated
                    int? initialState = Matrix[i, j];
                    // get the states of the neighbors
                    int?[] neighborStates = (int?[])getNeighbors(i, j).ToArray();
                    //update cell state
                    int? state = changeCellState(initialState, neighborStates);
                    //collect coordinates and state into a Cell struct
                    int x = i;
                    int y = j;
                    int? endState = state;
                    Cell cell = new Cell(x, y, endState);

                    //add to array     
                    arrayOfCells[count] = cell;
                    count++;
                }
            }
            return arrayOfCells;
        }

        /// <summary>
        /// Returns a List of the states of a cells neighbors.
        /// Is not implemented as an array to allow for cells at the edges which have less than 
        /// 8 neighbors.
        /// </summary>
        /// <param name="a">int the x coordinate</param>
        /// <param name="b">int the y coordinate</param>
        /// <returns>List of the states of a cells neighbors</returns>
        public List<int?> getNeighbors(int a, int b)
        { 

            List<int?> returnList = new List<int?>();    
            
            // runs through the 8 neighbors
            for(int i = a - 1; i < a + 2; i++) {
                for (int j = b - 1; j < b + 2; j++)
                {
                    try {
                        if (i == a && j == b) { 
                            /*do nothing : so the cell doesn't count itself in its list of neighbors*/
                        }
                        else { 
                        returnList.Add(Matrix[i, j]); 
                        }
                    }
                    catch (IndexOutOfRangeException e) {
                        /*do nothing : because the position is outside the matrix*/
                    }
                }
            }
            return returnList;
        }

        /// <summary>
        /// Changes the state of a cell. This method contains the rules which determine whether a cell
        /// changes state.
        /// </summary>
        /// <param name="initialState">The initial state of the cell</param>
        /// <param name="neighbors">The states of the neighboring cells</param>
        /// <returns>The new state of the cell</returns>
        public int? changeCellState(int? initialState, params int?[] neighbors)
        {
            int? stateToReturn = initialState;  

            // if the cell is a zombie it should never change
            if (initialState == null) return null;
            
            // loop counts neighbors and checks if any are zombies
            int countOfNeighbors = 0;
            bool hasZombieNeighbor = false; 
            foreach(int? neigh in neighbors)
            {
                if (neigh == 1) countOfNeighbors++;
                if (neigh == null) hasZombieNeighbor = true;
            }

            // if the cell is alive
            if (initialState == 1) {
                // if there were any zombie neighbors, the cell has a 50% chance of becoming a zombie
                if (hasZombieNeighbor == true)
                {
                    Random rand = new Random();
                    int decision = rand.Next(2);
                    if (decision == 0) return null; 
                } 
                // if it has 2 or 3 neighbors it will survive, if not, it dies.
                if (countOfNeighbors == 2 || countOfNeighbors == 3)
                {
                    stateToReturn = 1;
                }
                else stateToReturn = 0;
            }
             
            // if a cell is dead, but has 3 live neighbors, it comes back alive
            if(initialState == 0) {
                if (countOfNeighbors == 3) stateToReturn = 1;
            }
            return stateToReturn;        
        }

        /// <summary>
        /// Updates the matrix with the values in cells
        /// </summary>
        /// <param name="cells">cells contains coordinates and the state of each</param>
        private void ChangeMatrix(params Cell[] cells)
        {
            int count = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Matrix[i, j] = cells[count++].State;
                }
            }
        }

        /// <summary>
        /// Displays the matrix in the console
        /// </summary>
        public void display()
        {
            int length = Matrix.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    int? val = Matrix[i,j];
                    if (val == null) Console.Write("z ");
                    else Console.Write(val + " ");
                }
                Console.WriteLine();
            }
        }
    }     
        /// <summary>
        /// Cell is a struct to store a "life": a coordinate and its state; alive, dead, or zombie.
        /// </summary>
        internal struct Cell
        {
            internal int X;
            internal int Y;
            internal int? State;

            internal Cell(int x, int y, int? state)
            {
                X = x;
                Y = y;
                State = state;
            }
        }           
}
