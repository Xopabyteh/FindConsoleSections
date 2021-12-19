#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        
        private static void Main()
        {
            
            string? projectsPath = string.Empty; //@"D:\!VisualStudioProjects\source"
            while (string.IsNullOrEmpty(projectsPath))
            {
                Console.Write("Your projects path: ");
                projectsPath = Console.ReadLine();
            }

            IEnumerable<string> csFiles = FindCsFiles(projectsPath);
            StringBuilder builder = new StringBuilder();
            foreach (var csFile in csFiles)
            {
                string? consoleSections = FindConsoleSections(csFile);
                if (!string.IsNullOrWhiteSpace(consoleSections))
                {
                    builder.AppendLine(csFile).AppendLine(consoleSections);
                }
                
            }

            Console.WriteLine(builder.ToString());
        }

        private static string? FindConsoleSections(string csFilePath)
        {
            string data = File.ReadAllText(csFilePath);
            string[] lines = data.Split(Environment.NewLine);
            StringBuilder builder = new StringBuilder();
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                line = line.TrimStart();
                if (line.Contains("Console"))
                {
                    builder.Append("\tLine ").Append(i + 1).Append(" - ").AppendLine(line);
                }
            }

            return builder.ToString();
        }

        private static IEnumerable<string> FindCsFiles(string path)
        { 
            IEnumerable<string> csFiles = Directory.EnumerateFileSystemEntries(path, "*.cs", SearchOption.AllDirectories);
            return csFiles;
        }
            
    }
}
