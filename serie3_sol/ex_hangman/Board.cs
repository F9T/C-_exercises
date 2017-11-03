﻿using System;
using System.IO;

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
                if (Int32.TryParse(Console.ReadLine(), out int number))
                {
                    Console.WriteLine();
                    if (Enum.IsDefined(typeof(EnumChoice), number))
                    {
                        choice = (EnumChoice) number;
                        switch (choice)
                        {
                            case EnumChoice.ChooseFile:
                                string pathFile = ChoosePathFile();
                                if (pathFile != null)
                                {
                                    int numberWords = hangmanController.Deserialize(pathFile);
                                    Console.WriteLine($"{numberWords} chargés avec succès");
                                }
                                else
                                {
                                    Console.WriteLine("Le chemin spécifié contient une erreur!");
                                }
                                break;
                            case EnumChoice.Play:
                                if (hangmanController.LetsPlay())
                                {
                                    hangmanController.Play();
                                }
                                else
                                {
                                    goto case EnumChoice.ChooseFile;
                                }
                                break;
                            case EnumChoice.DisplayScores:

                                break;
                            case EnumChoice.AddNewWord:

                                break;
                            case EnumChoice.SaveFile:

                                break;
                            case EnumChoice.Exit:

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
            Console.WriteLine("4) Terminer");
            Console.Write("Choix : ");
        }

        private string ChoosePathFile()
        {
            Console.Write("Donner le nom d'un fichier (.ser) : ");
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