using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex_hangman
{
    [Serializable]
    public class Hangman
    {
        private Dictionary<StringBuilder, int> dictWordScore;
        private Tuple<StringBuilder, StringBuilder> currentPlayWord;

        public Hangman()
        {

        }

        public void ChoiceRandomWord()
        {
            const int min = 0;
            int max = dictWordScore.Count;
            int random = new Random().Next(min, max);
            var wordToFind = new StringBuilder(dictWordScore.Keys.ToArray()[random].ToString());
            var hideWord = new StringBuilder(wordToFind.Capacity);
            for (var i = 0; i < hideWord.Capacity; i++)
            {
                hideWord.Append("_");
            }
            this.currentPlayWord = new Tuple<StringBuilder, StringBuilder>(wordToFind, hideWord);
        }
    }
}