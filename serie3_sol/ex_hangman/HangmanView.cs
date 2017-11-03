using System;
using System.Text;

namespace ex_hangman
{
    public class HangmanView
    {
        public void PrintHangman(string _currentFindWord)
        {
            PrintMessage($"Mot : {_currentFindWord}");
            PrintMessage("Proposer une lettre : ", false);
        }

        public void PrintEnd(string _wordFind, int _numberInput, bool _isNewRecord)
        {
            PrintMessage($"Bravo, vous avez trouvé le mot {_wordFind} en {_numberInput} essais", false);
            if (_isNewRecord)
            {
                PrintMessage(" (nouveau record)", false);
            }
            PrintMessage("");
        }

        public void PrintScore(Tuple<StringBuilder, StringBuilder, int> _wordScores)
        {
            PrintMessage($"{_wordScores.Item1.GetHashCode()} : {_wordScores.Item2}  ", false);
            PrintMessage(_wordScores.Item3 > -1 ? $"{_wordScores.Item3} essais" : "pas trouvé");
        }
        public void PrintOptions()
        {
            PrintMessage("Vos options sont :");
            PrintMessage("1) Choisir un fichier de mots");
            PrintMessage("2) Jouer (trouver un mot)");
            PrintMessage("3) Afficher les scores");
            PrintMessage("4) Ajouter un nouveau mot");
            PrintMessage("5) Sauvegarder le fichier de mots");
            PrintMessage("6) Terminer");
            PrintMessage("Choix : ", false);
        }

        public void PrintMessage(string _message, bool _newLine = true)
        {
            if (_newLine)
            {
                Console.WriteLine($"{_message}");
            }
            else
            {
                Console.Write($"{_message}");
            }
        }
    }
}