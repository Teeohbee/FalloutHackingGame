using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace FalloutHackingGame
{
    public class Game : List<string>
    {
        // Private property defining the winning word
        private readonly string _winningWord;

        public Game(IEnumerable<string> options, string winningWord)
        {
            _winningWord = winningWord; // sets the winning word as a passed in object
            AddRange(options); // 
        }

        public GameState Attempt(string guess)
        {
            var won = guess == _winningWord;
            if (won)
            {
                return GameState.Win;
            }

            var correctPositions = 0;
            for (int index = 0; index < _winningWord.Length; index++)
            {
                var winningChar = _winningWord[index];
                var guessChar = guess[index];

                if (winningChar == guessChar)
                {
                    correctPositions++;
                }
            }

            return new GameState
            {
                CorrectCount = correctPositions
            };
        }
    }
}