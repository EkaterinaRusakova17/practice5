using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string directoryPath = "C:\\Users\\kater\\OneDrive\\Рабочий стол"; 

        Dictionary<string, string> incorrectWords = new Dictionary<string, string>()
        {
            { "привет", "првиет" },
            { "hello", "helo" },
            { "hi", "ih" }
        };

        ProcessFiles(directoryPath, incorrectWords);
    }

    static void ProcessFiles(string directoryPath, Dictionary<string, string> incorrectWords)
    {
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

            FileInfo[] files = directoryInfo.GetFiles("*.txt", SearchOption.AllDirectories);

            foreach (FileInfo file in files)
            {
                Console.WriteLine($"Обработка файла: {file.FullName}");

                string content = File.ReadAllText(file.FullName);

                foreach (var incorrectWord in incorrectWords)
                {
                    content = content.Replace(incorrectWord.Key, incorrectWord.Value);
                }

                content = Regex.Replace(content, @"\((\d{3})\)\s?(\d{3})-(\d{2})-(\d{2})", "+380 $1 $2 $3 $4");

                File.WriteAllText(file.FullName, content);
            }

            Console.WriteLine("Обработка файлов завершена.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}
