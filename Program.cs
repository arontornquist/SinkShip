﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinkShip
{
    class Program
    {
        static void Main(string[] args) //GOOD Snyggt med lite kod i main
        {
            Interface.log.Debug("Startar programmet");
            StartGame();
        }

        private static void StartGame()
        {
            var ui = new Interface();
            ui.Menu();
        }
    }
}
