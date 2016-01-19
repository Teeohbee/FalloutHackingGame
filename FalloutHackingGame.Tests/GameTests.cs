using System.Collections.Generic;
using NUnit.Framework;

namespace FalloutHackingGame.Tests
{
    [TestFixture]
    public class GameTests
    {
        private Game _game;

        [SetUp]
        public void SetUp()
        {
            var words = new List<string>
            {
                "SCORPION",
                "FLOGGING",
                "CROPPERS",
                "MIGRAINE",
                "FOOTNOTE",
                "REFINERY",
                "VAULTING",
                "VICARAGE",
                "PROTRACT",
                "DESCENTS"
            };

            _game = new Game(words, "CROPPERS");
        }

        [Test]
        public void Attempt_GivenWinningWord_Wins()
        {
            var state = _game.Attempt("CROPPERS");

            Assert.That(state, Is.EqualTo(GameState.Win));
        }

        [Test]
        public void Attempt_GivenNotWinningWord_ReturnsGameStateThatIsNotAWin()
        {
            var state = _game.Attempt("SCORPION");

            Assert.That(state, Is.Not.EqualTo(GameState.Win));
        }

        [Test]
        public void Attempt_GivenNotWinningWord_ReturnsPositionallyCorrectLetters()
        {
            var state = _game.Attempt("PROTRACT");

            Assert.That(state.CorrectCount, Is.EqualTo(2));
        }
    }
}