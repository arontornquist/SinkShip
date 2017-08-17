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
            //TODO: Skapa metoden!
            //throw new NotImplementedException();
            
            GameBoard gameBoard = CreateGameBoard();
            int shotsLeft = 5;
            bool result = false;
            do
            {
                gameBoard.Print();
                Console.WriteLine($"Du har {shotsLeft} försök kvar.");
                Console.WriteLine("Vart vill du stjuta? (x,y)");
                int x = AskForInt("x: ");
                int y = AskForInt("y: ");
                if (gameBoard.Shoot(x,y))
                {
                    result = true;
                }
                shotsLeft--;
            } while (shotsLeft > 0);
            EndGame(result);
        }

        private static void EndGame(bool result)
        {
            if(result)
                Console.WriteLine("Grattis, du har sänkt att skepp");
            else
                Console.WriteLine("Game Over");
        }

        private static GameBoard CreateGameBoard()
        {
            Console.WriteLine("Ange önskad storlek på spelbräde (x,y)");
            int x = AskForInt("x: ");
            int y = AskForInt("y: ");
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
            Console.WriteLine("-- Sänka Skepp --");
            Console.WriteLine("1. Nytt spel");
            Console.WriteLine("2. High Score");
            Console.WriteLine("3. Avsluta");
        }
    }
}
