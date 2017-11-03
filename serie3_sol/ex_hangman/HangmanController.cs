using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ex_hangman
{
    public class HangmanController
    {
        private readonly HangmanModel hangmanModel;
        private readonly HangmanView hangmanView;

        public HangmanController()
        {
            this.hangmanModel = new HangmanModel();
            this.hangmanView = new HangmanView();
        }

        public bool AddNewWord(string _word)
        {
            if (IsAWord(_word))
            {
                hangmanModel.AddNewWord(_word);
                return true;
            }
            return false;
        }

        private bool IsAWord(string _word)
        {
            return Regex.IsMatch(_word, @"^[A-Za-z]+$");
        }

        public bool IsFileOpen()
        {
            return hangmanModel.ContainsWords();
        }

        public void RandomWord()
        {
            hangmanModel.GetRandomWord();
        }

        public bool Play()
        {
            hangmanView.PrintHangman(hangmanModel.CurrentPlayWord.Item2.ToString());
            string str = Console.ReadLine();
            if (str != null && str.Length == 1)
            {
                hangmanModel.InputLetter(str[0]);
            }
            return hangmanModel.WordFind;
        }

        public void End()
        {
            hangmanView.PrintWord(hangmanModel.CurrentPlayWord.Item2.ToString());
            hangmanView.PrintEnd(hangmanModel.CurrentPlayWord.Item1.ToString(), hangmanModel.CurrentCount, hangmanModel.IsNewRecord());
            hangmanModel.UpdateHangmanScore();
        }

        public void DisplayScores()
        {
            foreach (var wordScore in hangmanModel.GetWordScore())
            {
                hangmanView.PrintScore(wordScore);
            }
        }

        public int Deserialize(string _path)
        {
            return hangmanModel.Deserialize(_path);
        }

        public bool Serialize(string _path)
        {
            return hangmanModel.Serialize(_path);
        }
    }
}