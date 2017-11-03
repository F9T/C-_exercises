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
        private bool isSaved;

        public string PathFile { get; set; }
        
        public Tuple<StringBuilder, StringBuilder> CurrentPlayWord { get; set; }

        public bool WordFind => CheckWin(CurrentPlayWord.Item2.ToString());
        public int CurrentCount { get; set; }

        public HangmanModel()
        {
            this.CurrentCount = 0;
            this.isSaved = true;
        }

        public void GetRandomWord()
        {
            CurrentPlayWord = hangman.ChoiceRandomWord();
            this.CurrentCount = 0;
        }

        public bool IsNewRecord()
        {
            return hangman.IsNewRecord(CurrentPlayWord.Item1, CurrentCount);
        }

        public void UpdateHangmanScore()
        {
            hangman.UpdateWordScore(CurrentPlayWord.Item1, CurrentCount);
            isSaved = false;
        }

        public List<Tuple<StringBuilder, StringBuilder, int>> GetWordScore()
        {
            List<Tuple<StringBuilder, StringBuilder, int>> wordScore = new List<Tuple<StringBuilder, StringBuilder, int>>();
            foreach (var entry in hangman.GetWordScore())
            {
                var word = entry.Key;
                var hideStr = hangman.GetHideString(word.Length);
                wordScore.Add(new Tuple<StringBuilder, StringBuilder, int>(word, hideStr, entry.Value));
            }
            return wordScore;
        }

        public void InputLetter(char _char)
        {
            string currentWord = CurrentPlayWord.Item1.ToString();
            if (currentWord.Contains(_char))
            {
                for (int i = 0; i < currentWord.Length; i++)
                {
                    if (currentWord[i] == _char)
                    {
                        CurrentPlayWord.Item2[i] = _char;
                    }
                }

            }
            CurrentCount++;
        }

        private bool CheckWin(string _currentWord)
        {
            return !_currentWord.Contains('_');
        }

        public void AddNewWord(string _word)
        {
            if (!FileCreate())
            {
                hangman = new Hangman();
            }
            hangman.AddNewWord(_word);
            isSaved = false;
        }

        public bool FileCreate()
        {
            return hangman != null;
        }

        public bool ContainsWords()
        {
            return FileCreate() && hangman.CountWords() > 0;
        }

        public bool IsSaved()
        {
            return isSaved && PathFile != null;
        }

        public int Deserialize(string _path)
        {
            hangman = HangmanSerializer.Deserialize(_path);
            if (FileCreate())
            {
                PathFile = _path;
                isSaved = true;
                return hangman.CountWords();
            }
            return 0;
        }

        public bool Serialize(string _path)
        {
            if (HangmanSerializer.Serialize(_path, hangman))
            {
                PathFile = _path;
                isSaved = true;
                return true;
            }
            return false;
        }
    }
}