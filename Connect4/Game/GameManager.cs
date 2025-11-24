using Connect4.Bot;
using Connect4.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Connect4.Game
{
    public class GameManager
    {
        public int Depth { get; set; } = 10;

        public bool GameEnded { get; set; } = false;
        public int Red { get; } = 1;

        public int Yellow { get; } = 2;

        public int CurrentPlayer { get; set; }

        public int Rows { get; set; } = 6;
        public int Columns { get; set; } = 7;

        public Board Board { get; set; }
        public BoardDisplay BoardDisplay { get; set; }

        public bool IsPlayingAgainstBot { get; set; } = false;

        public bool IsProcessingTurn { get; set; } = false;

        public Connect4Bot Connect4Bot { get; set; }

        public BoardMenu BoardMenu { get; set; }

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
            CurrentPlayer = Red;
            BoardMenu.UpdatePlayerCircle();
            BoardMenu.UpdateTextBlock("Current player: ");
            IsProcessingTurn = false;
            GameEnded = false;
        }

        public async Task HandleTurn(int row, int column, bool isBotTurn)
        {
            if (IsProcessingTurn && !isBotTurn)
            {
                return;
            }
            IsProcessingTurn = true;
           
            int player = CurrentPlayer;
            Board.UpdateBoard(row, column, player);
            BoardDisplay.UpdateCellUI(row, column, player);
            if (Board.IsBoardFull() == true)
            {
                GameEnded = true;
            }
            CheckAllDirections(Board.GameBoard, row, column);

            if (GameEnded)
            {
                BoardMenu.UpdateTextBlock("Winner: ");
                await Task.Delay(2000);
                ResetGame();
                return;
            }
            SwitchPlayerTurn();
            BoardMenu.UpdatePlayerCircle();
            await Task.Delay(100);

            //IsBotTurn = (IsPlayingAgainstBot && CurrentPlayer == Yellow);

            if (IsPlayingAgainstBot == true && CurrentPlayer == Yellow)
            {
                var result = Connect4Bot.MiniMax(Board.GameBoard, Depth, Double.NegativeInfinity, Double.PositiveInfinity, true);
                row = Board.CalculateLowestCell(Board.GameBoard, result.column);
                await HandleTurn(row, result.column, true);
            }
            IsProcessingTurn = false;
        }

        /// <summary>
        /// Checks all the directions of the placed cell and check the results
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

        public void SetGameMode(Button button)
        {
            if (button.Content.ToString() == "Bot")
            {
                IsPlayingAgainstBot = true;
            }
            else
            {
                IsPlayingAgainstBot = false;
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
