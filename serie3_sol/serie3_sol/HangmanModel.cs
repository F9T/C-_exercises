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

        public bool ContainsWords { get; set; }

        public HangmanModel()
        {
            
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

        public void Serialize(string _path)
        {
            if (hangman != null)
            {
                HangmanSerializer.Serialize(_path, hangman);
            }
        }
    }
}