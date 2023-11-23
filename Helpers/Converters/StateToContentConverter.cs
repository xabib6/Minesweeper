using Minesweeper.MVVM.Models;
using System.Globalization;
using FontAwesome.Sharp;
using System.Windows.Media;
using System.Windows.Controls;

namespace Minesweeper.Helpers.Converters
{
    internal class StateToContentConverter : BaseMultiConverter
    {
        private Dictionary<int, SolidColorBrush> _minesCountColor = new()
        {
            { 1, Brushes.Blue },
            { 2, Brushes.Green },
            { 3, Brushes.Red },
            { 4, Brushes.Navy },
            { 5, Brushes.Maroon },
            { 6, Brushes.DarkSlateGray },
            { 7, Brushes.Black },
            { 8, Brushes.Peru }
        };

        public override object? Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[0] is bool isChecked && value[1] is CellState realState && value[2] is CellState guessedState && value[3] is int minesAround)
            {
                if (isChecked)
                {
                    if (guessedState == CellState.WrongGuess)
                    {
                        return new IconBlock() { Icon = IconChar.LandMineOn, Foreground = Brushes.Red };
                    }
                    return minesAround == 0 ? null :
                        new TextBlock()
                        {
                            Text = minesAround.ToString(),
                            Foreground = _minesCountColor[minesAround]
                        };
                }
                else
                {
                    return guessedState switch
                    {
                        CellState.Emtpy => new IconBlock(),
                        CellState.Mined => new IconBlock() { Icon = IconChar.Flag, Foreground = Brushes.Red },
                        CellState.CorrectGuess => new IconBlock { Icon = IconChar.Check, Foreground = Brushes.Green },
                        CellState.WrongGuess => new IconBlock { Icon = IconChar.Xmark, Foreground = Brushes.Red },
                        _ => throw new NotImplementedException()
                    };
                }
            }
            return new ArgumentException();
        }

        public override object?[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
