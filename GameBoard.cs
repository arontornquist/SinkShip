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
        private Random random = new Random();
        private int[,] board;

        private int[,] Board
        {
            get { return board; }
            set { board = value; }
        }

        //private int[,] Board { get; set; }


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
                Ship x = new Ship();
                ships.Add(x);
                bool validation = true;
                int[,] validationArray = new int[Board.GetLength(0), Board.GetLength(1)];
                do
                {
                    validationArray = Board.Clone() as int[,];
                    validation = true;
                    ShipAlignment(ships, Board, i);
                    ShipStartingPoint(ships, Board, i);
                    log.Debug("Försöker skriva skepp till board");
                    for (int j = ships[i].StartingPoint[0]; j < ships[i].StartingPoint[0] + ships[i].ShipLength; j++)
                        for (int k = ships[i].StartingPoint[1]; k < ships[i].StartingPoint[1] + ships[i].ShipHeight; k++)
                        {
                            if (Board[j, k] != 1)
                            {
                                log.Debug($"Värde innan [{validationArray[j, k]}]");
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
                Board = validationArray.Clone() as int[,];
            }
            log.Debug("Skriver ut board till konsollen");
            
        }

        public bool Shoot(int x, int y)
        {
            if (Board[x, y] == 1)
                Board[x, y] = 2;
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                    if (Board[i,j] == 1)
                        return false;
            return true;
        }

        public void Print()
        {
            for (int i = 0; i < board.GetLength(0); i++)//TODO: Gör klart metoden!
            {
                for (int j = 0; j < board.GetLength(1); j++)
                    if (i == 0 && j == 0)
                    //{
                    //    Console.Write(" ¤ ");
                    //    i--;
                    //    j--;
                    //}
                    //else if (i == 0)
                    //{
                    //    Console.Write($" {(j)} ");

                    //}
                    //else if (j == 0)
                    //    Console.Write($" {(i)} ");
                    if (board[i, j] == 2)
                        Console.Write(" X ");
                    else
                        Console.Write(" ~ ");
                Console.WriteLine();
            }
        }

        private void ShipStartingPoint(List<Ship> ships, int[,] board, int i) //Sets the starting points (x,y) for a ship
        {
            int x = random.Next(0, (board.GetLength(0) - ships[i].ShipLength + 1));
            int y = random.Next(0, (board.GetLength(1) - ships[i].ShipHeight + 1));
            log.Debug($"Sätter startpunkt för skepp till {x},{y}");
            ships[i].StartingPoint = new int[] { x, y };
        }

        private void ShipAlignment(List<Ship> ships, int[,] board, int i) //Sets if the ship will be vertical or horizontal
        {
            log.Debug("Sätter ship alignment");
            int alignment = random.Next(0, 2);
            log.Debug($"Alignment slumpas till {alignment}");
            if (alignment == 0)
            {
                ships[i].ShipLength = random.Next(1, board.GetLength(0)+1);
                ships[i].ShipHeight = 1;
                log.Debug($"Ship alignment ShipHeight={ships[i].ShipHeight} ShipLength={ ships[i].ShipLength}");
            }
            else
            {
                ships[i].ShipHeight = random.Next(1, board.GetLength(1)+1);
                ships[i].ShipLength = 1;
                log.Debug($"Ship alignment ShipHeight={ships[i].ShipHeight} ShipLength={ ships[i].ShipLength}");
            }
        }
    }
}
