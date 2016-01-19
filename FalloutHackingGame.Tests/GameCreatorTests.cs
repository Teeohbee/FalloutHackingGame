using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace FalloutHackingGame.Tests
{
    [TestFixture]
    public class GameCreatorTests
    {
        private GameCreator _gameCreator;
        private List<string> _dic;
        private Mock<IPickAChoice<int>> _picker;

        [SetUp]
        public void SetUp()
        {
            _dic = File.ReadAllLines("e:\\enable1.txt").ToList();

            _picker = new Mock<IPickAChoice<int>>();
            _picker.Setup(x => x.Pick(It.IsAny<List<int>>())).Returns(5);

            _gameCreator = new GameCreator("easy", _dic, _picker.Object);
        }

        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void GetWords_EasyDifficulty_WordLengthsTheSameAsThePickerPicks(int lengthSelected)
        {
            _picker.Setup(x => x.Pick(It.IsAny<List<int>>())).Returns(lengthSelected);

            var words = _gameCreator.GetWords();

            Assert.That(words[0].Length, Is.EqualTo(lengthSelected));
        }

        [Test]
        public void GetWords_EasyDifficulty_AllWordSuggestionsAreDifferent()
        {
            var words = _gameCreator.GetWords();

            Assert.That(words.Distinct().Count(), Is.EqualTo(words.Count));
        }

        [Test]
        public void GetWords_EasyDifficulty_GivesBetweenFiveAndFifteenElements()
        {
            var words = _gameCreator.GetWords();

            Assert.That(words.Count, Is.AtLeast(5));
            Assert.That(words.Count, Is.AtMost(15));
        }
    }

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