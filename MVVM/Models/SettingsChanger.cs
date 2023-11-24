using Minesweeper.Helpers;
using System.Windows.Input;

namespace Minesweeper.MVVM.Models
{
    public sealed class SettingsChanger : BaseNotifier
    {
        public int Height { get => height; set => SetField(ref height, value); }
        private int height;
        public int Width { get => width; set => SetField(ref width, value); }
        private int width;
        public int AllMinesCount { get => allMinesCount; set => SetField(ref allMinesCount, value); }
        private int allMinesCount;
        public Difficulty Difficulty
        {
            get => difficulty; set
            {
                SetField(ref difficulty, value);
            }
        }
        private Difficulty difficulty = Difficulty.Low;

        private GameController _gameController;

        public SettingsChanger(GameController gameController)
        {
            _gameController = gameController;
            SetDifficulty();
        }

        private void SetDifficulty()
        {
            (Height, Width, AllMinesCount) = Difficulty switch
            {
                Difficulty.Low => (10, 10, 10),
                Difficulty.Medium => (20, 20, 40),
                Difficulty.High => (30, 30, 100),
                Difficulty.Custom => (Height, Width, AllMinesCount),
                _ => throw new NotImplementedException()
            };
        }

        public ICommand ChangeDifficulty
        {
            get
            {
                return changeDifficulty ??= new RelayCommand(obj =>
                {
                    Difficulty = obj.ToString() switch
                    {
                        "1" => Difficulty.Low,
                        "2" => Difficulty.Medium,
                        "3" => Difficulty.High,
                        "4" => Difficulty.Custom,
                    };
                });
            }
        }
        private ICommand changeDifficulty;

        public ICommand ApplyDifficulty
        {
            get
            {
                return applyDifficulty ??= new RelayCommand(obj =>
                {
                    SetDifficulty();
                    _gameController.RestartGame();
                });
            }
        }
        private ICommand? applyDifficulty;
    }
}
