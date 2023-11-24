using Minesweeper.MVVM.Models;
using System.Globalization;

namespace Minesweeper.Helpers.Converters
{
    internal sealed class DifficultyToBoolConverter : BaseConverter
    {
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Difficulty difficulty)
            {
                return difficulty == Difficulty.Custom;
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
