using System.Windows;
using TestWPFCore30.ViewModel;

namespace TestWPFCore30 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public GameViewModel ViewModel { get; } = new GameViewModel ();

        public MainWindow () {
            DataContext = this;
            InitializeComponent ();
        }

        private void StartGame (object _, RoutedEventArgs __) {
            ViewModel.Start ();
        }

        private void StopGame (object _, RoutedEventArgs __) {
            ViewModel.Stop ();
        }
    }
}
