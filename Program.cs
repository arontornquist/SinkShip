using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinkShip
{
    class Program
    {
        static void Main(string[] args)
        {
            StartGame();
        }

        private static void StartGame()
        {
            Interface ui = new Interface();
            ui.DisplayMenu();
            int choice = ui.AskForInt(": ");
            switch (choice)
            {
                case 1:
                    NewGame();
                    break;
                case 2:
                    ui.DisplayHighScore();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
            GameBoard g = new GameBoard(4,4);
        }

        private static void NewGame()
        {
            //TODO: Skapa metoden!
            //throw new NotImplementedException();
            Console.WriteLine("Nytt spel!!");
        }
    }
}
