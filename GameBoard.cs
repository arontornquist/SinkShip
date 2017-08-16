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
            CreateShips(Board);
        }

        private void CreateShips(int[,] board)
        {
            for (int i = 0; i < 4; i++)//TODO: Gör klart metoden!
            {
                
                for (int j = 0; j < 4; j++)
                {
                    board[i, j] = 0;
                    
                }
                Console.WriteLine();
            }
            
        }
    }
}
