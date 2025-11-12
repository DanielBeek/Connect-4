using System.Data.Common;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Connect4.Game;
using Connect4.UI;

namespace Connect4
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double CellMargin { get; set; } = 5;
        private bool GameEnded { get; set; } = false;

        private Random Random = new Random();

        private int Count = 0;

        private bool MiniMaxGameOver { get; set; } = false;

        private readonly GameManager _gameManager;
        private readonly BoardDisplay _boardDisplay;
        private readonly Board _board;

        public MainWindow()
        {
            InitializeComponent();
            _gameManager = new GameManager();

            _board = new Board(_gameManager.Rows, _gameManager.Columns);

            _boardDisplay = new BoardDisplay(GameGrid, ClickedCell, _gameManager);
            _boardDisplay.CreateGrid();
            _boardDisplay.FillGrid();
           
            Window.Background = new SolidColorBrush(Color.FromRgb(61, 59, 60));
            GameGrid.Background = new SolidColorBrush(Color.FromRgb(61, 59, 60));
            GameGrid.Margin = new Thickness(CellMargin);
            this.ResizeMode = ResizeMode.NoResize;
            this.SizeToContent = SizeToContent.WidthAndHeight;
            
        }

        //public async void CreateGame()
        //{
        //    ClearGrid();
        //    CurrentPlayer = Red;
        //    GameEnded = false;
        //    Cells = new Border[Rows, Columns];
        //    CreateGrid();
        //    FillGrid();
        //    CreateEmptyBoard();

        //    // used for bot vs bot
        //    //await RandomColumn();
        //}

        //public void ClearGrid()
        //{
        //    GameGrid.Children.Clear();
        //    GameGrid.RowDefinitions.Clear();
        //    GameGrid.ColumnDefinitions.Clear();
        //}

        /// <summary>
        /// Creates an empty int board that is used to keep track of player moves
        /// </summary>
        //public void CreateEmptyBoard()
        //{
        //    GameBoard = new int[Rows, Columns];
        //}

        ///// <summary>
        ///// Creates a new connect 4 grid.
        ///// </summary>
        //public void CreateGrid()
        //{
        //    // Add rows
        //    for (int r = 0; r < Rows; r++)
        //    {
        //        GameGrid.RowDefinitions.Add(new RowDefinition());

        //    }
        //    // Add columns
        //    for (int c = 0; c < Columns; c++)
        //    {
        //        GameGrid.ColumnDefinitions.Add(new ColumnDefinition());

        //    }
        //}

        ///// <summary>
        ///// Fills the grid with cells and circles.
        ///// </summary>
        //public void FillGrid()
        //{
        //    for (int row = 0; row < Rows; row++)
        //    {
        //        for (int col = 0; col < Columns; col++)
        //        {
        //            var newCell = CreateCell();
        //            var newCircle = CreateCircle();
        //            // Put circle inside border
        //            newCell.Child = newCircle;

        //            // cell click event
        //            newCell.MouseLeftButtonUp += ClickedCell;

        //            // add cell to list
        //            Cells[row, col] = newCell;

        //            // Position in grid
        //            Grid.SetRow(newCell, row);
        //            Grid.SetColumn(newCell, col);

        //            GameGrid.Children.Add(newCell);
        //        }
        //    }
        //}

        public async void ClickedCell(object sender, MouseButtonEventArgs e)
        {
            Border clickedCell = sender as Border;
            if (clickedCell != null)
            {
                //int row = Grid.GetRow(clickedCell);
                int column = Grid.GetColumn(clickedCell);
                int row = _board.CalculateLowestCell(column);
                //if (row != -1 && _gameManager.CurrentPlayer == _gameManager.Red)
                //{
                    await HandleTurn(row, column);
                //}
            }
        }

        public async Task HandleTurn(int row, int column)
        {
            int player = _gameManager.CurrentPlayer;
            Debug.WriteLine(player);
            _board.UpdateBoard(row, column, player);
            _boardDisplay.UpdateCellUI(row, column, player);

            PrintBoard(_board.GameBoard);
            ////CheckGameOver(CheckHorizontal(GameBoard, row, column));
            ////CheckGameOver(CheckVertical(GameBoard, row, column));
            ////CheckGameOver(CheckPrimaryDiagonal(GameBoard, row, column));
            ////CheckGameOver(CheckSecondaryDiagonal(GameBoard, row, column));
            //if (_board.IsBoardFull() == true)
            //{
            //    GameEnded = true;
            //}
            //if (GameEnded)
            //{
            //    //await Task.Delay(3500);
            //    //CreateGame();
            //    return;
            //}
            _gameManager.SwitchPlayerTurn();
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

        //public async Task RandomColumn()
        //{
        //    while (true)
        //    {
        //        var randomColumn = Random.Next(0, 7);
        //        int row = CalculateLowestCell(randomColumn);
        //        if (row != -1)
        //        {
        //            await HandleTurn(row, randomColumn);
        //            break;
        //        }
        //    }
        //}

        //public bool IsBoardFull()
        //{
        //    foreach (var cellValue in GameBoard)
        //    {
        //        if (cellValue == 0)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        //public int CalculateLowestCell(int col)
        //{
        //    for (int row = Rows - 1; row >= 0; row--)
        //    {
        //        if (GameBoard[row, col] == 0)
        //        {
        //            return row;
        //        }
        //    }
        //    return -1;
        //}

        //public void CheckGameOver(int length)
        //{
        //    if (!GameEnded && length >= 4)
        //    {
        //        GameEnded = true;
        //    }
        //}



        //public void UpdateBoard(int row, int col)
        //{
        //    GameBoard[row, col] = CurrentPlayer;
        //}

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

        public void PrintBoard(int[,] board)
        {
            for (int row = 0; row < _gameManager.Rows; row++)
            {
                string rowString = "";
                for (int column = 0; column < _gameManager.Columns; column++)
                {
                    rowString += board[row, column] + " ";
                }
                Debug.WriteLine(rowString);
            }
        }

        ///// <summary>
        ///// Create a new cell for the grid.
        ///// </summary>
        ///// <returns>a new cell with size, background and margin</returns>
        //public Border CreateCell()
        //{
        //    var cell = new Border();
        //    cell.Background = new SolidColorBrush(Color.FromRgb(31, 30, 30));
        //    cell.Margin = new Thickness(CellMargin);
        //    cell.Width = CellSize;
        //    cell.Height = CellSize;
        //    return cell;
        //}
        ///// <summary>
        ///// Create a new circle for the cell.
        ///// </summary>
        ///// <returns>a new Circle with size, background and margin</returns>
        //public Ellipse CreateCircle()
        //{
        //    var circle = new Ellipse();
        //    circle.Fill = new SolidColorBrush(Color.FromRgb(61, 59, 60));
        //    circle.Width = CellSize * 0.9;
        //    circle.Height = CellSize * 0.9;
        //    return circle;
        //}

        //    public int CheckHorizontal(int[,] board, int row, int col)
        //    {
        //        var target = board[row, col];
        //        int length = 1;
        //        // count left
        //        for (int i = col - 1; i >= 0; i--)
        //        {
        //            if (board[row, i] != target)
        //            {
        //                break;
        //            }
        //            length++;
        //        }
        //        // count right
        //        for (int i = col + 1; i < Columns; i++)
        //        {
        //            if (board[row, i] != target)
        //            {
        //                break;
        //            }
        //            length++;
        //        }
        //        return length;
        //    }

        //    public int CheckVertical(int[,] board, int row, int col)
        //    {
        //        var target = board[row, col];
        //        var length = 1;
        //        for (int i = row + 1; i < Rows; i++)
        //        {
        //            if (board[i, col] != target)
        //            {
        //                break;
        //            }
        //            length += 1;
        //        }
        //        return length;
        //    }

        //    public int CheckPrimaryDiagonal(int[,] board, int row, int col)
        //    {
        //        var target = board[row, col];
        //        var length = 1;
        //        for (int x = row - 1, y = col - 1; x >= 0 && y >= 0; x--, y--)
        //        {
        //            if (board[x, y] != target)
        //            {
        //                break;
        //            }
        //            length += 1;
        //        }
        //        for (int x = row + 1, y = col + 1; x < Rows && y < Columns; x++, y++)
        //        {
        //            if (board[x, y] != target)
        //            {
        //                break;
        //            }
        //            length += 1;
        //        }
        //        return length;
        //    }

        //    public int CheckSecondaryDiagonal(int[,] board, int row, int col)
        //    {
        //        var target = board[row, col];
        //        var length = 1;
        //        for (int x = row + 1, y = col - 1; x < Rows && y >= 0; x++, y--)
        //        {
        //            if (board[x, y] != target)
        //            {
        //                break;
        //            }
        //            length += 1;
        //        }
        //        for (int x = row - 1, y = col + 1; x >= 0 && y < Columns; x--, y++)
        //        {
        //            if (board[x, y] != target)
        //            {
        //                break;
        //            }
        //            length += 1;
        //        }
        //        return length;
        //    }
    }
}