using System;

namespace TestWPFCore30.ViewModel {
    public class GameOptionViewModel {
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

        public GameViewModel Start () {
            GameViewModel game = new GameViewModel (width, height);
            game.Start ();
            return game;
        }
    }
}
