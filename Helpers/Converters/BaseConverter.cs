﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Minesweeper.Helpers.Converters
{
    internal abstract class BaseConverter : MarkupExtension, IValueConverter
    {
        public abstract object? Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
