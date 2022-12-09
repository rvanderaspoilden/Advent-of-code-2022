using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_code_2022.Day2 {
    public class DayFive : DaySolver {
        private static readonly string textFile = @"C:\Workspace\C#\Advent-of-code-2022\Advent-of-code-2022\Day5\data.txt";
        
        public static void Solve() {
            if (!File.Exists(textFile)) throw new Exception("Data file not found");

            string[] lines = File.ReadAllLines(textFile);
            int stackLineIdx = 0;
            List<Stack<char>> stacks = new List<Stack<char>>();

            // Init stacks
            for (int i = 0; i < lines.Length; i++) {
                if (int.TryParse(lines[i].Replace(" ", string.Empty), out int result)) {
                    stackLineIdx = i;
                    string stackLine = lines[i];
                    
                    // Check all chars
                    for (int j = 0; j < stackLine.Length; j++) {
                        if (stackLine[j] != ' ') {
                            int crateColumnIdx = j;
                            
                            // Fill the stack
                            Stack<char> stack = new Stack<char>();

                            for (int k = stackLineIdx - 1; k >= 0; k--) {
                                string crateLine = lines[k];

                                if (crateColumnIdx < crateLine.Length && crateLine[crateColumnIdx] != ' ') {
                                    stack.Push(crateLine[crateColumnIdx]);
                                }
                            }
                            
                            stacks.Add(stack);
                        }
                    }
                    
                    break;
                }
            }
            
            // Read instructions
            for (int i = stackLineIdx + 2; i < lines.Length; i++) {
                string[] split = lines[i].Split(' ');
                int quantityToMove = int.Parse(split[1]);
                int from = int.Parse(split[3]);
                int to = int.Parse(split[5]);

                Stack<char> tmp = new Stack<char>();
                for (int j = 0; j < quantityToMove; j++) {
                    char crate = stacks[from - 1].Pop();
                   tmp.Push(crate);
                }

                foreach (var item in tmp) {
                    stacks[to - 1].Push(item);
                }
            }

            string message = new string(stacks.Select(x => x.Peek()).ToArray());

            Console.Write($"Message: {message}");
        }
    }
}