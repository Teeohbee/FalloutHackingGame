using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FalloutHackingGame;

namespace FalloutHackingGameConsole
{
    class Program
    {
        private static List<string> _dic;
        private static SelectFromChoices _pickachoice;

        static void Main(string[] args)
        {
            Console.WriteLine("Nuka Cola presents - The Fallout Hacking Game\n");
            Console.WriteLine("Please select a difficulty - easy, medium, hard, very hard");
            string difficulty = Console.ReadLine();
            Console.WriteLine("You've chosen " + difficulty + " difficulty\n");

            _dic = File.ReadAllLines("e:\\enable1.txt").ToList();
            _pickachoice = new SelectFromChoices();
            GameCreator gamesetup = new GameCreator(difficulty, _dic, _pickachoice);
            var game = gamesetup.GetWords();
            Console.WriteLine(game);
            foreach (var word in game)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine("What is your guess?");
            string guess = Console.ReadLine();
            var gamestate = game.Attempt(guess);
            Console.ReadLine();
        }
    }
}
