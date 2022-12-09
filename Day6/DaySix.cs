using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_code_2022.Day2 {
    public class DaySix : DaySolver {
        private static readonly string textFile = @"C:\Workspace\C#\Advent-of-code-2022\Advent-of-code-2022\Day6\data.txt";
        
        public static void Solve() {
            if (!File.Exists(textFile)) throw new Exception("Data file not found");

            string text = File.ReadAllText(textFile);
            int firstMarkerIdx = 0;

            for (int i = 0; i < text.Length; i++) {
                string part = text.Substring(i, (i + 14 < text.Length) ? 14 : (text.Length - i));

                bool findOccurence = false;
                List<char> array = new List<char>();
                for (int j = 0; j < part.Length; j++) {
                    if (array.Contains(part[j])) {
                        findOccurence = true;
                        break;
                    }
                    
                    array.Add(part[j]);
                }

                if (!findOccurence) {
                    firstMarkerIdx = i + 14;
                    break;
                }
            }

            Console.Write($"Marker: {firstMarkerIdx}");
        }
    }
}