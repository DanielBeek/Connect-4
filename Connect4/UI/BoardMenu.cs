using Connect4.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Connect4.UI
{
    public class BoardMenu
    {
        private int BoardMenuHeigth { get; } = 50;

        private Ellipse _currentPlayerCircle;

        private TextBlock _textBlock;

        private readonly Grid _boardMenu;

        private readonly GameManager _gameManager;

        private RoutedEventHandler _gameModeButton;

        public BoardMenu(Grid boardMenu, GameManager gameManager, RoutedEventHandler gamemodebutton)
        {
            _boardMenu = boardMenu;
            _gameManager = gameManager;
            _boardMenu.Width = Double.NaN;
            _boardMenu.Height = BoardMenuHeigth;
            _boardMenu.HorizontalAlignment = HorizontalAlignment.Stretch;
            _gameModeButton = gamemodebutton;
           
            _boardMenu.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) }); // spacing
            _boardMenu.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // item 1
            _boardMenu.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // item 2
            _boardMenu.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // space that pushes right side
            _boardMenu.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // item 3
            _boardMenu.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) }); // right spacing


            PlayerText();
            CurrentPlayerCircle();
            GameSwitchButton();
           
        }

        public void PlayerText()
        {
            _textBlock = new TextBlock
            {
                FontSize = 24,
                Text = "Current player: ",
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
            };
          
            Grid.SetColumn(_textBlock, 1);
            _boardMenu.Children.Add(_textBlock);
        }

        public void CurrentPlayerCircle()
        {
            _currentPlayerCircle = new Ellipse
            {
                Fill = _gameManager.CurrentPlayer == _gameManager.Red ? Brushes.Red : Brushes.Yellow,
                Width = 40,
                Height = 40,
                VerticalAlignment = VerticalAlignment.Center,
            };
            Grid.SetColumn(_currentPlayerCircle, 2);
            _boardMenu.Children.Add(_currentPlayerCircle);
        }

        public void UpdatePlayerCircle()
        {
            _currentPlayerCircle.Fill = _gameManager.CurrentPlayer == _gameManager.Red ? Brushes.Red : Brushes.Yellow;
        }

        public void UpdateTextBlock(string text)
        {
            _textBlock.Text = text;
        }

        public void GameSwitchButton()
        {
            var button = new Button
            {
                Content = "Player",
                Width = 100,
                Height = 25,
                Background = new SolidColorBrush(Color.FromRgb(61, 59, 60)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(31, 30, 30)),
                BorderThickness = new Thickness(2),
                Foreground = new SolidColorBrush(Color.FromRgb(31, 30, 30)),
                FontSize = 16,
                OverridesDefaultStyle = true
            };
            button.Click += _gameModeButton;
            Grid.SetColumn(button, 4);
            _boardMenu.Children.Add(button);
        }

        public void SwitchButtonText(Button button)
        {
            if (button.Content.ToString() == "Bot")
            {
                button.Content = "Player";
            }
            else
            {
                button.Content = "Bot";
            }
        }
    }

    
}
