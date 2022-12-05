using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_code_2022.Day2 {
    public class DayFour : DaySolver {
        private static readonly string textFile = @"C:\Workspace\C#\Advent-of-code-2022\Advent-of-code-2022\Day4\data.txt";
        
        public static void Solve() {
            if (!File.Exists(textFile)) throw new Exception("Data file not found");

            string[] lines = File.ReadAllLines(textFile);
            int total = 0;

            foreach (var line in lines) {
                string[] ranges = line.Split(',');
                List<int> firstRange = CreateRange(int.Parse(ranges[0].Split('-')[0]), int.Parse(ranges[0].Split('-')[1]));
                List<int> secondRange = CreateRange(int.Parse(ranges[1].Split('-')[0]), int.Parse(ranges[1].Split('-')[1]));

                List<int> intersect = firstRange.Intersect(secondRange).ToList();

                if (intersect.Count > 0) {
                    total++;
                }
            }
            

            Console.Write($"Total : {total}");
        }

        public static List<int> CreateRange(int start, int end) {
            List<int> list = new List<int>();
            
            for (int i = start; i <= end; i++) {
                list.Add(i);
            }

            return list;
        }
    }
}