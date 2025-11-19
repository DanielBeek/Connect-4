using Connect4.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Connect4.Bot
{
    public class Connect4Bot
    {
        private readonly GameManager _gameManager;
        private readonly Board _board;

        public Connect4Bot(GameManager gameManager, Board board) { 
            _gameManager = gameManager;
            _board = board;
        }    
        private bool MiniMaxGameOver { get; set; } = false;

        public (double score, int column) MiniMax(int[,] board, int depth, double alpha, double beta, bool isMaximizingPlayer)
        {
            if (depth == 0 || MiniMaxGameOver == true)
            {
                MiniMaxGameOver = false;
                return (EvaluateBoard(board), -1);
            }

            if (isMaximizingPlayer)
            {
                double maxEval = double.NegativeInfinity;
                int bestCol = -1;

                for (int col = 0; col < _gameManager.Columns; col++)
                {
                    int row = _board.CalculateLowestCell(board, col);
                    if (row == -1)
                    {
                        continue;
                    }

                    board[row, col] = _gameManager.Yellow;

                    var (eval, _) = MiniMax(board, depth - 1, alpha, beta, false);
                    board[row, col] = 0; // undo move
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        bestCol = col;
                    }
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return (maxEval, bestCol);
            }
            else
            {
                double minEval = double.PositiveInfinity;
                int bestCol = -1;

                for (int col = 0; col < _gameManager.Columns; col++)
                {
                    int row = _board.CalculateLowestCell(board, col);
                    if (row == -1)
                    {
                        continue;
                    }

                    board[row, col] = _gameManager.Red;

                    var (eval, _) = MiniMax(board, depth - 1, alpha, beta, true);
                    board[row, col] = 0; // undo move
                    if (eval < minEval)
                    {
                        minEval = eval;
                        bestCol = col;
                    }
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return (minEval, bestCol);
            }
        }

        public int EvaluateBoard(int[,] board)
        {
            int score = 0;

            for (int row = 0; row < _gameManager.Rows; row++)
            {
                for (int col = 0; col < _gameManager.Columns; col++)
                {
                    int cell = board[row, col];
                    if (cell == 0)
                    {
                        continue;
                    }
                    if (cell == _gameManager.Red)
                    {
                        // check directions for Red
                        score -= EvaluatePosition(_gameManager.CheckHorizontal(board, row, col));
                        score -= EvaluatePosition(_gameManager.CheckVertical(board, row, col));
                        score -= EvaluatePosition(_gameManager.CheckPrimaryDiagonal(board, row, col));
                        score -= EvaluatePosition(_gameManager.CheckSecondaryDiagonal(board, row, col));
                    }
                    else if (cell == _gameManager.Yellow)
                    {
                        // check directions for Yellow
                        score += EvaluatePosition(_gameManager.CheckHorizontal(board, row, col));
                        score += EvaluatePosition(_gameManager.CheckVertical(board, row, col));
                        score += EvaluatePosition(_gameManager.CheckPrimaryDiagonal(board, row, col));
                        score += EvaluatePosition(_gameManager.CheckSecondaryDiagonal(board, row, col));
                    }
                }
            }
            return score;
        }

        public int EvaluatePosition(int length)
        {
            switch (length)
            {
                case 1: return 1;
                case 2: return 100;
                case 3: return 10000;
                case 4: return 1000000;
                default: return 0;
            }
        }
    }
}
