using Connect4.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Game
{
    public class GameManager
    {
        public Boolean GameEnded { get; set; } = false;
        public int Red { get; } = 1;

        public int Yellow { get; } = 2;

        public int CurrentPlayer { get; set; }

        public int Rows { get; set; } = 6;
        public int Columns { get; set; } = 7;

        public Board Board { get; set; }
        public BoardDisplay BoardDisplay { get; set; }

        public GameManager()
        {
            Board = new Board(Rows, Columns);
            CurrentPlayer = Red;
        }

        /// <summary>
        /// Switches the player turn if red > yellow & if yellow > red
        /// </summary>
        public void SwitchPlayerTurn()
        {
            CurrentPlayer = CurrentPlayer == Red ? Yellow : Red;
        }

        /// <summary>
        /// Resets the game if <see cref="GameEnded"/> = true
        /// </summary>
        public void ResetGame()
        {
            Board.CreateEmptyBoard();
            BoardDisplay.ClearGrid();
            BoardDisplay.CreateGrid();
            BoardDisplay.FillGrid();
            SwitchPlayerTurn();
            GameEnded = false;
        }

        public async Task HandleTurn(int row, int column)
        {
            int player = CurrentPlayer;
            Board.UpdateBoard(row, column, player);
            BoardDisplay.UpdateCellUI(row, column, player);
            if (Board.IsBoardFull() == true)
            {
                GameEnded = true;
            }
            //PrintBoard(_board.GameBoard);
            CheckAllDirections(Board.GameBoard, row, column);

            if (GameEnded)
            {
                ResetGame();
                return;
            }
            SwitchPlayerTurn();
            ////if (CurrentPlayer == Yellow)
            ////{
            ////    //await Task.Delay(1);
            ////    //await RandomColumn();
            ////    Count = 0;
            ////    var result = MiniMax(GameBoard, 7, Double.NegativeInfinity, Double.PositiveInfinity, true);
            ////    Debug.WriteLine(result);
            ////    row = _board.CalculateLowestCell(result.column);
            ////    await HandleTurn(row, result.column);
            ////}
        }

        /// <summary>
        /// Checks all the directions of the placed cell
        /// </summary>
        /// <param name="gameBoard"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void CheckAllDirections(int[,] gameBoard, int row, int column)
        {
            int[] results = new int[]
            {
                CheckHorizontal(gameBoard, row, column),
                CheckVertical(gameBoard, row, column),
                CheckPrimaryDiagonal(gameBoard, row, column),
                CheckSecondaryDiagonal(gameBoard, row, column)
            };

            foreach (var result in results)
            {
                CheckGameOver(result);
            }
        }

        /// <summary>
        /// Checks if a player has connect 4
        /// </summary>
        /// <param name="length"></param>
        public void CheckGameOver(int length)
        {
            if (!GameEnded && length >= 4)
            {
                GameEnded = true;
            }
        }

        public int CheckHorizontal(int[,] board, int row, int col)
        {
            var target = board[row, col];
            int length = 1;
            // count left
            for (int i = col - 1; i >= 0; i--)
            {
                if (board[row, i] != target)
                {
                    break;
                }
                length++;
            }
            // count right
            for (int i = col + 1; i < Columns; i++)
            {
                if (board[row, i] != target)
                {
                    break;
                }
                length++;
            }
            return length;
        }

        public int CheckVertical(int[,] board, int row, int col)
        {
            var target = board[row, col];
            var length = 1;
            for (int i = row + 1; i < Rows; i++)
            {
                if (board[i, col] != target)
                {
                    break;
                }
                length += 1;
            }
            return length;
        }

        public int CheckPrimaryDiagonal(int[,] board, int row, int col)
        {
            var target = board[row, col];
            var length = 1;
            for (int x = row - 1, y = col - 1; x >= 0 && y >= 0; x--, y--)
            {
                if (board[x, y] != target)
                {
                    break;
                }
                length += 1;
            }
            for (int x = row + 1, y = col + 1; x < Rows && y < Columns; x++, y++)
            {
                if (board[x, y] != target)
                {
                    break;
                }
                length += 1;
            }
            return length;
        }

        public int CheckSecondaryDiagonal(int[,] board, int row, int col)
        {
            var target = board[row, col];
            var length = 1;
            for (int x = row + 1, y = col - 1; x < Rows && y >= 0; x++, y--)
            {
                if (board[x, y] != target)
                {
                    break;
                }
                length += 1;
            }
            for (int x = row - 1, y = col + 1; x >= 0 && y < Columns; x--, y++)
            {
                if (board[x, y] != target)
                {
                    break;
                }
                length += 1;
            }
            return length;
        }
    }
}
