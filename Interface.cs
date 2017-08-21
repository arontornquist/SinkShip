using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinkShip
{
    class Interface
    {
        //log-objekt för att skriva till loggfilen
        public static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Print output to console. Read String from user.
        /// </summary>
        /// <param name="output">Text to print </param>
        /// <returns>A String from user</returns>
        private static string AskForString(string output)
        {
            Console.Write(output);
            return Console.ReadLine();
        }
        /// <summary>
        /// Print output to console. Read Integer from user.
        /// </summary>
        /// <param name="output">Text to print </param>
        /// <returns>An interger from user</returns>
        private static int AskForInt(string output)
        {
            int answer = 0;
            bool isInt = false;
            do
            {
                Console.Write(output); //Kul att prova och använda oss av try/catch - Vi är medvetna om att Try.Parse kanske blir renare kod
                try
                {
                    answer = int.Parse(Console.ReadLine());
                    isInt = true;
                }
                catch (FormatException e)
                {
                    log.Error($"Fel vid inmatning, {e.GetType()} fångat");
                    Console.WriteLine("Skriv in ett nummer, försök igen");
                }
            } while (!isInt);
            return answer;
        }
        /// <summary>
        /// Start a new game.
        /// </summary>
        private static void NewGame()
        {
            GameBoard gameBoard = CreateGameBoard();
            int shotsLeft = NumberOfShots(gameBoard.X, gameBoard.Y);
            log.Debug($"Spelplan på {gameBoard.X} x {gameBoard.Y} ger {shotsLeft} skott till användaren");
            Console.Clear();
            bool result = false;
            Shoot(gameBoard, ref shotsLeft, ref result);
            EndGame(result);
        }

        private static void Shoot(GameBoard gameBoard, ref int shotsLeft, ref bool result)
        {
            do
            {
                log.Debug($"Användaren skjuter. Antal skott kvar {shotsLeft}");
                gameBoard.Print();
                Console.WriteLine($"Du har {shotsLeft} försök kvar.");
                Console.WriteLine("Vart vill du skjuta? (x,y)");
                int x, y;
                PrepareShot(gameBoard, out x, out y);

                if (gameBoard.Shoot(x, y))
                {
                    log.Debug($"Användaren skjuter, träff på x:{x} y:{y}");
                    if (gameBoard.Check())
                    {
                        gameBoard.Print();
                        result = true;
                        break;
                    }
                }
                else
                {
                    Console.Clear();
                    log.Debug($"Användaren skjuter, miss på x:{x} y:{y}");
                    shotsLeft--;
                }
            } while (shotsLeft > 0);
        }

        private static void PrepareShot(GameBoard gameBoard, out int x, out int y)
        {
            do
            {
                int tmpX = AskForInt("x: ");
                if (tmpX < 1 || tmpX > gameBoard.X)
                {
                    log.Error($"Användaren matar in felaktigt x värde ({tmpX})");
                    Console.WriteLine("Felaktig inmatning, värde på x ligger utanför spelbrädet");
                }
                else
                {
                    x = tmpX;
                    break;
                }
            } while (true);
            do
            {
                int tmpY = AskForInt("y: ");
                if (tmpY < 1 || tmpY > gameBoard.Y)
                {
                    log.Error($"Användaren matar in felaktigt y värde ({tmpY})");
                    Console.WriteLine("Felaktig inmatning, värde på y ligger utanför spelbrädet");
                }
                else
                {
                    y = tmpY;
                    break;
                }
            } while (true);
        }

        private static int NumberOfShots(int x, int y)
        {
            return x*y/2;
        }

        /// <summary>
        /// Ends the current game. Display result to the user.
        /// </summary>
        /// <param name="result">True=win, False=loss </param>
        private static void EndGame(bool gameResult)
        {
            if (gameResult)
            {
                log.Debug("Alla skepp sänkta");
                Console.WriteLine("Grattis, du har sänkt alla skepp"); //TODO - Hur många skepp är kvar att sänka?
                Console.WriteLine(@"                                                       /\  /\");
                Console.WriteLine(@"        Grattis                                        /\  /\");
                Console.WriteLine(@"    du har sänkt alla skepp!                          /  ''  \");
                Console.WriteLine(@"                                                     / .o\  ..\");
                Console.WriteLine(@"                                                    /.'  |\ | '.");
                Console.WriteLine(@"              __                                    '    |_\|");
                Console.WriteLine(@"              \ \___     .__                             |_|<o>");
                Console.WriteLine(@"            .--^^___\..--^ /                             | _ | | ");
                Console.WriteLine(@"        .__.|-^^^..... ' /                          _____|_|_|\__");
                Console.WriteLine(@"________\_______________/______________________..-'::::::::::::::::-.._");
                Console.WriteLine();
                Console.WriteLine("Tryck på valfri tangent för att fortsätta");
                Console.ReadKey();
            }

            else
            {
                log.Debug("Game over");
                Console.WriteLine(@"              ('-.    _   .-')      ('-.                   (`-.     ('-. _  .-')  ");
                Console.WriteLine(@"             ( OO ).-( '.( OO )_  _(  OO)                _(OO  )_ _(  OO( \( -O ) ");
                Console.WriteLine(@"  ,----.     / . --. /,--.   ,--.(,------..-'),-----.,--(_/   ,. (,------,------. ");
                Console.WriteLine(@" '  .-./-')  | \-.  \ |   `.'   | |  .---( OO'  .-.  \   \   /(__/|  .---|   /`. '");
                Console.WriteLine(@" |  |_( O- .-'-'  |  ||         | |  |   /   |  | |  |\   \ /   / |  |   |  /  | |");
                Console.WriteLine(@" |  | .--, \\| |_.'  ||  |'.'|  |(|  '--.\_) |  |\|  | \   '   /,(|  '--.|  |_.' |");
                Console.WriteLine(@"(|  | '. (_/ |  .-.  ||  |   |  | |  .--'  \ |  | |  |  \     /__)|  .--'|  .  '.'");
                Console.WriteLine(@" |  '--'  |  |  | |  ||  |   |  | |  `---.  `'  '-'  '   \   /    |  `---|  |\  \ ");
                Console.WriteLine(@"  `------'   `--' `--'`--'   `--' `------'    `-----'     `-'     `------`--' '--'");
                Console.WriteLine();
                Console.WriteLine("Tryck på valfri tangent för att fortsätta");
                Console.ReadKey();
            }
                
        }
        /// <summary>
        /// Creates gameboard.
        /// </summary>
        /// <returns>New GameBoard object</returns>
        private static GameBoard CreateGameBoard()
        {
            int x, y;
            GetXandY(out x, out y);
            return new GameBoard(x, y);
        }

        private static void GetXandY(out int x, out int y)
        {
            Console.WriteLine("Ange önskad storlek på spelbräde (x,y)");
            do
            {
                int tmpX = AskForInt("x: ");
                if (tmpX < 3 || tmpX > 20)
                {
                    Console.WriteLine("Felaktig inmatning, x kan bara ha ett värde mellan 3-20");
                }
                else
                {
                    x = tmpX;
                    break;
                }
            } while (true);
            do
            {
                int tmpY = AskForInt("y: ");
                if (tmpY < 3 || tmpY > 20)
                {
                    Console.WriteLine("Felaktig inmatning, y kan bara ha ett värde mellan 3-20");
                }
                else
                {
                    y = tmpY;
                    break;
                }
            } while (true);
        }

        /// <summary>
        /// Print out the High Score list to the console.
        /// </summary>
        public void DisplayHighScore()
        {
            //TODO: Skriv metoden! <------ Ja, vi håller med. Vi saknade vår vinst i listan.
            //throw new NotImplementedException();
            Console.WriteLine("High Score Lista"); 
            Console.ReadLine();
        }
        /// <summary>
        /// Init the menu
        /// </summary>
        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                DisplayMainMenu();
                int choice = AskForInt(": ");
                switch (choice)
                {
                    case 1:
                        NewGame();
                        break;
                    case 2:
                        DisplayHighScore();
                        break;
                    case 3:
                        log.Debug("Avslutar programmet");
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }

        }
        /// <summary>
        /// Print out the main menu to the console.
        /// </summary>
        public void DisplayMainMenu()
        {
            log.Debug("Skriver ut huvudmeny"); ///GOOD! Snygg logga.
            Console.WriteLine(@" ________  ___  ________   ___  __            ________  ___  ___  ___  ________   ");
            Console.WriteLine(@"|\   ____\|\  \|\   ___  \|\  \|\  \         |\   ____\|\  \|\  \|\  \|\   __  \  ");
            Console.WriteLine(@"\ \  \___|\ \  \ \  \\ \  \ \  \/  /|_       \ \  \___|\ \  \\\  \ \  \ \  \|\  \ ");
            Console.WriteLine(@" \ \_____  \ \  \ \  \\ \  \ \   ___  \       \ \_____  \ \   __  \ \  \ \   ____\");
            Console.WriteLine(@"  \|____|\  \ \  \ \  \\ \  \ \  \\ \  \       \|____|\  \ \  \ \  \ \  \ \  \___|");
            Console.WriteLine(@"    ____\_\  \ \__\ \__\\ \__\ \__\\ \__\        ____\_\  \ \__\ \__\ \__\ \__\   ");
            Console.WriteLine(@"   |\_________\|__|\|__| \|__|\|__| \|__|       |\_________\|__|\|__|\|__|\|__|   ");
            Console.WriteLine(@"   \|_________|                                 \|_________|                      ");
            Console.WriteLine();
            Console.WriteLine("-- Sänka Skepp --");
            Console.WriteLine("1. Nytt spel");
            Console.WriteLine("2. High Score");
            Console.WriteLine("3. Avsluta");
        }
    }
}
