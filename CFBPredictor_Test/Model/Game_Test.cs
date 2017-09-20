using CFBPredictor.Models;
using NUnit.Framework;

namespace CFBPredictor_Test.Model
{
    [TestFixture]
    class Game_Test
    {
        Game sut;
        Team home = new Team("Test home team", "test conference");
        Team away = new Team("Test away team", "test conference");

        [SetUp]
        public void Before()
        {
            sut = new Game(home, away, 30.2, 22.7);
        }

        /// <summary>
        /// Tests the constructor with no scores for Game
        /// </summary>
        [Test]
        public void TestConstructorWithNoScores()
        {
            sut = new Game(home, away);

            Assert.AreEqual(home, sut.GetHomeTeam());
            Assert.AreEqual(away, sut.GetAwayTeam());
        }

        /// <summary>
        /// Tests the constructor with scores for Game
        /// </summary>
        [Test]
        public void TestConstructorWithScores()
        {
            Assert.AreEqual(home, sut.GetHomeTeam());
            Assert.AreEqual(away, sut.GetAwayTeam());
            Assert.AreEqual(30, sut.GetHomeScore());
            Assert.AreEqual(23, sut.GetAwayScore());
        }
    }
}
