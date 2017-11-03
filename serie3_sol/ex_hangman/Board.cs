using System;

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

        //controller instance never change (readonly)
        private readonly HangmanController hangmanController;

        public Board()
        {
            hangmanController = new HangmanController();
        }

        public void LoopGame()
        {
            EnumChoice choice = EnumChoice.None;
            while (choice != EnumChoice.Exit)
            {
                choice = EnumChoice.None;
                hangmanController.DisplayOptions();
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    Console.Clear();
                    if (Enum.IsDefined(typeof(EnumChoice), number))
                    {
                        choice = (EnumChoice) number;
                        switch (choice)
                        {
                            case EnumChoice.ChooseFile:
                                ChooseFileOption();
                                break;
                            case EnumChoice.Play:
                                PlayOption();
                                break;
                            case EnumChoice.DisplayScores:
                                DisplayScoresOption();
                                break;
                            case EnumChoice.AddNewWord:
                                hangmanController.AddNewWord();
                                break;
                            case EnumChoice.SaveFile:
                                SaveFile();
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

        private void SaveFile()
        {
            string pathSaveFile = hangmanController.ChoosePathFile(false);
            if (pathSaveFile != null)
            {
                hangmanController.Serialize(pathSaveFile);
            }
            else
            {
                Console.WriteLine("Le chemin spécifié contient une erreur!");
            }
        }

        private void DisplayScoresOption()
        {
            if (hangmanController.IsFileOpen())
            {
                hangmanController.DisplayScores();
            }
            else
            {
                Console.Write("Aucun fichier de mots n'est ouvert!");
            }
        }

        private void ChooseFileOption()
        {
            string pathLoadFile = hangmanController.ChoosePathFile();
            if (pathLoadFile != null)
            {
                hangmanController.Deserialize(pathLoadFile);
            }
            else
            {
                Console.WriteLine("Le chemin spécifié contient une erreur!");
            }
        }

        private void PlayOption()
        {
            if (hangmanController.IsFileOpen())
            {
                bool findWord = hangmanController.RandomWord();
                if (findWord)
                {
                    while (!hangmanController.Play())
                    {
                    }
                    hangmanController.End();
                }
                else
                {
                    Console.WriteLine("Aucun mot n'est présent dans la base de données.");
                }
            }
            else
            {
                Console.Write("Aucun fichier de mots n'est ouvert!");
            }
        }

        public static void Main(string[] _args)
        {
            var prog = new Board();
            prog.LoopGame();
        }
    }
}