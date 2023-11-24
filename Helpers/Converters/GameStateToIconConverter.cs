using FontAwesome.Sharp;
using Minesweeper.MVVM.Models;
using System.Globalization;

namespace Minesweeper.Helpers.Converters
{
    internal sealed class GameStateToIconConverter : BaseConverter
    {
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is GameState gameState)
            {
                return gameState switch
                {
                    GameState.InProcess => IconChar.Smile,
                    GameState.Victory => IconChar.Laugh,
                    GameState.Defeat => IconChar.Frown,
                    _ => IconChar.Meh
                };
            }
            return IconChar.Meh;
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
