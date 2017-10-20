using System;
using System.IO;

namespace ex4
{
    public class Program
    {
        public static void publicationDesdonnees() { Console.ReadKey(); }

        public static void Main(string[] args)
        {
            string[] lines = null;
            string path = null;
            try
            {
                path = Console.ReadLine();
                path = @"..\..\..\data1.txt";
                lines = File.ReadAllLines(path);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Environment.Exit(0);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Environment.Exit(0);
            }
            int[] datas = new int[lines.Length];
            int index = 0;
            foreach (string line in lines)
            {
                try
                {
                    datas[index] = Convert.ToInt32(line);
                    index++;
                    Console.Write($"{line} ");
                }
                catch (OverflowException e) { Console.WriteLine($"line {index}: {e.Message}"); }
                catch (FormatException e) { Console.WriteLine($"line {index}: {e.Message}"); }
            }
            int[] tabDataFilter = new int[datas.Length - 2];
            for (int i = 0; i < tabDataFilter.Length; i++)
            {
                tabDataFilter[i] = datas[i] / (datas[i + 1] - datas[i + 2]);
            }
            Console.WriteLine();
            foreach (int value in tabDataFilter)
            {
                Console.Write($"{value} ");
            }
            string savePath = Path.GetFileNameWithoutExtension(path);
            string content = string.Join("\n", tabDataFilter);
            File.WriteAllText($"{savePath}_filter.txt", content);
            Console.ReadLine();
        }
    }
}