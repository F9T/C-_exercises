using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ex_hangman
{
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

        public bool AddNewWord()
        {
            hangmanView.PrintMessage("Entrer votre nouveau mot (ne pas mettre d'accent) : ", false);
            string newWord = Console.ReadLine();
            bool success = IsAWord(newWord);
            hangmanView.PrintMessage(success
                ? $"Le mot {newWord} a été ajouté."
                : $"Le mot {newWord} contient une erreur!");
            if (success)
            {
                hangmanModel.AddNewWord(newWord);
                return true;
            }
            return false;
        }

        private static bool IsAWord(string _word)
        {
            return Regex.IsMatch(_word, @"^[A-Za-z]+$");
        }

        public bool IsFileOpen()
        {
            return hangmanModel.IsFileCreate();
        }

        public bool RandomWord()
        {
            if (hangmanModel.ContainsWords())
            {
                hangmanModel.GetRandomWord();
                return true;
            }
            return false;
        }

        public void DisplayOptions()
        {
            hangmanView.PrintOptions();
        }

        public bool Play()
        {
            hangmanView.PrintHangman(hangmanModel.CurrentPlayWord.Item2.ToString());
            string str = Console.ReadLine();
            if (str != null && str.Length == 1)
            {
                hangmanModel.InputLetter(str[0]);
            }
            return hangmanModel.WordFind;
        }

        public void End()
        {
            hangmanView.PrintMessage($"Mot : {hangmanModel.CurrentPlayWord.Item2}");
            hangmanView.PrintEnd(hangmanModel.CurrentPlayWord.Item1.ToString(), hangmanModel.CurrentCount,
                hangmanModel.IsNewRecord());
            hangmanModel.UpdateHangmanScore();
        }

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

        private static bool CheckFile(string _path)
        {
            if (!string.IsNullOrEmpty(_path))
            {
                FileInfo file = new FileInfo(_path);
                return file.Exists && file.Extension.ToLower() == ".ser";
            }
            return false;
        }

        public void Deserialize(string _path)
        {
            int numberWords = hangmanModel.Deserialize(_path);
            hangmanView.PrintMessage($"{numberWords} mots chargés avec succès.");
        }

        public void Serialize(string _path)
        {
            if (hangmanModel.Serialize(_path))
            {
                hangmanView.PrintMessage("Sauvegarde réussie.");
            }
        }
    }
}