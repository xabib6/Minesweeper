using System.Globalization;
using System.Windows;

namespace Minesweeper.Helpers.Converters
{
    internal class BoolToVisibilityConverter : BaseConverter
    {
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool visible)
            {
                if (parameter is not null)
                {
                    return visible ? Visibility.Hidden : Visibility.Visible;
                }
                return visible ? Visibility.Visible : Visibility.Hidden;
            }
            throw new ArgumentException();
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
