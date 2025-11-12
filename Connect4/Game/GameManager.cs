using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Game
{
    public class GameManager
    {
        public int Red { get; } = 1;

        public int Yellow { get; } = 2;

        public int CurrentPlayer { get; set; }

        public int Rows { get; set; } = 6;
        public int Columns { get; set; } = 7;

        public GameManager()
        {
            CurrentPlayer = Red;
        }

        public void SwitchPlayerTurn()
        {
            CurrentPlayer = CurrentPlayer == Red ? Yellow : Red;
        }
    }
}
