using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutHackingGame
{
    public class GameCreator
    {
        private readonly string _difficulty;
        private readonly List<string> _dic;
        private readonly IPickAChoice<int> _pickAChoice;

        private static readonly Dictionary<string, List<int>> DifficultyAndWordLengths = new Dictionary<string, List<int>>
        {
            { "easy", new List<int> { 4, 5, 6 } },
            { "medium", new List<int> { 7, 8, 9 } },
            { "hard", new List<int> { 10, 11, 12 } },
            { "very hard", new List<int> { 13, 14, 15 } },
        };

        public GameCreator(string difficulty, List<string> dic, IPickAChoice<int> pickAChoice)
        {
            _difficulty = difficulty;
            _dic = dic;
            _pickAChoice = pickAChoice;
        }

        public Game GetWords()
        {
            var length = _pickAChoice.Pick(DifficultyAndWordLengths[_difficulty]);

            var options =  _dic.Where(x => x.Length == length)
                .OrderBy(x => Guid.NewGuid())
                .Take(5)
                .ToList();

            var winner =  options.OrderBy(x => Guid.NewGuid()).First();

            return new Game(options, winner);
        }
    }
    public class GameState
    {
        public static GameState Win = new GameState();

        public int CorrectCount { get; set; }
    }
}
