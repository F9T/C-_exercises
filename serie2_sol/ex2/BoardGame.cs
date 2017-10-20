using System;
using System.Collections;
using System.Collections.Generic;

namespace ex2
{
    public class BoardGame : IEnumerable<string>
    {
        private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private int[] board;
        private int slotNumber, pawnNumber;

        public BoardGame()
        {
            this.slotNumber = 26;
            this.board = new int[slotNumber];
            this.pawnNumber = 6;
            this.Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = 0;
            }
            for (int i = 1; i <= pawnNumber;)
            {
                int random = new Random().Next(0, board.Length);
                if (board[random] == 0)
                    board[random] = i++;
            }
        }

        public bool this[char c]
        {
            get
            {
                int index = alphabet.IndexOf(c);
                if (index >= 0)
                    return board[index] != 0;
                return false;
            }
        }

        public char this[int number]
        {
            get
            {
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == number) return alphabet[i];
                }
                return '-';
            }
        }

        public int SlotNumber
        {
            get { return slotNumber; }
        }

        public int PawnNumber
        {
            get { return pawnNumber; }
        }

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 1; i <= pawnNumber; i++)
            {
                yield return i + ":" + this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}