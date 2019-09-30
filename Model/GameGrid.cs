using System;
using System.Linq;

namespace TestWPFCore30.Model {
    public class GameGrid {
        private const int INITIAL_ALIVE_PERCENTAGE = 10;
        private readonly byte width;
        private readonly byte height;
        private readonly Cell[,] cells;

        public GameGrid (byte width, byte height) {
            this.width = width;
            this.height = height;
            cells = new Cell[width + 2, height + 2];

            // Create a ring of "dead" cells around the grid
            // Actual grid index is then 1-based
            foreach (int x in Enumerable.Range (0, width + 2)) {
                cells[x, 0] = new Cell (isAlive: false);
                cells[x, height + 1] = new Cell (isAlive: false);
            }
            foreach (int y in Enumerable.Range (1, height)) {
                cells[0, y] = new Cell (isAlive: false);
                cells[width + 1, y] = new Cell (isAlive: false);
            }

            // Randomise the cells
            Random random = new Random ();
            foreach (int x in Enumerable.Range (1, width))
                foreach (int y in Enumerable.Range (1, height))
                    cells[x, y] = new Cell (isAlive: random.Next (1, 100) <= INITIAL_ALIVE_PERCENTAGE);
        }

        public void NextIteration () {
            Cell[] neighbours = new Cell[8];
            foreach (int x in Enumerable.Range (1, width))
                foreach (int y in Enumerable.Range (1, height)) {
                    // Construct a list of up to 8 Cells that represent the neighbours
                    neighbours[0] = cells[x - 1, y - 1];
                    neighbours[1] = cells[x,     y - 1];
                    neighbours[2] = cells[x + 1, y - 1];
                    neighbours[3] = cells[x - 1, y];
                    neighbours[4] = cells[x + 1, y];
                    neighbours[5] = cells[x - 1, y + 1];
                    neighbours[6] = cells[x,     y + 1];
                    neighbours[7] = cells[x + 1, y + 1];
                    cells[x, y].ShouldBeAlive (neighbours);
                }
            // Note: Must be in separate iterations
            foreach (int x in Enumerable.Range (1, width))
                foreach (int y in Enumerable.Range (1, height))
                    cells[x, y].ApplyNextState ();
        }
    }
}
