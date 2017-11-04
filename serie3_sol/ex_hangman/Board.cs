using System;

namespace ex_hangman
{
    /// <summary>
    /// Cette classe s'occupe de lancer la boucle du jeu
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Enumeration des options possibles
        /// </summary>
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

        /// <summary>
        /// Boucle du jeu.
        /// </summary>
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
                else
                {
                    Console.Clear();
                    Console.WriteLine("Choix incorrect!\n");
                }
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Option de sauvegarde
        /// </summary>
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

        /// <summary>
        /// Option d'affichage des scores
        /// </summary>
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

        /// <summary>
        /// Option de chargement d'un fichier
        /// </summary>
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

        /// <summary>
        /// Option pour jouer une partie
        /// </summary>
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

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="_args">string[] : arguments command line</param>
        public static void Main(string[] _args)
        {
            var prog = new Board();
            prog.LoopGame();
        }
    }
}