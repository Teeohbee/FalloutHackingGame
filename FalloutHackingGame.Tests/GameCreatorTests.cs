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

            // Using mock framework Moq
            _picker = new Mock<IPickAChoice<int>>();
            _picker.Setup(x => x.Pick(It.IsAny<List<int>>())).Returns(5);
            // Creates the mock of IPickAChoice and fixes the return of Pick to 5,
            // if any list of integers is provided as an argument.

            _gameCreator = new GameCreator("easy", _dic, _picker.Object);
        }

        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        // These tests change the return of the _picker.Pick mock to return all test cases.
        public void GetWords_EasyDifficulty_WordLengthsTheSameAsThePickerPicks(int lengthSelected)
        {
            _picker.Setup(x => x.Pick(It.IsAny<List<int>>())).Returns(lengthSelected);

            var words = _gameCreator.GetWords();

            Assert.That(words[0].Length, Is.EqualTo(lengthSelected));
        }

        [Test]
        // Early test to ensure we couldn't cheat and hardcode words.
        public void GetWords_EasyDifficulty_AllWordSuggestionsAreDifferent()
        {
            var words = _gameCreator.GetWords();

            Assert.That(words.Distinct().Count(), Is.EqualTo(words.Count));
        }

        [Test]
        // Rules specify game must provided between, 5 and 15 words. Currently
        // hardcoded to always return 5 words so currently useless.
        public void GetWords_EasyDifficulty_GivesBetweenFiveAndFifteenElements()
        {
            var words = _gameCreator.GetWords();

            Assert.That(words.Count, Is.AtLeast(5));
            Assert.That(words.Count, Is.AtMost(15));
        }
    }
}