using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Bot
{
    public class Bot
    {

        //private bool MiniMaxGameOver { get; set; } = false;

        //public (double score, int column) MiniMax(int[,] board, int depth, double alpha, double beta, bool isMaximizingPlayer)
        //{
        //    if (depth == 0 || MiniMaxGameOver == true)
        //    {
        //        MiniMaxGameOver = false;
        //        return (EvaluateBoard(board), -1); // -1 means "no move" at leaf
        //    }

        //    Count++;
        //    Debug.WriteLine(Count);

        //    if (isMaximizingPlayer)
        //    {
        //        double maxEval = double.NegativeInfinity;
        //        int bestCol = -1;

        //        for (int col = 0; col < Columns; col++)
        //        {
        //            int row = CalculateLowestCell(col);
        //            if (row == -1)
        //            {
        //                continue;
        //            }

        //            board[row, col] = Yellow;

        //            var (eval, _) = MiniMax(board, depth - 1, alpha, beta, false);
        //            board[row, col] = 0; // undo move
        //            if (eval > maxEval)
        //            {
        //                maxEval = eval;
        //                bestCol = col;
        //            }
        //            alpha = Math.Max(alpha, eval);
        //            if (beta <= alpha)
        //            {
        //                break;
        //            }
        //        }

        //        return (maxEval, bestCol);
        //    }
        //    else
        //    {
        //        double minEval = double.PositiveInfinity;
        //        int bestCol = -1;

        //        for (int col = 0; col < Columns; col++)
        //        {
        //            int row = CalculateLowestCell(col);
        //            if (row == -1)
        //            {
        //                continue;
        //            }

        //            board[row, col] = Red;

        //            var (eval, _) = MiniMax(board, depth - 1, alpha, beta, true);
        //            board[row, col] = 0; // undo move
        //            if (eval < minEval)
        //            {
        //                minEval = eval;
        //                bestCol = col;
        //            }
        //            beta = Math.Min(beta, eval);
        //            if (beta <= alpha)
        //            {
        //                break;
        //            }
        //        }
        //        return (minEval, bestCol);
        //    }
        //}

        //public int EvaluateBoard(int[,] board)
        //{
        //    int score = 0;

        //    for (int row = 0; row < Rows; row++)
        //    {
        //        for (int col = 0; col < Columns; col++)
        //        {
        //            int cell = board[row, col];
        //            if (cell == 0)
        //            {
        //                continue;
        //            }
        //            if (cell == Red)
        //            {
        //                // check directions for Red
        //                score -= EvaluatePosition(CheckHorizontal(board, row, col));
        //                score -= EvaluatePosition(CheckVertical(board, row, col));
        //                score -= EvaluatePosition(CheckPrimaryDiagonal(board, row, col));
        //                score -= EvaluatePosition(CheckSecondaryDiagonal(board, row, col));
        //            }
        //            else if (cell == Yellow)
        //            {
        //                // check directions for Yellow
        //                score += EvaluatePosition(CheckHorizontal(board, row, col));
        //                score += EvaluatePosition(CheckVertical(board, row, col));
        //                score += EvaluatePosition(CheckPrimaryDiagonal(board, row, col));
        //                score += EvaluatePosition(CheckSecondaryDiagonal(board, row, col));
        //            }
        //        }
        //    }
        //    return score;
        //}

        //public int EvaluatePosition(int length)
        //{
        //    switch (length)
        //    {
        //        case 1: return 1;
        //        case 2: return 10;
        //        case 3: return 100;
        //        case 4:
        //            MiniMaxGameOver = true;
        //            return 1000;
        //        default: return 0;
        //    }
        //}
    }
}
