using System.Linq;
using NUnit.Framework;
using TestWPFCore30.Model;

namespace Test {
    [TestFixture]
    public class Tests {
        private static readonly object[] TEST_CASES = new object[] {
            // Living cell, 2, or 3 neighbours -> continue to be alive
            new object[] { true, 0, false },
            new object[] { true, 1, false },
            new object[] { true, 2, true },
            new object[] { true, 3, true },
            new object[] { true, 4, false },
            new object[] { true, 5, false },
            new object[] { true, 6, false },
            new object[] { true, 7, false },

            // Dead cell, if exactly 3 neighbours -> will become alive (reproduction)
            new object[] { false, 0, false },
            new object[] { false, 1, false },
            new object[] { false, 2, false },
            new object[] { false, 3, true },
            new object[] { false, 4, false },
            new object[] { false, 5, false },
            new object[] { false, 6, false },
            new object[] { false, 7, false }
        };

        [TestCaseSource (nameof (TEST_CASES))]
        public void TestNextState (bool isAlive, int livingNeighbours, bool expectedToBeAlive) {
            Cell testCell = new Cell (isAlive: isAlive);
            testCell.ShouldBeAlive (Enumerable.
                Range (0, livingNeighbours).

                Select (_ => new Cell (isAlive: true)));
            testCell.ApplyNextState ();
            Assert.AreEqual (expectedToBeAlive, testCell.IsAlive);
        }
    }
}
