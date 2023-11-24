using Minesweeper.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Helpers.Converters
{
    internal sealed class GameStateToBoolConverter : BaseConverter
    {
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is GameState gameState)
            {
                return gameState == GameState.InProcess;
            }
            return new ArgumentException();
        }

        public override object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
