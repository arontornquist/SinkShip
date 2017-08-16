using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinkShip
{
    class GameBoard
    {
        public int[,] Board { get; private set; }


        public GameBoard(int x, int y)
        {
            Board = new int[x, y];
        }
    }
}
