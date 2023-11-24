using Minesweeper.Helpers;
using System.Windows.Input;

namespace Minesweeper.MVVM.Models
{
    public sealed class Cell : BaseNotifier
    {
        public CellState GuessedState { get => guessedState; set => SetField(ref guessedState, value); }
        private CellState guessedState;

        public CellState RealState { get => realState; set => SetField(ref realState, value); }
        private CellState realState;

        public bool IsChecked { get => isChecked; set => SetField(ref isChecked, value); }
        private bool isChecked;
        public int MinesAround { get => minesAround; set => SetField(ref minesAround, value); }
        private int minesAround;

        public event Action<Cell>? CellChecked;
        public event Action<Cell>? CellQuickChecked;
        public event Action<Cell>? GuessChanged;

        public ICommand Check
        {
            get
            {
                return check ??= new RelayCommand(obj =>
                {
                    CellChecked?.Invoke(this);
                }, 
                obj => !IsChecked && GuessedState == CellState.Clear);
            }
        }
        private ICommand? check;

        public ICommand QuickCheck
        {
            get
            {
                return quickCheck ??= new RelayCommand(obj =>
                {
                    CellQuickChecked?.Invoke(this);
                },
                obj => IsChecked);
            }
        }
        private ICommand? quickCheck;

        public ICommand ChangeGuessedState
        {
            get
            {
                return changeGuessedState ??= new RelayCommand(obj => 
                {
                    GuessChanged?.Invoke(this); 
                },
                obj => !IsChecked);
            }
        }
        private ICommand? changeGuessedState;

        public void CompareGuessedReal()
        {
            if (GuessedState == CellState.Mined)
            {
                GuessedState = GuessedState == RealState ? CellState.CorrectGuess : CellState.WrongGuess;
            }
        }
    }
}
