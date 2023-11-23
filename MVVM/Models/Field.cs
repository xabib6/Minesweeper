using Minesweeper.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Minesweeper.MVVM.Models
{
    public class Field : BaseNotifier
    {      
        public ObservableCollection<ObservableCollection<Cell>> Cells { get; set; } = new();       
        
        public void GenerateField(int height, int width, int minesCount)
        {
            for (int i = 0; i < height; i++)
            {
                Cells.Add(new());
                for (int j = 0; j < width; j++)
                {
                    Cells[i].Add(new());
                    Cells[i][j].RealState = GetRandomState(height * width, minesCount);
                }
            }
        }

        private CellState GetRandomState(int cellsCount, int minesCount)
        {
            Random gen = new Random();
            int emptyProbability = gen.Next(cellsCount);
            return emptyProbability < minesCount ? CellState.Mined : CellState.Emtpy;
        }
    }
}
