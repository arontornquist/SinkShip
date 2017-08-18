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
                Console.Write(output);
                try
                {
                    answer = int.Parse(Console.ReadLine());
                    isInt = true;
                }
                catch (Exception)
                {
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
            int shotsLeft = 5;
            bool result = false;
            do
            {
                log.Debug($"Användaren skjuter. Antal skott kvar {shotsLeft}");
                gameBoard.Print();
                Console.WriteLine($"Du har {shotsLeft} försök kvar.");
                Console.WriteLine("Vart vill du stjuta? (x,y)");
                int x, y;
                do
                {
                    int tmpX = AskForInt("x: ");
                    if(tmpX < 1 || tmpX > gameBoard.X)
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
                

                if (gameBoard.Shoot(x,y))
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
                    log.Debug($"Användaren skjuter, miss på x:{x} y:{y}");
                    shotsLeft--;
                }              
            } while (shotsLeft > 0);
            EndGame(result);
        }
        /// <summary>
        /// Ends the current game. Display result to the user.
        /// </summary>
        /// <param name="result">True=win, False=loss </param>
        private static void EndGame(bool result)
        {
            if (result)
            {
                log.Debug("Alla skepp sänkta");
                Console.WriteLine("Grattis, du har sänkt alla skepp");
                Console.ReadKey();
            }

            else
            {
                log.Debug("Game over");
                Console.WriteLine("Game Over");
                Console.ReadKey();
            }
                
        }
        /// <summary>
        /// Creates gameboard. Gets size from user input.
        /// </summary>
        /// <returns>New GameBoard object</returns>
        private static GameBoard CreateGameBoard()
        {
            Console.WriteLine("Ange önskad storlek på spelbräde (x,y)");
            int x, y;
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
            return new GameBoard(x, y);
        }
        /// <summary>
        /// Print out the High Score list to the console.
        /// </summary>
        public void DisplayHighScore()
        {
            //TODO: Skriv metoden!
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
            log.Debug("Skriver ut huvudmeny");
            Console.WriteLine("-- Sänka Skepp --");
            Console.WriteLine("1. Nytt spel");
            Console.WriteLine("2. High Score");
            Console.WriteLine("3. Avsluta");
        }
    }
}
