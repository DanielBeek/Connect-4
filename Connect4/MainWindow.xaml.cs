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
using Connect4.Bot;
using Connect4.Game;
using Connect4.UI;

namespace Connect4
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameManager _gameManager;
        private readonly BoardDisplay _boardDisplay;
        private readonly Board _board;
        private readonly BoardMenu _boardMenu;
        private readonly Connect4Bot _connect4Bot;

        public MainWindow()
        {
            InitializeComponent();
            _gameManager = new GameManager();

            _board = _gameManager.Board;
            _board.GameManager = _gameManager;

            _boardDisplay = new BoardDisplay(GameGrid, ClickedCell, _gameManager);
            _gameManager.BoardDisplay = _boardDisplay;
            _boardDisplay.CreateGrid();
            _boardDisplay.FillGrid();

            _boardMenu = new BoardMenu(GameMenu, _gameManager, SwitchGameMode);
            _gameManager.BoardMenu = _boardMenu;

            _connect4Bot = new Connect4Bot(_gameManager, _board);
            _gameManager.Connect4Bot = _connect4Bot;

            Window.Background = new SolidColorBrush(Color.FromRgb(61, 59, 60));
            GameGrid.Background = new SolidColorBrush(Color.FromRgb(61, 59, 60));
            GameGrid.Margin = new Thickness(_boardDisplay.CellMargin);
            this.ResizeMode = ResizeMode.NoResize;
            this.SizeToContent = SizeToContent.WidthAndHeight;

        }

        public async void ClickedCell(object sender, MouseButtonEventArgs e)
        {
            Border clickedCell = sender as Border;
            if (clickedCell != null)
            {
                int column = Grid.GetColumn(clickedCell);
                int row = _board.CalculateLowestCell(_board.GameBoard, column);
                if (row > -1)
                {
                    await _gameManager.HandleTurn(row, column, false);
                    //_menu.UpdatePlayerCircle();
                }
            }
        }

        public void SwitchGameMode(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                _boardMenu.SwitchButtonText(clickedButton);
                _gameManager.SetGameMode(clickedButton);
                _gameManager.ResetGame();
            }
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
    }
}