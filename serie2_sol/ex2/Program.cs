using System;

namespace ex2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BoardGame boardGame = new BoardGame();

            Console.WriteLine("THE BOARD");
            Console.WriteLine("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            for (char c = 'A'; c < 'Z'; c++)
            {
                if (boardGame[c])
                {
                    Console.Write("X");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine("\nTHE PAWNS");
            foreach (string str in boardGame)
            {
                Console.Write($"{str}, ");
            }/*
            for (int i = 1; i <= boardGame.PawnNumber; i++)
            {
                Console.Write($"{i} : {boardGame[i]}");
                if (i < boardGame.PawnNumber)
                {
                    Console.Write(", ");
                }
            }*/
            Console.ReadLine();
        }
    }
}