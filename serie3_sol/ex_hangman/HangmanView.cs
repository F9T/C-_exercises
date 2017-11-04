using System;
using System.Text;

namespace ex_hangman
{
    /// <summary>
    /// Cette classe s'occupe de l'affichage du jeu
    /// </summary>
    public class HangmanView
    {
        /// <summary>
        /// Affiche le mot totalement ou partiellement caché a trouvé
        /// </summary>
        /// <param name="_currentFindWord">string : le mot caché</param>
        public void PrintHangman(string _currentFindWord)
        {
            PrintMessage($"Mot : {_currentFindWord}");
            PrintMessage("Proposer une lettre : ", false);
        }

        /// <summary>
        /// Affiche le message de fin de la partie avec le nombre de coup fait.
        /// </summary>
        /// <param name="_wordFind">string : le mot trouvé</param>
        /// <param name="_numberInput">int : le nombre de coups</param>
        /// <param name="_isNewRecord">bool : si c'est un nouveau record ou non</param>
        public void PrintEnd(string _wordFind, int _numberInput, bool _isNewRecord)
        {
            PrintMessage($"Bravo, vous avez trouvé le mot {_wordFind} en {_numberInput} essais", false);
            if (_isNewRecord)
            {
                PrintMessage(" (nouveau record)", false);
            }
            PrintMessage("");
        }

        /// <summary>
        /// Affiche un score d'un mot
        /// </summary>
        /// <param name="_wordScores">tuple(StringBuilder, StringBuilder, int) : premier élément = le mot a trouvé, 
        /// deuxième = le mot caché et dernier élément le score</param>
        public void PrintScore(Tuple<StringBuilder, StringBuilder, int> _wordScores)
        {
            PrintMessage($"{_wordScores.Item1.GetHashCode()} : {_wordScores.Item2}  ", false);
            PrintMessage(_wordScores.Item3 > -1 ? $"{_wordScores.Item3} essais" : "pas trouvé");
        }

        /// <summary>
        /// Affiche le menu principal du jeu
        /// </summary>
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

        /// <summary>
        /// Affiche un message dans la console
        /// </summary>
        /// <param name="_message">string : le message a affiché</param>
        /// <param name="_newLine">bool : ajouté (vrai) ou non (faux) une nouvelle ligne (par défaut: vrai)</param>
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