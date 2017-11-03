using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hangman_lib;

namespace word_generator
{
    class Program
    {
        public static void Main(string[] _args)
        {
            Hangman hangman = new Hangman();
            hangman.AddNewWord("patate");
            hangman.AddNewWord("casserole");
            hangman.AddNewWord("canape");
            hangman.AddNewWord("pomme");
            HangmanSerializer.Serialize("word.ser", hangman);
        }
    }
}
