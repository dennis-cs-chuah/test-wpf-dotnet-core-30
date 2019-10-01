using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TestWPFCore30.Model;

namespace Test {
    [TestFixture]
    public class TestGameGrid {
        [Test]
        public void TestGrid () {
            GameGrid grid = new GameGrid (10, 10, 100); // All alive!
            IEnumerable<Cell> cells = grid.GetCells ().SelectMany (row => row);
            Assert.AreEqual (100, cells.Count ());
            Assert.AreEqual (100, cells.Where (cell => cell.IsAlive).Count ());
            grid.NextIteration (); // Special case, only 4 corner cells will be left alive
            cells = grid.GetCells ().SelectMany (row => row);
            Assert.AreEqual (4, cells.Where (cell => cell.IsAlive).Count ());
            grid = new GameGrid (10, 10, 0); // All dead!
            cells = grid.GetCells ().SelectMany (row => row);
            Assert.AreEqual (100, cells.Count ());
            Assert.AreEqual (0, cells.Where (cell => cell.IsAlive).Count ());
        }
    }
}
