﻿using Minesweeper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Minesweeper.MVVM.Models
{
    public sealed class GameController : BaseNotifier
    {
        public int Height { get => height; set => SetField(ref height, value); }
        private int height;
        public int Width { get => width; set => SetField(ref width, value); }
        private int width;
        public int AllMinesCount { get => allMinesCount; set => SetField(ref allMinesCount, value); }
        private int allMinesCount;
        public int SupposedMinesCount { get => supposedMinesCount; set => SetField(ref supposedMinesCount, value); }
        private int supposedMinesCount;

        public Field Field { get; set; } = new();

        public Difficulty Difficulty
        {
            get => difficulty; set
            {
                SetField(ref difficulty, value);                
            }
        }
        private Difficulty difficulty = Difficulty.Low;

        public ICommand ApplyDifficulty
        {
            get
            {
                return applyDifficulty ??= new RelayCommand(obj =>
                {
                    SetMapDifficulty();
                    Generate();
                });
            }
        }
        private ICommand? applyDifficulty;

        private void SetMapDifficulty()
        {
            (Height, Width, AllMinesCount) = Difficulty switch
            {
                Difficulty.Low => (10, 10, 100),
                Difficulty.Medium => (20, 20, 40),
                Difficulty.High => (30, 30, 100),
                Difficulty.Custom => (Height, Width, allMinesCount),
                _ => throw new NotImplementedException()
            };
        }

        
        public ICommand GenerateField
        {
            get
            {
                return generateField ??= new RelayCommand(obj =>
                {
                    Generate();
                });
            }
        }
        private ICommand? generateField;

        public void Generate()
        {
            Field.GenerateField(Height, Width, AllMinesCount);
        }
    }
}
