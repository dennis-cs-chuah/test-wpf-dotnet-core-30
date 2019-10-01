using System;
using System.ComponentModel;
using NUnit.Framework;
using TestWPFCore30.ViewModel;

namespace Test {
    [TestFixture]
    public class TestGameViewModel {
        [Test]
        public void TestSize() {
            GameViewModel viewModel = new GameViewModel ();
            Assert.Throws<ArgumentException> (() => { viewModel.Width = 5; });
            Assert.Throws<ArgumentException> (() => { viewModel.Height = 9; });
            Assert.DoesNotThrow (() => { viewModel.Width = 15; });
            Assert.DoesNotThrow (() => { viewModel.Height = 11; });
        }

        [Test]
        public void TestStart () {
            GameViewModel viewModel = new GameViewModel {
                Width = 10,
                Height = 10,
                InitialAlivePercentage = 0 // All dead
            };
            viewModel.Start ();
            viewModel.Stop ();
            Assert.AreEqual (
                "                    \r\n" +
                "                    \r\n" +
                "                    \r\n" +
                "                    \r\n" +
                "                    \r\n" +
                "                    \r\n" +
                "                    \r\n" +
                "                    \r\n" +
                "                    \r\n" +
                "                    ",
                viewModel.Display);
            viewModel = new GameViewModel {
                Width = 10,
                Height = 10,
                InitialAlivePercentage = 100 // All alive
            };
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            firstTime = true;
            viewModel.Start ();
        }

        private bool firstTime;

        private void ViewModel_PropertyChanged (object sender, PropertyChangedEventArgs e) {
            if (sender is GameViewModel viewModel && e.PropertyName?.Equals (nameof (viewModel.Display), StringComparison.InvariantCulture) == true) {
                if (firstTime) {
                    firstTime = false;
                    Assert.AreEqual (
                        "####################\r\n" +
                        "####################\r\n" +
                        "####################\r\n" +
                        "####################\r\n" +
                        "####################\r\n" +
                        "####################\r\n" +
                        "####################\r\n" +
                        "####################\r\n" +
                        "####################\r\n" +
                        "####################",
                        viewModel.Display);
                } else {
                    viewModel.Stop ();
                    Assert.AreEqual (
                        "##                ##\r\n" +
                        "                    \r\n" +
                        "                    \r\n" +
                        "                    \r\n" +
                        "                    \r\n" +
                        "                    \r\n" +
                        "                    \r\n" +
                        "                    \r\n" +
                        "                    \r\n" +
                        "##                ##",
                        viewModel.Display);
                }
            }
        }
    }
}
