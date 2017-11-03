using System;
using hangman_lib;

namespace word_generator
{
    class Program
    {
        public static void Main(string[] _args)
        {
            var hangman = new Hangman();
            hangman.AddNewWord("patate");
            hangman.AddNewWord("casserole");
            hangman.AddNewWord("canape");
            hangman.AddNewWord("pomme");
            hangman.AddNewWord("champs");
            hangman.AddNewWord("Chariot");
            hangman.AddNewWord("Ruisseau");
            HangmanSerializer.Serialize("word.ser", hangman);
            Console.WriteLine("Word created");
            Console.ReadLine();
        }
    }
}
