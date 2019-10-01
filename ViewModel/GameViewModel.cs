using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using TestWPFCore30.Model;

namespace TestWPFCore30.ViewModel {
    public class GameViewModel : INotifyPropertyChanged {
        private GameGrid? gameGrid;
        private DispatcherTimer? timer;
        public event PropertyChangedEventHandler? PropertyChanged;

        private byte width = 30;
        public byte Width {
            get => width;
            set {
                if (value < 10)
                    throw new ArgumentException ("Width cannot be less than 10");
                width = value;
            }
        }

        private byte height = 30;
        public byte Height {
            get => height;
            set {
                if (value < 10)
                    throw new ArgumentException ("Height cannot be less than 10");
                height = value;
            }
        }

        public bool IsStarted => timer != null;
        public bool IsStopped => timer is null;

        public int InitialAlivePercentage { get; set; } = GameGrid.DEFAULT_ALIVE_PERCENTAGE;

        private void NotifyPropertyChanged (string propertyName) {
            PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
        }

        public string Display { get; private set; } = "";

        public void Start (int intervalSeconds = 1) {
            gameGrid = new GameGrid (width, height, InitialAlivePercentage);
            UpdateDisplay ();
            timer = new DispatcherTimer ();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan (0, 0, intervalSeconds);
            timer.Start ();
            NotifyPropertyChanged (nameof (IsStarted));
            NotifyPropertyChanged (nameof (IsStopped));
        }

        public void Stop () {
            timer?.Stop ();
            timer = null;
            NotifyPropertyChanged (nameof (IsStarted));
            NotifyPropertyChanged (nameof (IsStopped));
        }

        private void Timer_Tick (object? _, EventArgs __) {
            gameGrid?.NextIteration ();
            UpdateDisplay ();
        }

        private void UpdateDisplay () {
            Display = CalculateDisplay ();
            NotifyPropertyChanged (nameof (Display));
        }

        private string CalculateDisplay () {
            return gameGrid != null ?
                string.Join ("\r\n",
                gameGrid.
                    GetCells ().
                    Select (row => string.Join ("", row.
                        Select (cell => cell.IsAlive ? "##" : "  ")))) :
                "";
        }
    }
}
