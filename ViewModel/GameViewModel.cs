using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using TestWPFCore30.Model;

namespace TestWPFCore30.ViewModel {
    public class GameViewModel : INotifyPropertyChanged {
        private readonly GameGrid gameGrid;
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged (string propertyName) {
            PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
        }

        public GameViewModel (byte width, byte height) {
            gameGrid = new GameGrid (width, height);
            Display = CalculateDisplay ();
        }

        public string Display { get; private set; }

        public void Start () {
            DispatcherTimer timer = new DispatcherTimer ();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan (0, 0, 1); // 1 second
            timer.Start ();
        }

        private void Timer_Tick (object? _, EventArgs __) {
            UpdateDisplay ();
        }

        private void UpdateDisplay () {
            Display = CalculateDisplay ();
            NotifyPropertyChanged (nameof (Display));
        }

        private string CalculateDisplay () {
            return string.Join ("\r\n",
                gameGrid.
                    GetCells ().
                    Select (row => string.Join ("", row.
                        Select (cell => cell.IsAlive ? "##" : "  "))));
        }
    }
}
