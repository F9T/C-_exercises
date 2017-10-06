using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex5
{
    public class Program
    {
        /// <summary>
        /// Comparaison de deux strings avec :
        ///     -Equals : vérifie si deux string ont la même valeur (texte) uniquement, la valeur null est équale à rien
        ///     -CompareTo : compare l'instance des strings et renvoie un entier si l'instance précéde, 
        ///                  suit ou apparait dans la même position dans l'ordre de tri spécifié
        ///     -ReferenceEquals : vérifie si les références des deux strings sont identiques, indépendemment des valeurs des strings
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        public static void StringCompare(string s1, string s2)
        {
            Console.WriteLine($"{"",2}Equals : {s1.Equals(s2)}");
            Console.WriteLine($"{"",2}CompareTo : {s1.CompareTo(s2)}");
            Console.WriteLine($"{"",2}ReferenceEquals : {ReferenceEquals(s1, s2)}");
        }

        public static void Main(string[] args)
        {
            string s1 = "Hello World";
            string s2 = "Hello World";
            string s3 = s1;

            Console.WriteLine("Compare s1 with s2 :");
            StringCompare(s1, s2);
            Console.WriteLine("Compare s1 with s3 :");
            StringCompare(s1, s3);
            Console.WriteLine("Compare s2 with s3 :");
            StringCompare(s2, s3);
            s3 += "!"; //On change la référence de la string
            Console.WriteLine("\nModification de s3 :\n");
            Console.WriteLine("Compare s1 with s2 :");
            StringCompare(s1, s2);
            Console.WriteLine("Compare s1 with s3 :");
            StringCompare(s1, s3);
            Console.WriteLine("Compare s2 with s3 :");
            StringCompare(s2, s3);
            Console.ReadLine();
        }
    }
}
