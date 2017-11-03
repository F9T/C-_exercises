using System;
using System.IO;
using System.Net.Mime;

namespace ex_hangman
{
    public class Board
    {
        private enum EnumChoice
        {
            ChooseFile = 1,
            Play = 2,
            DisplayScores = 3,
            AddNewWord = 4,
            SaveFile = 5,
            Exit = 6,
            None
        }

        private HangmanController hangmanController;

        public Board()
        {
            this.hangmanController = new HangmanController();
        }

        public void LoopGame()
        {
            EnumChoice choice = EnumChoice.None;
            while (choice != EnumChoice.Exit)
            {
                choice = EnumChoice.None;
                PrintOptions();
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    Console.Clear();
                    if (Enum.IsDefined(typeof(EnumChoice), number))
                    {
                        choice = (EnumChoice) number;
                        switch (choice)
                        {
                            case EnumChoice.ChooseFile:
                                string pathLoadFile = ChoosePathFile();
                                if (pathLoadFile != null)
                                {
                                    int numberWords = hangmanController.Deserialize(pathLoadFile);
                                    Console.WriteLine($"{numberWords} mots chargés avec succès.");
                                }
                                else
                                {
                                    Console.WriteLine("Le chemin spécifié contient une erreur!");
                                }
                                break;
                            case EnumChoice.Play:
                                if (hangmanController.IsFileOpen())
                                {
                                    hangmanController.RandomWord();
                                    while (!hangmanController.Play()) {}
                                    hangmanController.End();
                                }
                                else
                                {
                                    Console.Write("Aucun fichier de mots n'est ouvert!");
                                }
                                break;
                            case EnumChoice.DisplayScores:
                                if (hangmanController.IsFileOpen())
                                {
                                    hangmanController.DisplayScores();
                                }
                                else
                                {
                                    Console.Write("Aucun fichier de mots n'est ouvert!");
                                }
                                break;
                            case EnumChoice.AddNewWord:
                                Console.Write("Entrer votre nouveau mot (ne pas mettre d'accent) : ");
                                string newWord = Console.ReadLine();
                                Console.WriteLine(hangmanController.AddNewWord(newWord)
                                    ? $"Le mot {newWord} a été ajouté."
                                    : $"Le mot {newWord} contient une erreur!");
                                break;
                            case EnumChoice.SaveFile:
                                string pathSaveFile = ChoosePathFile();
                                if (pathSaveFile != null)
                                {
                                    hangmanController.Serialize(pathSaveFile);
                                }
                                else
                                {
                                    Console.WriteLine("Le chemin spécifié contient une erreur!");
                                }
                                break;
                            case EnumChoice.Exit:
                                Environment.Exit(0);
                                break;
                        }
                        Console.WriteLine();
                    }
                }
            }
            Console.ReadLine();
        }

        private void PrintOptions()
        {
            Console.WriteLine("Vos options sont :");
            Console.WriteLine("1) Choisir un fichier de mots");
            Console.WriteLine("2) Jouer (trouver un mot)");
            Console.WriteLine("3) Afficher les scores");
            Console.WriteLine("4) Ajouter un nouveau mot");
            Console.WriteLine("5) Sauvegarder le fichier de mots");
            Console.WriteLine("6) Terminer");
            Console.Write("Choix : ");
        }

        private string ChoosePathFile(string _path="")
        {
            Console.Write($"Donner le chemin du fichier (.ser) : {_path}");
            string path = Console.ReadLine();
            return CheckFile(path) ? path : null;
        }

        private bool CheckFile(string _path)
        {
            FileInfo file = new FileInfo(_path);
            return file.Exists && file.Extension.ToLower() == ".ser";
        }

        public static void Main(string[] _args)
        {
            var prog = new Board();
            prog.LoopGame();
        }
    }
}