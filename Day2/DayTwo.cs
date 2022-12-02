using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_code_2022.Day2 {
    public class DayTwo : DaySolver {
        private static readonly string textFile = @"C:\Workspace\C#\Advent-of-code-2022\Advent-of-code-2022\Day2\data.txt";

        // A = ROCK
        // B = PAPER
        // C = SCISSOR


        // X = ROCK = 1 = LOSE
        // Y = PAPER = 2 = DRAW
        // Z = SCISSOR = 3 = WIN

        // WIN = 6
        // DRAW = 3
        // LOST = 0

        // WIN CALCULATION = SHAPE + RESULT
        public static void Solve() {
            if (!File.Exists(textFile)) throw new Exception("Data file not found");

            string[] lines = File.ReadAllLines(textFile);

            Dictionary<Shape, Shape> winShapes = new Dictionary<Shape, Shape>() {
                {Shape.ROCK, Shape.SCISSORS},
                {Shape.SCISSORS, Shape.PAPER},
                {Shape.PAPER, Shape.ROCK},
            };

            int total = 0;

            foreach (var line in lines) {
                string value = line.Replace(" ", string.Empty);
                Shape elfShape = ConvertToShape(value[0]);
                Shape playerShape;

                if (value[1] == 'X') { // LOSE
                    playerShape = winShapes[elfShape];
                } else if (value[1] == 'Y') { // DRAW
                    playerShape = elfShape;
                } else { // WIN
                    playerShape = winShapes.FirstOrDefault(x => x.Value == elfShape).Key;
                }

                int result = FirstAnalyze(elfShape, playerShape);

                total += (result + ((int)playerShape + 1));
            }

            Console.Write($"Total : {total}");
        }

        static int FirstAnalyze(Shape elfShape, Shape playerShape) {
            int result;

            if ((playerShape == Shape.ROCK && elfShape == Shape.SCISSORS) ||
                (playerShape == Shape.SCISSORS && elfShape == Shape.PAPER) ||
                (playerShape == Shape.PAPER && elfShape == Shape.ROCK)) {
                result = 6;
            } else if (playerShape == elfShape) {
                result = 3;
            } else {
                result = 0;
            }

            return result;
        }

        static Shape ConvertToShape(char letter) {
            switch (letter) {
                case 'X':
                case 'A':
                    return Shape.ROCK;
                
                case 'Y':
                case 'B':
                    return Shape.PAPER;
                
                default:
                    return Shape.SCISSORS;
            }
        }

        public enum Shape {
            ROCK,
            PAPER,
            SCISSORS
        }
    }
}