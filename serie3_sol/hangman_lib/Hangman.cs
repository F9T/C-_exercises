﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hangman_lib
{
    [Serializable]
    public class Hangman
    {
        private Dictionary<StringBuilder, int> dictWordScore;

        public int CountWords()
        {
            //null-coalescing epxression with ?
            return dictWordScore?.Count ?? 0;
        }

        public void AddNewWord(string _word)
        {
            this.AddNewWord(new StringBuilder(_word.ToLower()));
        }

        private void AddNewWord(StringBuilder _word)
        {
            if (dictWordScore == null)
            {
                dictWordScore = new Dictionary<StringBuilder, int>();
            }
            dictWordScore.Add(_word, -1);
        }

        public void UpdateWordScore(StringBuilder _word, int _score)
        {
            dictWordScore.TryGetValue(_word, out int score);
            if (IsNewRecord(_word, _score))
            {
                dictWordScore[_word] = _score;
            }
        }

        public bool IsNewRecord(StringBuilder _word, int _score)
        {
            dictWordScore.TryGetValue(_word, out int score);
            return score == -1 || _score < score;
        }

        public Tuple<StringBuilder, StringBuilder> ChoiceRandomWord()
        {
            int random = new Random().Next(0, dictWordScore.Count);
            var wordToFind = dictWordScore.Keys.ToArray()[random];
            var hideWord = GetHideString(wordToFind.Length);
            return new Tuple<StringBuilder, StringBuilder>(wordToFind, hideWord);
        }

        public StringBuilder GetHideString(int _length)
        {
            var hideWord = new StringBuilder();
            for (var i = 0; i < _length; i++)
            {
                hideWord.Append("_");
            }
            return hideWord;
        }

        public Dictionary<StringBuilder, int> GetWordScore()
        {
            return dictWordScore;
        }
    }
}