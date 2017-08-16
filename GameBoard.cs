using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinkShip
{
    class GameBoard
    {
        public Random random = new Random();
        public int[,] Board { get; private set; }


        public GameBoard(int x, int y)
        {
            Board = new int[x, y];
            CreateShips(Board);
        }

        private void CreateShips(int[,] board)
        {
            List<Ship> Ships = new List<Ship>();
            for (int i = 0; i < 2; i++) //TODO: Skapa funktion för nrOfShips
            {
                int alignment = random.Next(0, 2);
                if(alignment==0)
                {
                    Ship x = new Ship();
                    x.ShipLength = random.Next(1, board.GetLength(0) + 1);
                    Ships.Add(x);
                }
                else
                {
                    Ship x = new Ship();
                    x.ShipHeight = random.Next(1, board.GetLength(1) + 1);
                    Ships.Add(x);
                }
            }
             
            for (int i = 0; i < board.GetLength(0); i++)//TODO: Gör klart metoden!
            {

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = 0;

                }
                Console.WriteLine(board.GetLength(1));
            }
            
        }
    }
}
