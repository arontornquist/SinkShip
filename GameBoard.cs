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
            List<Ship> ships = new List<Ship>();
            for (int i = 0; i < 2; i++) //TODO: Skapa funktion för nrOfShips
            {
                ShipAlignment(ships, board);
                ShipStartingPoint(ships, board, i);
            }
             
            for (int i = 0; i < board.GetLength(0); i++)//TODO: Gör klart metoden!
            {

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = 0;
                }
            }
            
        }

        private void ShipStartingPoint(List<Ship> ships, int[,] board, int i) //Sets the starting points (x,y) for a ship
        {
            int x = random.Next(0, (board.GetLength(0) - ships[i].ShipLength + 1));
            int y = random.Next(0, (board.GetLength(1) - ships[i].ShipHeight + 1));
            ships[i].StartingPoint = new int[] { x, y };
            //Console.WriteLine($"{ships[i].StartingPoint[0]}, {ships[i].StartingPoint[1]}, ship length: {ships[i].ShipLength}, ship height: {ships[i].ShipHeight}");

        }

        private void ShipAlignment(List<Ship> ships, int[,] board) //Sets if the ship will be vertical or horizontal
        {
            int alignment = random.Next(0, 2);
            if (alignment == 0)
            {
                Ship x = new Ship();
                x.ShipLength = random.Next(1, board.GetLength(0)+1);
                x.ShipHeight = 1;
                ships.Add(x);
            }
            else
            {
                Ship x = new Ship();
                x.ShipHeight = random.Next(1, board.GetLength(1)+1);
                x.ShipLength = 1;
                ships.Add(x);
            }
        }
    }
}
