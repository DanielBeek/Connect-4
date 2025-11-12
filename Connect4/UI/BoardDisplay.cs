using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Connect4.Game;

namespace Connect4.UI
{
    public class BoardDisplay
    {
        private readonly Grid _gameGrid;
        private readonly MouseButtonEventHandler _cellHandler;
        private readonly GameManager _gameManager;
        //public int Rows { get; set; }
        //public int Columns { get; set; }

        public int CellSize = 100;
        public double CellMargin { get; set; } = 5;

        //public int CurrentPlayer { get; set; }

        public Border[,] Cells { get; set; }

        public BoardDisplay(Grid gameGrid, MouseButtonEventHandler cellHandler, GameManager gameManager )
        {
            _gameGrid = gameGrid;
            _cellHandler = cellHandler;
            //Rows = rows;
            //Columns = columns;
            //CurrentPlayer = currentPlayer;
            _gameManager = gameManager;

            Cells = new Border[_gameManager.Rows, _gameManager.Columns];
        }

        /// <summary>
        /// Creates a new connect 4 grid.
        /// </summary>
        public void CreateGrid()
        {
            // Add rows
            for (int r = 0; r < _gameManager.Rows; r++)
            {
                _gameGrid.RowDefinitions.Add(new RowDefinition());

            }
            // Add columns
            for (int c = 0; c < _gameManager.Columns; c++)
            {
                _gameGrid.ColumnDefinitions.Add(new ColumnDefinition());

            }
        }

        /// <summary>
        /// Fills the grid with cells and circles.
        /// </summary>
        public void FillGrid()
        {
            for (int row = 0; row < _gameManager.Rows; row++)
            {
                for (int col = 0; col < _gameManager.Columns; col++)
                {
                    var newCell = CreateCell();
                    var newCircle = CreateCircle();
                    // Put circle inside border
                    newCell.Child = newCircle;

                    // cell click event
                    newCell.MouseLeftButtonUp += _cellHandler;

                    // add cell to list
                    Cells[row, col] = newCell;

                    // Position in grid
                    Grid.SetRow(newCell, row);
                    Grid.SetColumn(newCell, col);

                    _gameGrid.Children.Add(newCell);
                }
            }
        }

        /// <summary>
        /// Create a new cell for the grid.
        /// </summary>
        /// <returns>a new cell with size, background and margin</returns>
        public Border CreateCell()
        {
            var cell = new Border();
            cell.Background = new SolidColorBrush(Color.FromRgb(31, 30, 30));
            cell.Margin = new Thickness(CellMargin);
            cell.Width = CellSize;
            cell.Height = CellSize;
            return cell;
        }
        /// <summary>
        /// Create a new circle for the cell.
        /// </summary>
        /// <returns>a new Circle with size, background and margin</returns>
        public Ellipse CreateCircle()
        {
            var circle = new Ellipse();
            circle.Fill = new SolidColorBrush(Color.FromRgb(61, 59, 60));
            circle.Width = CellSize * 0.9;
            circle.Height = CellSize * 0.9;
            return circle;
        }

        public void UpdateCellUI(int row, int col, int player)
        {
            var cell = Cells[row, col];
            if (cell.Child is Ellipse circle)
            {
                circle.Fill = player == _gameManager.Red ? Brushes.Red : Brushes.Yellow;
            }
        }

        public void ClearGrid()
        {
            _gameGrid.Children.Clear();
            _gameGrid.RowDefinitions.Clear();
            _gameGrid.ColumnDefinitions.Clear();
        }
    }
}
