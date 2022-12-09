using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Advent_of_code_2022.Day7 {
    public class DaySeven : DaySolver {
        private static readonly string textFile = @"C:\Workspace\C#\Advent-of-code-2022\Advent-of-code-2022\Day7\data.txt";
        private static Directory rootDirectory = new Directory("/", null);
        private static Directory currentDirectory;
        private static int lineIdx = 0;
        private static readonly long requiredSpace = 30000000L;
        private static readonly long fileSystemSpace = 70000000L;

        public static void Solve() {
            if (!System.IO.File.Exists(textFile)) throw new Exception("Data file not found");

            string[] lines = System.IO.File.ReadAllLines(textFile);
            Long totalSize = new Long(0);

            for (int i = 0; i < lines.Length; i++) {
                if (lines[i][0].Equals('$')) {
                    lineIdx = i;
                    ExecuteCommand(lines[i].Substring(2), lines);
                }
            }

            rootDirectory.CalculateSize();
            GetTotalSize(rootDirectory, totalSize, 100000L);

            long unusedSpace = fileSystemSpace - rootDirectory.size;
            long minSize = requiredSpace - unusedSpace;
            HashSet<long> eligibleDirectories = new HashSet<long>();
            
            CalculateEligibleDirectories(rootDirectory, eligibleDirectories, minSize);

            Console.Write($"Total size: {eligibleDirectories.Min()}");
        }

        public static void GetTotalSize(Directory directory, Long current, long limitSize) {
            if (directory.size <= limitSize) {
                current.value += directory.size;
            }

            directory.directories.ForEach(x => GetTotalSize(x, current, limitSize));
        }
        
        public static void CalculateEligibleDirectories(Directory directory, HashSet<long> list, long minSize) {
            if (directory.size >= minSize) {
                list.Add(directory.size);
            }

            directory.directories.ForEach(x => CalculateEligibleDirectories(x, list, minSize));
        }

        public static void ExecuteCommand(string command, string[] lines) {
            string[] split = command.Split(' ');

            if (split[0] == "cd") {
                if (split[1] == "/") {
                    currentDirectory = rootDirectory;
                } else if (split[1] == "..") {
                    currentDirectory = currentDirectory != rootDirectory ? currentDirectory.parent : currentDirectory;
                } else {
                    currentDirectory = currentDirectory.directories.Find(x => x.name == split[1]);
                }
            } else if (split[0] == "ls") {
                for (int i = lineIdx + 1; i < lines.Length; i++) {
                    if (lines[i][0].Equals('$')) break;

                    string[] splitElem = lines[i].Split(' ');

                    if (splitElem[0] == "dir") {
                        if (!currentDirectory.directories.Exists(x => x.name == splitElem[1])) {
                            currentDirectory.directories.Add(new Directory(splitElem[1], currentDirectory));
                        }
                    } else {
                        if (!currentDirectory.files.Exists(x => x.name == splitElem[1])) {
                            currentDirectory.files.Add(new File(splitElem[1], long.Parse(splitElem[0])));
                        }
                    }
                }
            } else {
                throw new Exception("Command not recognized");
            }
        }
    }

    public class Long {
        public long value;

        public Long(long value) {
            this.value = value;
        }
    }

    public class Directory {
        public Directory parent;
        public string name;
        public List<File> files;
        public List<Directory> directories;
        public long size;

        Directory() { }

        public Directory(string name, Directory parent) {
            this.name = name;
            this.parent = parent;
            this.files = new List<File>();
            this.directories = new List<Directory>();
            this.size = 0;
        }

        public long CalculateSize() {
            this.size = this.files.Sum(x => x.size);

            this.size += this.directories.Sum(x => x.CalculateSize());

            return this.size;
        }
    }

    public class File {
        public long size;
        public string name;

        public File(string name, long size) {
            this.name = name;
            this.size = size;
        }
    }
}