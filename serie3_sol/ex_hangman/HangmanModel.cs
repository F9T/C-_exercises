using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hangman_lib;

namespace ex_hangman
{
    public class HangmanModel
    {
        private Hangman hangman;

        public string PathFile { get; set; }

        public HangmanModel()
        {
            
        }

        public bool ContainsWords()
        {
            return hangman != null && hangman.CountWords() > 0;
        }

        public int Deserialize(string _path)
        {
            hangman = HangmanSerializer.Deserialize(_path);
            if (hangman != null)
            {
                return hangman.CountWords();
            }
            return 0;
        }

        public bool Serialize(string _path)
        {
            return HangmanSerializer.Serialize(_path, hangman);
        }
    }
}