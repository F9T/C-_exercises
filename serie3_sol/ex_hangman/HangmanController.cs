using System;
using System.IO;
using System.Text.RegularExpressions;
using hangman_lib;

namespace ex_hangman
{
    /// <summary>
    /// Classe contrôleur du jeu, s'occupe de vérifier les données et de faire le lien entre le model et la vue.
    /// </summary>
    public class HangmanController
    {
        //view and model instances never change (readonly)
        private readonly HangmanModel hangmanModel;

        private readonly HangmanView hangmanView;

        public HangmanController()
        {
            hangmanModel = new HangmanModel();
            hangmanView = new HangmanView();
        }

        /// <summary>
        /// Demande un nouveau mot et vérifie si le nouveau entré est correct.
        /// </summary>
        /// <returns>bool : vrai si il est ajouté, faux sinon</returns>
        public bool AddNewWord()
        {
            hangmanView.PrintMessage("Entrer votre nouveau mot (accent pas pris en compte) : ", false);
            string newWord = Console.ReadLine();
            bool success = IsAWord(newWord);
            hangmanView.PrintMessage(success
                ? $"Le mot {newWord.ToLower()} a été ajouté."
                : $"Le mot {newWord} contient une erreur!");
            if (success)
            {
                hangmanModel.AddNewWord(newWord);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Contrôle si une chaîne de caractère est un mot (il ne contient que des lettres alphabétiques)
        /// </summary>
        /// <param name="_word">string : la chaîne à vérifier</param>
        /// <returns>bool : vrai si c'est un mot, sinon faux</returns>
        private static bool IsAWord(string _word)
        {
            return Regex.IsMatch(_word, @"^[A-Za-z]+$");
        }

        /// <summary>
        /// Contrôle si un fichier de mots est ouvert
        /// </summary>
        /// <returns>bool : vrai si un fichier est ouvert, sinon faux</returns>
        public bool IsFileOpen()
        {
            return hangmanModel.IsFileOpen();
        }

        /// <summary>
        /// Vérifie que le fichier de mots contient au minimum un mot et tire un mot au hasard.
        /// </summary>
        /// <returns>bool : vrai si il a tiré un mot au hasard, sinon faux</returns>
        public bool RandomWord()
        {
            if (hangmanModel.ContainsWords())
            {
                hangmanModel.GetRandomWord();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Affiche le menu de départ.
        /// </summary>
        public void DisplayOptions()
        {
            hangmanView.PrintOptions();
        }

        /// <summary>
        /// Permet de jouer une lettre au pendu.
        /// </summary>
        /// <returns>bool : vrai si il a gagné, faux sinon</returns>
        public bool Play()
        {
            hangmanView.PrintHangman(hangmanModel.CurrentPlayWord.Item2.ToString());
            string str = Console.ReadLine();
            if (str != null && str.Length == 1 && IsAWord(str))
            {
                hangmanModel.InputLetter(Hangman.RemoveDiacritics(str.ToLower())[0]);
            }
            return hangmanModel.WordFind;
        }

        /// <summary>
        /// Met fin à la partie en affichant le score.
        /// </summary>
        public void End()
        {
            hangmanView.PrintMessage($"Mot : {hangmanModel.CurrentPlayWord.Item2}");
            hangmanView.PrintEnd(hangmanModel.CurrentPlayWord.Item1.ToString(), hangmanModel.CurrentCount,
                hangmanModel.IsNewRecord());
            hangmanModel.UpdateHangmanScore();
        }

        /// <summary>
        /// Vérifie si des mots existent dans le dictionnaire et affiche les scores.
        /// </summary>
        public void DisplayScores()
        {
            int size = hangmanModel.GetWordScore().Count;
            if (size > 0)
            {
                foreach (var wordScore in hangmanModel.GetWordScore())
                {
                    hangmanView.PrintScore(wordScore);
                }
            }
            else
            {
                hangmanView.PrintMessage("Aucun mot dans la base de données.");
            }
        }

        /// <summary>
        /// Choix et vérification du chemin du fichier de mots.
        /// </summary>
        /// <param name="_load">bool : vrai si on charge un fichier, faux si on sauvegarde un fichier (par défaut: true)</param>
        /// <returns>string : le chemin vérifié</returns>
        public string ChoosePathFile(bool _load = true)
        {
            string path;
            if (_load)
            {
                hangmanView.PrintMessage("Donner le chemin du fichier (.ser) : ", false);
                path = Console.ReadLine();
            }
            else
            {
                hangmanView.PrintMessage($"Donner le chemin du fichier (.ser) [{hangmanModel.PathFile}] : ", false);
                path = hangmanModel.PathFile + Console.ReadLine();
            }
            return CheckFile(path) ? path : null;
        }

        /// <summary>
        /// Vérifie si un fichier est conforme
        /// </summary>
        /// <param name="_path">string : le chemin du fichier</param>
        /// <returns>bool : vrai si le fichier est conforme, sinon faux</returns>
        private bool CheckFile(string _path)
        {
            if (!string.IsNullOrEmpty(_path))
            {
                try
                {
                    var file = new FileInfo(_path);
                    return file.Exists && file.Extension.ToLower() == ".ser";
                }
                catch (ArgumentException e)
                {
                    hangmanView.PrintMessage(e.Message);
                    return false;
                }
                catch (UnauthorizedAccessException e)
                {
                    hangmanView.PrintMessage(e.Message);
                    return false;
                }
                catch (PathTooLongException e)
                {
                    hangmanView.PrintMessage(e.Message);
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Déserialise le fichier de mots
        /// </summary>
        /// <param name="_path">string : le fichier à déserialiser</param>
        public void Deserialize(string _path)
        {
            int numberWords = hangmanModel.Deserialize(_path);
            hangmanView.PrintMessage($"{numberWords} mots chargés avec succès.");
        }

        /// <summary>
        /// Serialise le fichier de mots
        /// </summary>
        /// <param name="_path">string : le fichier à serialiser</param>
        public void Serialize(string _path)
        {
            if (hangmanModel.Serialize(_path))
            {
                hangmanView.PrintMessage("Sauvegarde réussie.");
            }
        }
    }
}