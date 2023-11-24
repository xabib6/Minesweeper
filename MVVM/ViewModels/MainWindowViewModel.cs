using Minesweeper.MVVM.Models;

namespace Minesweeper.MVVM.ViewModels
{
    internal sealed class MainWindowViewModel : BaseViewModel
    {
        public GameController GameController { get; set; } = new();

        public MainWindowViewModel() 
        {

        }
    }
}
