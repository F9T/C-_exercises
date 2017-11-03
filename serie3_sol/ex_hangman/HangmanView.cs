using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex_hangman
{
    public class HangmanView
    {
        public void PrintHangman(string _currentFindWord)
        {
            PrintWord(_currentFindWord);
            Console.Write("Proposer une lettre : ");
        }

        public void PrintWord(string _currentFindWord)
        {
            Console.WriteLine($"Mot : {_currentFindWord}");
        }

        public void PrintEnd(string _wordFind, int _numberInput, bool _isNewRecord)
        {
            Console.Write($"Bravo, vous avez trouvé le mot {_wordFind} en {_numberInput} essais");
            if (_isNewRecord)
            {
                Console.Write(" (nouveau record)");
            }
            Console.WriteLine();
        }

        public void PrintScore(Tuple<StringBuilder, StringBuilder, int> _wordScores)
        {
            Console.Write($"{_wordScores.Item1.GetHashCode()} : {_wordScores.Item2}  ");
            Console.WriteLine(_wordScores.Item3 > -1 ? $"{_wordScores.Item3} essais" : "pas trouvé");
        }
    }
}