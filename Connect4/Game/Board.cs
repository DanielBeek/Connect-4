using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public GameManager GameManager { get; set; }

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

        /// <summary>
        /// Updates the int board with <see cref="GameManager.CurrentPlayer"/>
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="currentPlayer"></param>
        public void UpdateBoard(int row, int col, int currentPlayer)
        {
            GameBoard[row, col] = currentPlayer;
        }


        /// <summary>
        /// Checks if the int board is full
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Calculates the lowest cell of the given column
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public int CalculateLowestCell(int[,] board, int col)
        {
            for (int row = Rows - 1; row >= 0; row--)
            {
                if (board[row, col] == 0)
                {
                    return row;
                }
            }
            return -1;
        }

        public void PrintBoard(int[,] board)
        {
            for (int row = 0; row < GameManager.Rows; row++)
            {
                string rowString = "";
                for (int column = 0; column < GameManager.Columns; column++)
                {
                    rowString += board[row, column] + " ";
                }
                Debug.WriteLine(rowString);
            }
        }
    }
}
