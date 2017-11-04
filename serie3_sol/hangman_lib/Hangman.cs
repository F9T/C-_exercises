using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace hangman_lib
{
    /// <summary>
    /// Cette classe est serializable sous forme binaire afin d'éviter qu'une personne puisse "tricher" 
    /// en ouvrant le fichier sous forme de texte.
    /// </summary>
    [Serializable]
    public class Hangman
    {
        private Dictionary<StringBuilder, int> dictWordScore;

        public Hangman()
        {
            dictWordScore = new Dictionary<StringBuilder, int>();
        }

        /// <summary>
        /// Le nombre de paires de mots et de scores existants dans le dictionnaire.
        /// </summary>
        /// <returns>int : le nombre de paires de mots et de scorest</returns>
        public int CountWords()
        {
            //null-coalescing epxression with obj?.
            return dictWordScore?.Count ?? 0;
        }

        /// <summary>
        /// Ajoute un nouveau mot dans le dictionnaire
        /// </summary>
        /// <param name="_word">string : le nouveau mot</param>
        public void AddNewWord(string _word)
        {
            AddNewWord(new StringBuilder(RemoveDiacritics(_word.ToLower())));
        }

        /// <summary>
        /// Ajoute un nouveau mot dans le dictionnaire
        /// </summary>
        /// <param name="_word">string : le nouveau mot</param>
        private void AddNewWord(StringBuilder _word)
        {
            if (dictWordScore == null)
            {
                dictWordScore = new Dictionary<StringBuilder, int>();
            }
            dictWordScore.Add(_word, -1);
        }

        /// <summary>
        /// Met à jour le score du mot seulement si c'est un nouveau record
        /// </summary>
        /// <param name="_word">string : le mot à modifié</param>
        /// <param name="_score">int : le score a ajouté</param>
        public void UpdateWordScore(StringBuilder _word, int _score)
        {
            //Update only if it's a new record
            if (IsNewRecord(_word, _score))
            {
                dictWordScore[_word] = _score;
            }
        }

        /// <summary>
        /// Contrôle si le score est un nouveau record pour un mot donné
        /// </summary>
        /// <param name="_word">string : le mot à vérifier</param>
        /// <param name="_score">int : le score réussi</param>
        /// <returns>bool : vrai si c'est un nouveau record, faux sinon</returns>
        public bool IsNewRecord(StringBuilder _word, int _score)
        {
            dictWordScore.TryGetValue(_word, out int score);
            return score == -1 || _score < score;
        }

        /// <summary>
        /// Choisi un mot aléatoirement dans le dictionnaire
        /// </summary>
        /// <returns>tuple(StringBuilder, StringBuilder) : un tuple de 2 éléments, le premier le mot à trouver et le second le mot caché</returns>
        public Tuple<StringBuilder, StringBuilder> ChoiceRandomWord()
        {
            int random = new Random().Next(0, dictWordScore.Count);
            var wordToFind = dictWordScore.Keys.ToArray()[random];
            var hideWord = GetHideString(wordToFind.Length);
            return new Tuple<StringBuilder, StringBuilder>(wordToFind, hideWord);
        }

        /// <summary>
        /// Recupère une chaîne de caractère de '_' au nombre entré en paramètre.
        /// </summary>
        /// <param name="_length">int : longueur de la chaine de '_'</param>
        /// <returns>string : une chaine de caractère</returns>
        public StringBuilder GetHideString(int _length)
        {
            var hideWord = new StringBuilder();
            for (var i = 0; i < _length; i++)
            {
                hideWord.Append("_");
            }
            return hideWord;
        }

        /// <summary>
        /// Recupère le dictonnaire des mots et des scores
        /// </summary>
        /// <returns>Dictionary(StringBuilder, int) : un dictionnaire de StringBuilder et d'entier</returns>
        public Dictionary<StringBuilder, int> GetWordScore()
        {
            return dictWordScore;
        }

        /// <summary>
        /// Supprime tous les accents d'une chaîne de caractères.
        /// Source : http://www.levibotelho.com/development/c-remove-diacritics-accents-from-a-string/
        /// </summary>
        /// <param name="_text">string : chaine de caractère</param>
        /// <returns>string : la meme chaine qu'en paramètre sans accent</returns>
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