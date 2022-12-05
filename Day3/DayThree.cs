using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_code_2022.Day2 {
    public class DayThree : DaySolver {
        private static readonly string textFile = @"C:\Workspace\C#\Advent-of-code-2022\Advent-of-code-2022\Day3\data.txt";
        private static readonly string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        
        public static void Solve() {
            if (!File.Exists(textFile)) throw new Exception("Data file not found");

            string[] lines = File.ReadAllLines(textFile);
            int total = 0;
            
            for (int i = 0; i < lines.Length; i+=3) {
                string firstElf = lines[i];
                string secondElf = lines[i + 1];
                string thirdElf = lines[i + 2];
                
                char[] items = findOccurences(firstElf, secondElf, thirdElf);

                foreach (var item in items) {
                    int value = alphabet.IndexOf(item) + 1;

                    total += value;
                }
            }

            Console.Write($"Total : {total}");
        }

        static char[] findOccurences(string compartment1, string compartment2, string compartment3) {
            HashSet<char> occurences = new HashSet<char>();
            
            for (int i = 0; i < compartment1.Length; i++) {
                if (compartment2.IndexOf(compartment1[i]) != -1) {
                    if (compartment3.IndexOf(compartment1[i]) != -1) {
                        occurences.Add(compartment1[i]);
                    }
                }
            }

            return occurences.ToArray();
        }
    }
}