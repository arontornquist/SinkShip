using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinkShip
{
    class Interface
    {
        public string AskForString(string output)
        {
            Console.Write(output);
            return Console.ReadLine();
        }
        /// <summary>
        /// Print output to console. Read Integer from user.
        /// </summary>
        /// <param name="output">Text to print </param>
        /// <returns>An interger from user</returns>
        public int AskForInt(string output)
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
        private static void NewGame()
        {
            //TODO: Skapa metoden!
            //throw new NotImplementedException();
            Console.WriteLine("Nytt spel!!");
        }
        public void DisplayHighScore()
        {
            //TODO: Skriv metoden!
            //throw new NotImplementedException();
            Console.WriteLine("High Score Lista");
        }
        public void Menu()
        {
            DisplayMainMenu();
            int choice = AskForInt(": ");
            switch (choice)
            {
                case 1:
                    //NewGame();
                    break;
                case 2:
                    //DisplayHighScore();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        public void DisplayMainMenu()
        {
            Console.WriteLine("-- Sänka Skepp --");
            Console.WriteLine("1. Nytt spel");
            Console.WriteLine("2. High Score");
            Console.WriteLine("3. Avsluta");
        }
    }
}
