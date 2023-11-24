using Minesweeper.Helpers;
using Minesweeper.MVVM.Models;
using System.Windows.Input;

namespace Minesweeper.MVVM.ViewModels
{
    internal sealed class MainWindowViewModel : BaseViewModel
    {
        public GameController GameController { get; set; } = new();

        public bool ShowSettings { get => showSettings; set => SetField(ref showSettings, value); }
        private bool showSettings;

        public MainWindowViewModel() 
        {

        }

        public ICommand ShowHideSettings
        {
            get
            {
                return showHideSettings ??= new RelayCommand(obj =>
                {
                    ShowSettings = !ShowSettings;
                });
            }
        }
        private ICommand showHideSettings;
    }
}
