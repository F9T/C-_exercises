using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace hangman_lib
{
    [Serializable]
    public class Hangman
    {
        private Dictionary<StringBuilder, int> dictWordScore;

        public Hangman()
        {
            dictWordScore = new Dictionary<StringBuilder, int>();
        }

        public int CountWords()
        {
            //null-coalescing epxression with obj?.
            return dictWordScore?.Count ?? 0;
        }

        public void AddNewWord(string _word)
        {
            AddNewWord(new StringBuilder(RemoveDiacritics(_word.ToLower())));
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
            //Update only if it's a new record
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

        //Source : http://www.levibotelho.com/development/c-remove-diacritics-accents-from-a-string/
        public static string RemoveDiacritics(string _text)
        {
            if (string.IsNullOrWhiteSpace(_text))
                return _text;

            _text = _text.Normalize(NormalizationForm.FormD);
            var chars = _text.Where(_c => CharUnicodeInfo.GetUnicodeCategory(_c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }
    }
}