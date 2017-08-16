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

        internal void DisplayHighScore()
        {
            //TODO: Skriv metoden!
            //throw new NotImplementedException();
            Console.WriteLine("High Score Lista");
        }

        public void DisplayMenu()
        {
            Console.WriteLine("-- Sänka Skepp --");
            Console.WriteLine("1. Nytt spel");
            Console.WriteLine("2. High Score");
            Console.WriteLine("3. Avsluta");
        }
    }
}
