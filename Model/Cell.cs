using System.Collections.Generic;
using System.Linq;

namespace TestWPFCore30.Model {
    // Conway's game of life rules
    // https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
    // Any cell with fewer than two neighbours die
    // Any cell with two or three neighbours live
    // Any cell with more than three neighbours die
    // Any empty cell (dead cell) with three live neighbours becomes alive (reproduction)
    public class Cell {
        private bool willBeAlive;

        public Cell (bool isAlive) {
            IsAlive = isAlive;
        }

        public bool IsAlive { get; private set; }

        // Takes an enumerable of 8 or fewer cells that represent the neighbours
        // The array itself is not nullable
        public void ShouldBeAlive (IEnumerable<Cell> neighbours) {
            int livingNeighbours = neighbours.Sum (neighbour => neighbour.IsAlive ? 1 : 0);
            // Using C# 8's new switch expression
            willBeAlive = livingNeighbours switch {
                2 when IsAlive => true,
                3 => true,
                _ => false
            };
        }

        public void ApplyNextState () {
            IsAlive = willBeAlive;
        }
    }
}
