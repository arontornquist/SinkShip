using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinkShip
{
    class GameBoard
    {
        //log-objekt för att skriva till logfilen
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Random random = new Random();
        public int[,] Board { get; private set; }


        public GameBoard(int x, int y)
        {
            log.Debug($"Skapar nytt GameBoard med storlek {x},{y}");
            Board = new int[x, y];
            CreateShips(Board);
        }

        private void CreateShips(int[,] board)
        {
            List<Ship> ships = new List<Ship>();
            log.Debug("Skapar List<Ship> ships och sätter alla värden till 0");
            for (int i = 0; i < board.GetLength(0); i++)//TODO: Gör klart metoden!
                for (int j = 0; j < board.GetLength(1); j++)
                    board[i, j] = 0;
            log.Debug("Försöker skapa skepp");
            for (int i = 0; i < 2; i++) //TODO: Skapa funktion för nrOfShips
            {
                bool validation = true;
                int[,] validationArray = new int[board.GetLength(0), board.GetLength(1)];
                do
                {
                    //validationArray = board;
                    Array.Copy(board, validationArray, board.GetLength(0));
                    ShipAlignment(ships, board);
                    ShipStartingPoint(ships, board, i);
                    log.Debug("Försöker skriva skepp till board");
                    for (int j = ships[i].StartingPoint[0]; j < ships[i].StartingPoint[0]+ships[i].ShipLength; j++)
                        for (int k = ships[i].StartingPoint[1]; k < ships[i].StartingPoint[1]+ships[i].ShipHeight; k++)
                        {
                            if (board[j, k] != 1)
                            {
                                validationArray[j, k] = 1;
                                log.Debug($"Sätter board[{j},{k}] till 1");
                            }
                            else
                            {
                                log.Debug($"Sätter validation till false på board[{j},{k}]");
                                validation = false;
                            }
                        }
                } while (!validation);
                log.Debug("Lyckades skriva skepp till board");
                board = validationArray;
            }
            log.Debug("Skriver ut board till konsollen");
            for (int i = 0; i < board.GetLength(0); i++)//TODO: Gör klart metoden!
            {
                for (int j = 0; j < board.GetLength(1); j++)
                    Console.Write($"{board[i, j]} ");
                Console.WriteLine();
            }
        }

        private void ShipStartingPoint(List<Ship> ships, int[,] board, int i) //Sets the starting points (x,y) for a ship
        {
            int x = random.Next(0, (board.GetLength(0) - ships[i].ShipLength + 1));
            int y = random.Next(0, (board.GetLength(1) - ships[i].ShipHeight + 1));
            log.Debug($"Sätter startpunkt för skepp till {x},{y}");
            ships[i].StartingPoint = new int[] { x, y };
            //Console.WriteLine($"{ships[i].StartingPoint[0]}, {ships[i].StartingPoint[1]}, ship length: {ships[i].ShipLength}, ship height: {ships[i].ShipHeight}");

        }

        private void ShipAlignment(List<Ship> ships, int[,] board) //Sets if the ship will be vertical or horizontal
        {
            log.Debug("Sätter ship alignment");
            int alignment = random.Next(0, 2);
            log.Debug($"Alignment slumpas till {alignment}");
            if (alignment == 0)
            {
                Ship x = new Ship();
                x.ShipLength = random.Next(1, board.GetLength(0)+1);
                x.ShipHeight = 1;
                ships.Add(x);
                log.Debug($"Ship alignment ShipHeight={x.ShipHeight} ShipLength={ x.ShipLength}");
            }
            else
            {
                Ship x = new Ship();
                x.ShipHeight = random.Next(1, board.GetLength(1)+1);
                x.ShipLength = 1;
                ships.Add(x);
                log.Debug($"Ship alignment ShipHeight={x.ShipHeight} ShipLength={ x.ShipLength}");
            }
        }
    }
}
