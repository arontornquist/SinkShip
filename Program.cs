using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinkShip
{
    class Program
    {
        static void Main(string[] args)
        {
            StartGame();
        }

        private static void StartGame()
        {
            Interface ui = new Interface();
            int i = ui.A

            GameBoard g = new GameBoard(10,10);
            g.Print();
        }
    }
}
