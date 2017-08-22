using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
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
        public int X { get; }
        public int Y { get; }
        private int[,] Board
        {
            get { return board; }
            set { board = value; }
        }


        public GameBoard(int x, int y)
        {
            X = x;
            Y = y;
            log.Debug($"Skapar nytt GameBoard med storlek {x},{y}");
            Board = new int[x, y];
            CreateShips(Board);
        }

        private void CreateShips(int[,] board)
        {
            List<Ship> ships = new List<Ship>();
            log.Debug("Skapar List<Ship> ships och sätter alla värden till 0");
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                    board[i, j] = 0;
            log.Debug("Försöker skapa skepp");
            int nrOfShips=NrOfShips();
            for (int i = 0; i < nrOfShips; i++) 
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

        private int NrOfShips() // Skapar ett heltal ( som används för att skapa skepp) 1 mindre än det minsta värdet av X och Y vilket är brädets koordinater
        {
            if (X >= Y)
                return (X - 1);
            else
                return (Y - 1);
        }

        public bool Shoot(int x, int y)
        {
            if (Board[(x-1), (y-1)] == 1)
            {
                Board[(x - 1), (y - 1)] = 2;
                HitEffects();
                return true;
            }
            return false;   
        }

        private static void HitEffects()
        {
            bool visible = true;
            for (int i = 0; i < 7; i++)
            {
                string hit = "HIT! HIT!! HIT!!!";
                using (SoundPlayer player = new SoundPlayer(@"../../audio/beep-03.wav"))
                    player.PlaySync();
                Console.Clear();
                Console.ForegroundColor = visible ? ConsoleColor.Red : ConsoleColor.Yellow;
                visible = !visible;
                
                Console.Write(hit);
                Thread.Sleep(100);
            }
                Console.Clear();
        }

        public bool Check()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                    if (Board[i, j] == 1)
                        return false;
            }
            return true;
        }

        public void Print()
        {
            Console.WriteLine();
            for (int i = -1; i < board.GetLength(0); i++)
            {
                for (int j = -1; j < board.GetLength(1); j++)
                    if (i == -1 && j == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("¤".PadLeft(3));
                        Console.ResetColor();
                    }
                    else if (i == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"{(j + 1).ToString().PadLeft(3)}");
                        Console.ResetColor();
                    }
                    else if (j == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"{(i + 1).ToString().PadLeft(3)}");
                        Console.ResetColor();
                    }
                    else if (board[i, j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X".PadLeft(3));
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("~".PadLeft(3));
                        Console.ResetColor();
                    }
                Console.WriteLine();
            }
            Console.WriteLine();
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
