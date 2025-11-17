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
            _boardMenu.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            _boardMenu.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            _boardMenu.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            PlayerText();
            CurrentPlayerCircle();
            GameSwitchButton();
           
        }

        public void PlayerText()
        {
            var textblock = new TextBlock();
            textblock.FontSize = 24;
            textblock.Text = "Current player: ";
            textblock.Foreground = Brushes.White;
            textblock.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textblock, 0);
            _boardMenu.Children.Add(textblock);
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
            Grid.SetColumn(_currentPlayerCircle, 1);
            _boardMenu.Children.Add(_currentPlayerCircle);
        }

        public void UpdatePlayerCircle()
        {
            _currentPlayerCircle.Fill = _gameManager.CurrentPlayer == _gameManager.Red ? Brushes.Red : Brushes.Yellow;
        }

        public void GameSwitchButton()
        {
            var button = new Button
            {
                Content = "Bot",
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
            Grid.SetColumn(button, 2);
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
