using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CFBPredictor.Service;
using CFBPredictor.Models;

namespace CFBPredictor_Test.Service
{
    [TestFixture]
    class GameSimulator_Test
    {
        /// <summary>
        /// Tests that null is returned if a game is simulated which has an invalid
        /// team name
        /// </summary>
        [Test]
        public void TestInvalidTeamNames()
        {
            GameSimulator sut = new GameSimulator(new NCAA(), "homeTeam", "awayTeam");

            Assert.Null(sut.SimulateGame());
        }

        /// <summary>
        /// Tests that the return isn't null and that the proper teams are created if
        /// the passed in team names are valid
        /// </summary>
        [Test]
        public void TestValidTeamNames()
        {
            GameSimulator sut = new GameSimulator(new NCAA(), "Kansas State", "Oklahoma");

            Game simulatedGame = sut.SimulateGame();
            Assert.NotNull(simulatedGame);
            Assert.AreEqual(simulatedGame.GetHomeTeam().GetTeamName(), "Kansas State");
            Assert.AreEqual(simulatedGame.GetAwayTeam().GetTeamName(), "Oklahoma");
        }
    }
}
