using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Connect4.Game
{
    public class Board
    {
        public int Rows { get; }
        public int Columns { get; }
        public int[,] GameBoard { get; set; }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            CreateEmptyBoard();
        }

        /// <summary>
        /// Creates an empty int board that is used to keep track of player moves
        /// </summary>
        public void CreateEmptyBoard()
        {
            GameBoard = new int[Rows, Columns];
        }

        public void UpdateBoard(int row, int col, int currentPlayer)
        {
            GameBoard[row, col] = currentPlayer;
        }

        public bool IsBoardFull()
        {
            foreach (var cellValue in GameBoard)
            {
                if (cellValue == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public int CalculateLowestCell(int col)
        {
            for (int row = Rows - 1; row >= 0; row--)
            {
                if (GameBoard[row, col] == 0)
                {
                    return row;
                }
            }
            return -1;
        }
    }
}
