using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Helpers
{
    public static class ExtensionsHelper
    {
        private static readonly Random _rand = new();
        public static (int, int) CoordinatesOf<T>(this ObservableCollection<ObservableCollection<T>> source, T value)
        {
            for (int i = 0; i < source.Count; i++)
            {
                for (int j = 0; j < source[i].Count; j++)
                {
                    if (source[i][j].Equals(value))
                    {
                        return (i, j);
                    }
                }
            }
            return (-1, -1);
        }

        public static T GetRandomElement<T>(this ObservableCollection<ObservableCollection<T>> source)
        {
            var row = _rand.Next(source.Count);
            var column = _rand.Next(source[row].Count);

            return source[row][column];
        }

        public static List<T> FindElementsAround<T>(this ObservableCollection<ObservableCollection<T>> source, T item)
        {
            var elementsAround = new List<T>();
            var coordinates = source.CoordinatesOf(item);
            var row = coordinates.Item1;
            var column = coordinates.Item2;
            for (int i = -1; i < 2; i++)
            {
                if (row + i < 0 || row + i == source.Count)
                {
                    continue;
                }
                for (int j = -1; j < 2; j++)
                {
                    if (column + j < 0 || column + j == source[i + row].Count || i == 0 && j == 0)
                    {
                        continue;
                    }
                    elementsAround.Add(source[i + row][j + column]);

                }
            }
            return elementsAround;
        }
    }
}
