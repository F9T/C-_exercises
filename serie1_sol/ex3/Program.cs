using System;
using System.IO;

namespace ex3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //Le fichier mesures.txt est copié depuis la solution dans le fichier de build directement au post-build (propriété du projet)
                using (StreamReader sr = new StreamReader(new FileStream(@"mesures.txt", FileMode.Open, FileAccess.Read)))
                {
                    short i = 1, numberPerLine = 10;
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (line != null)
                        {
                            Console.Write($"{line}");
                            //Permet juste de ne pas avoir une virgule à a fin
                            if (!sr.EndOfStream)
                            {
                                Console.Write(", ");
                            }
                            //Saute une ligne si le module retourne 0 et incrémente le i d'un élément
                            if (i++ % numberPerLine == 0)
                            {
                                Console.WriteLine();
                            }
                        }
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error read file");
            }
            Console.ReadLine();
        }
    }
}
