using Minesweeper.Helpers;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Minesweeper.MVVM.Models
{
    public sealed class GameController : BaseNotifier
    {
        public int RestMinesCount { get => restMinesCount; set => SetField(ref restMinesCount, value); }
        private int restMinesCount;

        public int TimeElapsed { get => timeElapsed; set => SetField(ref timeElapsed, value); }
        private int timeElapsed;

        public GameState GameState { get => gameState; set => SetField(ref gameState, value); }
        private GameState gameState;

        public SettingsChanger SettingsChanger { get; set; }
        public ObservableCollection<ObservableCollection<Cell>> Cells { get; set; } = new();
        private IEnumerable<Cell> _allCells => Cells.SelectMany(c => c);
        private Stopwatch _stopwatch = Stopwatch.StartNew();

        public GameController()
        {
            SettingsChanger = new(this);
            RestartGame();
            StartTimer();
        }

        public void RestartGame()
        {
            GameState = GameState.InProcess;
            GenerateField();
            _stopwatch.Restart();
        }

        private void GenerateField()
        {
            ClearField();
            var height = SettingsChanger.Height;
            var width = SettingsChanger.Width;
            var minesCount = SettingsChanger.AllMinesCount;
            for (int i = 0; i < height; i++)
            {
                Cells.Add(new());
                for (int j = 0; j < width; j++)
                {
                    var newCell = new Cell();
                    Cells[i].Add(newCell);
                    SubsribeCell(newCell);
                }
            }

            while (RestMinesCount < minesCount)
            {
                var randomCell = Cells.GetRandomElement();
                TrySetMine(randomCell, height * width, minesCount);
            }
        }

        private void ClearField()
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                for (int j = 0; j < Cells[i].Count; j++)
                {
                    UnsubscribeCell(Cells[i][j]);
                }
            }
            Cells.Clear();
            RestMinesCount = 0;
        }

        private void CellChecked(Cell cell)
        {
            cell.IsChecked = true;
            CheckDefeat(cell);
            cell.MinesAround = CalculateMinesAround(cell);
            if (cell.MinesAround == 0)
            {
                CellQuickChecked(cell);
            }
            CheckWin();
        }        

        private void CellQuickChecked(Cell cell)
        {
            var cellsAround = Cells.FindElementsAround(cell);
            var uncheckedCellsAround = cellsAround.Where(c => !c.IsChecked && c.GuessedState == CellState.Clear).ToList();
            var guessedMinesAround = cellsAround.Where(c => c.GuessedState == CellState.Mined).Count();
            if (guessedMinesAround == cell.MinesAround)
            {
                foreach (var cellAround in uncheckedCellsAround)
                {
                    cellAround.MinesAround = CalculateMinesAround(cellAround);
                    CellChecked(cellAround);
                }
            }

        }

        private void GuessChanged(Cell cell)
        {
            if (cell.GuessedState == CellState.Clear)
            {
                cell.GuessedState = CellState.Mined;
                RestMinesCount--;
            }
            else if (cell.GuessedState == CellState.Mined)
            {
                cell.GuessedState = CellState.Clear;
                RestMinesCount++;
            }
        }

        private void CheckDefeat(Cell cell)
        {
            if (cell.RealState == CellState.Mined)
            {
                cell.GuessedState = CellState.WrongGuess;
                ShowRestMines();
                ShowMarkedCells();
                GameState = GameState.Defeat;
            }
        }

        private void CheckWin()
        {
            if (_allCells.Where(c => !c.IsChecked).Count() == SettingsChanger.AllMinesCount)
            {
                ShowRestMines();
                ShowMarkedCells();
                GameState = GameState.Victory;
            }
        }

        private int CalculateMinesAround(Cell cell)
        {
            return Cells.FindElementsAround(cell).Where(c => c.RealState == CellState.Mined).Count();
        }

        private void TrySetMine(Cell cell, int cellsCount, int minesCount)
        {
            if (cell.RealState == CellState.Mined)
            {
                return;
            }
            cell.RealState = CellState.Mined;
            RestMinesCount++;
        }

        private async void StartTimer()
        {
            await Task.Run(() =>
            {                
                while (true)
                {
                    while (GameState == GameState.InProcess)
                    {
                        TimeElapsed = _stopwatch.Elapsed.Seconds + _stopwatch.Elapsed.Minutes*60;
                    }
                }
            });
        }

        private void ShowRestMines()
        {
            var minedCells = _allCells.Where(c => c.RealState == CellState.Mined).ToList();
            foreach (var cell in minedCells)
            {
                cell.IsChecked = true;
                cell.CompareGuessedReal();
            }
        }

        private void ShowMarkedCells()
        {
            var guessedCells = _allCells.Where(c => c.GuessedState == CellState.Mined).ToList();

            foreach (var cell in guessedCells)
            {
                cell.IsChecked = true;
                if (cell.RealState == CellState.Mined)
                {
                    cell.GuessedState = CellState.CorrectGuess;
                }
                else
                {
                    cell.GuessedState = CellState.WrongGuess;
                }
            }
        }

        private void UnsubscribeCell(Cell cell)
        {
            cell.CellChecked -= CellChecked;
            cell.CellQuickChecked -= CellQuickChecked;
            cell.GuessChanged -= GuessChanged;
        }
        private void SubsribeCell(Cell cell)
        {
            cell.CellChecked += CellChecked;
            cell.CellQuickChecked += CellQuickChecked;
            cell.GuessChanged += GuessChanged;
        }

        public ICommand Restart
        {
            get
            {
                return restart ??= new RelayCommand(obj =>
                {
                    RestartGame();
                });
            }
        }
        private ICommand restart;
    }
}
