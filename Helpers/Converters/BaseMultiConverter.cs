using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Minesweeper.Helpers.Converters
{
    abstract class BaseMultiConverter : MarkupExtension, IMultiValueConverter
    {
        public abstract object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

        public abstract object?[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);
    }
}
