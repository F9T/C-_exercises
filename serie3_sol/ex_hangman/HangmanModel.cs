using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using hangman_lib;

namespace ex_hangman
{
    /// <summary>
    /// Cette classe s'occupe des données du jeu.
    /// </summary>
    public class HangmanModel
    {
        private Hangman hangman;

        public string PathFile { get; set; }
        
        public Tuple<StringBuilder, StringBuilder> CurrentPlayWord { get; set; }

        public bool WordFind => CheckWin(CurrentPlayWord.Item2.ToString());
        public int CurrentCount { get; set; }

        public HangmanModel()
        {
            CurrentCount = 0;
            PathFile = "";
        }

        /// <summary>
        /// Choisir un mot a trouvé au hasard.
        /// </summary>
        public void GetRandomWord()
        {
            CurrentPlayWord = hangman.ChoiceRandomWord();
            CurrentCount = 0;
        }

        /// <summary>
        /// Vérifie si le score réussi est un nouveau record
        /// </summary>
        /// <returns>bool : vrai si c'est un record, faux sinon</returns>
        public bool IsNewRecord()
        {
            return hangman.IsNewRecord(CurrentPlayWord.Item1, CurrentCount);
        }

        /// <summary>
        /// Met à jour le nouveau score du mot dans le dictionnaire ssi c'est un nouveau record
        /// </summary>
        public void UpdateHangmanScore()
        {
            hangman.UpdateWordScore(CurrentPlayWord.Item1, CurrentCount);
        }

        /// <summary>
        /// Récupère tous les scores de tous les mots existants
        /// </summary>
        /// <returns>list(tuple(StringBuilder, StringBuilder, int)) : une liste de tuple contenant le mot a trouvé, 
        /// le mot caché et le score</returns>
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

        /// <summary>
        /// Dévoile une lettre trouvé dans le mot caché
        /// </summary>
        /// <param name="_char">char : la lettre trouvée</param>
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

        /// <summary>
        /// Vérifie si le joueur a gagné.
        /// </summary>
        /// <param name="_currentWord">string : le mot caché actuel</param>
        /// <returns>bool : vrai sivictoire, faux sinon</returns>
        private bool CheckWin(string _currentWord)
        {
            return !_currentWord.Contains('_');
        }

        /// <summary>
        /// Ajoute un nouveau mot dans le dictionnaire. Créer un nouveau fichier si aucun n'a été ouvert. (chemin courant)
        /// </summary>
        /// <param name="_word">string : le nouveau mot</param>
        public void AddNewWord(string _word)
        {
            if (hangman == null)
            {
                hangman = new Hangman();
                PathFile = $@"{Directory.GetCurrentDirectory()}\word.ser";
            }
            hangman.AddNewWord(_word);
        }

        /// <summary>
        /// Vérifie si un fichier a été ouvert.
        /// </summary>
        /// <returns>bool : vrai si ouvert, sinon faux</returns>
        public bool IsFileOpen()
        {
            return hangman != null && PathFile.Length > 3;
        }

        /// <summary>
        /// Vérifie si le fichier est ouvert et que le dictionnaire contient au minimum un mot
        /// </summary>
        /// <returns>bool : vrai s'il contient un mot, faux sinon</returns>
        public bool ContainsWords()
        {
            return IsFileOpen() && hangman.CountWords() > 0;
        }

        /// <summary>
        /// Deserialise le fichier binaire
        /// </summary>
        /// <param name="_path">string : le chemin du fichier binaire</param>
        /// <returns>int : le nombre de mots du dictionnaire (si exception retourne -1)</returns>
        public int Deserialize(string _path)
        {
            hangman = HangmanSerializer.Deserialize(_path);
            if (hangman != null)
            {
                PathFile = _path;
                return hangman.CountWords();
            }
            return -1;
        }

        /// <summary>
        /// Serialize le jeu sous forme binaire
        /// </summary>
        /// <param name="_path">string : le chemin du fichier a sauvegardé</param>
        /// <returns>bool : vrai si la sauvegarde a réussie, faux sinon</returns>
        public bool Serialize(string _path)
        {
            if (hangman == null)
            {
                hangman = new Hangman();
            }
            if (HangmanSerializer.Serialize(_path, hangman))
            {
                PathFile = _path;
                return true;
            }
            return false;
        }
    }
}