using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_code_2022.Day1 {
    internal class DayOne : DaySolver {
        private static readonly string textFile = @"C:\Workspace\C#\Advent-of-code-2022\Advent-of-code-2022\Day1\data.txt";

        public static void Solve() {
            if (!File.Exists(textFile)) throw new Exception("Data file not found");

            string[] lines = File.ReadAllLines(textFile);

            List<Elf> elves = new List<Elf>();

            int calories = 0;
            foreach (var line in lines) {
                if (line.Equals(string.Empty)) {
                    elves.Add(new Elf(calories));
                    calories = 0;
                } else {
                    calories += int.Parse(line);
                }
            }


            int result = elves.Max(x => x.Calories);
            Console.WriteLine($"Max Calories is : {result}");

            List<Elf> orderedElves = elves.OrderByDescending(x => x.Calories).ToList();
            int total = orderedElves[0].Calories + orderedElves[1].Calories + orderedElves[2].Calories;
            Console.WriteLine($"Total Calories is : {total}");
        }
    }

    internal class Elf {
        private int calories;

        public Elf(int calories) {
            this.calories = calories;
        }

        public int Calories => calories;
    }
}