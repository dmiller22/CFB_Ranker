using System;
using CFBPredictor.Models;
using NUnit.Framework;

namespace CFBPredictor_Test
{
    [TestFixture]
    public class Team_Test
    {
        Team sut;

        [SetUp]
        public void Before()
        {
            sut = new Team("testTeam", "testConference", "222");
        }

        /// <summary>
        /// Tests that the 2 argument constructor properly creates a Team
        /// </summary>
        [Test]
        public void TestTwoArgumentConstructor()
        {
            Team team = new Team("testName", "testConference");

            Assert.AreEqual("testName", team.GetTeamName());
            Assert.AreEqual("testConference", team.GetTeamConference());
        }

        /// <summary>
        /// Tests that the 3 argument constructor properly creates a Team
        /// </summary>
        [Test]
        public void TestThreeArgumentConstructor()
        {
            Assert.AreEqual("testTeam", sut.GetTeamName());
            Assert.AreEqual("testConference", sut.GetTeamConference());
            Assert.AreEqual("222", sut.GetTeamID());
        }

        /// <summary>
        /// Tests that GetWinPercentage() returns the proper win percentage
        /// when the team has games played
        /// </summary>
        [Test]
        public void TestGetWinPctWithGamesPlayed()
        {
            sut.IncreaseWins();
            sut.IncreaseLosses();

            Assert.AreEqual(0.500, sut.GetWinPercentage());
        }

        /// <summary>
        /// Tests that GetWinPercentage() returns 0 when the team has not
        /// played any games
        /// </summary>
        [Test]
        public void TestGetWinPctWithNoGamesPlayed()
        {
            Assert.AreEqual(0.000, sut.GetWinPercentage());
        }

        /// <summary>
        /// Tests that GetPPG() returns the proper value when the team has
        /// games played
        /// </summary>
        [Test]
        public void TestGetPPGWithGamesPlayed()
        {
            sut.IncreaseWins();
            sut.IncreaseWins();
            sut.IncreaseTotalPoints(84);

            Assert.AreEqual(42, sut.GetPPG());
        }

        /// <summary>
        /// Tests that GetPPG() returns 0 when the team has not played any
        /// games
        /// </summary>
        [Test]
        public void TestGetPPGWithNoGamesPlayed()
        {
            Assert.AreEqual(0, sut.GetPPG());
        }

        /// <summary>
        /// Tests that GetDefensePPG() returns the proper value when the team
        /// has played games
        /// </summary>
        [Test]
        public void TestGetDPPGWithGamesPlayed()
        {
            sut.IncreaseWins();
            sut.IncreaseWins();
            sut.IncreaseTotalPointsAllowed(35);
            Assert.AreEqual(17.5, sut.GetDefensePPG());
        }

        /// <summary>
        /// Tests that GetDefensePPG() returns 0 when the team has not
        /// played any games
        /// </summary>
        [Test]
        public void TestGetDPPGWithNoGamesPlayed()
        {
            Assert.AreEqual(0, sut.GetDefensePPG());
        }

        /// <summary>
        /// Tests that GetOpponentPPG() returns the proper value when the
        /// team's opponents have played games
        /// </summary>
        [Test]
        public void TestGetOpponentPPGWithGames()
        {
            sut.IncreaseOpponentWins(2);
            sut.IncreaseOpponentLosses(1);
            sut.IncreaseOpponentTotalPoints(33);

            Assert.AreEqual(11, sut.GetOpponentPPG());
        }

        /// <summary>
        /// Tests that GetOpponentPPG() returns 0 when the team's opponents
        /// have not played any games
        /// </summary>
        [Test]
        public void TestGetOpponentPPGWithNoGames()
        {
            Assert.AreEqual(0, sut.GetOpponentPPG());
        }

        /// <summary>
        /// Tests that GetOpponentDefensePPG() returns the proper value when
        /// the team's opponents have played games
        /// </summary>
        [Test]
        public void TestGetOpponentDPPGWithGames()
        {
            sut.IncreaseOpponentWins(2);
            sut.IncreaseOpponentLosses(1);
            sut.IncreaseOpponentTotalPointsAllowed(33);

            Assert.AreEqual(11, sut.GetOpponentDefensePPG());
        }

        /// <summary>
        /// Tests that GetOpponentDefensePPG() returns 0 when the team's opponents
        /// have not played any games
        /// </summary>
        [Test]
        public void TestGetOpponentDPPGWithNoGames()
        {
            Assert.AreEqual(0, sut.GetOpponentDefensePPG());
        }

        /// <summary>
        /// Tests that GetPPGvsOppAvg() returns the proper value
        /// </summary>
        [Test]
        public void TestGetPPGvsOppAvg()
        {
            sut.IncreaseTotalPoints(30);
            sut.IncreaseWins();
            sut.IncreaseOpponentTotalPointsAllowed(20);
            sut.IncreaseOpponentWins(1);

            Assert.AreEqual(10, sut.GetPPGvsOppAvg());
        }

        /// <summary>
        /// Tests that GetDefensePPGvsOppAvg() returns the proper value
        /// </summary>
        [Test]
        public void TestGetDPPGvsOppAvg()
        {
            sut.IncreaseTotalPointsAllowed(14);
            sut.IncreaseWins();
            sut.IncreaseOpponentTotalPoints(35);
            sut.IncreaseOpponentWins(1);

            Assert.AreEqual(-21, sut.GetDefensePPGvsOppAvg());
        }

        /// <summary>
        /// Tests that GetOpponentWinPercentage() returns the proper value when
        /// the opponents have games played
        /// </summary>
        [Test]
        public void TestGetOpponentWinPctWithGamesPlayed()
        {
            sut.IncreaseOpponentWins(3);
            sut.IncreaseOpponentLosses(1);

            Assert.AreEqual(.750, sut.GetOpponentWinPercentage());
        }

        /// <summary>
        /// Tests that GetOpponentWinPercentage() returns 0 when the opponents have
        /// no games played
        /// </summary>
        [Test]
        public void TestGetOpponentWinPctWithNoGamesPlayed()
        {
            Assert.AreEqual(0, sut.GetOpponentWinPercentage());
        }

        /// <summary>
        /// Tests that IncreaseWins() increases the team's win count by 1
        /// </summary>
        [Test]
        public void TestIncreaseWins()
        {
            sut.IncreaseWins();

            Assert.AreEqual(1, sut.GetWins());
        }

        /// <summary>
        /// Tests that IncreaseLosses() increases the team's loss count by 1
        /// </summary>
        [Test]
        public void TestIncreaseLosses()
        {
            sut.IncreaseLosses();

            Assert.AreEqual(1, sut.GetLosses());
        }

        /// <summary>
        /// Tests that IncreaseTotalPoints() increases the team's total points
        /// by the correct amount
        /// </summary>
        [Test]
        public void TestIncreaseTotalPoints()
        {
            sut.IncreaseTotalPoints(45);

            Assert.AreEqual(45, sut.GetTotalPoints());
        }

        /// <summary>
        /// Tests that IncreaseTotalPointsAllowed() increases the team's points
        /// allowed by the proper amount
        /// </summary>
        [Test]
        public void TestIncreaseTotalPointsAllowed()
        {
            sut.IncreaseTotalPointsAllowed(21);

            Assert.AreEqual(21, sut.GetTotalPointsAllowed());
        }

        /// <summary>
        /// Tests that IncreaseOpponentWins() increases the team's opponents' win
        /// count by the proper amount
        /// </summary>
        [Test]
        public void TestIncreaseOpponentWins()
        {
            sut.IncreaseOpponentWins(6);

            Assert.AreEqual(6, sut.GetOpponentWins());
        }

        /// <summary>
        /// Tests that IncreaseOpponentLosses() increases the team's opponents' loss
        /// count by the proper amount
        /// </summary>
        [Test]
        public void TestIncreaseOpponentLosses()
        {
            sut.IncreaseOpponentLosses(7);

            Assert.AreEqual(7, sut.GetOpponentLosses());
        }

        /// <summary>
        /// Tests that IncreaseOpponentTotalPoints() increases the team's opponents'
        /// total points scored by the proper amount
        /// </summary>
        [Test]
        public void TestIncreaseOpponentPoints()
        {
            sut.IncreaseOpponentTotalPoints(33);

            Assert.AreEqual(33, sut.GetOpponentTotalPoints());
        }

        /// <summary>
        /// Tests that IncreaseOpponentPointsAllowed() increases the team's opponents'
        /// total points allowed by the proper amount
        /// </summary>
        [Test]
        public void TestIncreaseOpponentPointsAllowed()
        {
            sut.IncreaseOpponentTotalPointsAllowed(22);

            Assert.AreEqual(22, sut.GetOpponentTotalPointsAllowed());
        }

        /// <summary>
        /// Tests that the team's opponent adjusted PPG is set properly
        /// </summary>
        [Test]
        public void TestOpponentAdjustedPPG()
        {
            double low = -5.5;
            sut.IncreaseTotalPoints(30);
            sut.IncreaseWins();
            sut.IncreaseOpponentTotalPointsAllowed(20);
            sut.IncreaseOpponentWins(1);
            sut.SetOppAdjustedPPG(low);

            Assert.AreEqual(20 - low - 8.5, sut.GetOppAdjustedPPG());
        }

        /// <summary>
        /// Tests that the team's opponent adjusted DPPG is set properly
        /// </summary>
        [Test]
        public void TestOpponentAdjustedDPPG()
        {
            double high = 11.5;
            sut.IncreaseTotalPointsAllowed(14);
            sut.IncreaseWins();
            sut.IncreaseOpponentTotalPoints(35);
            sut.IncreaseOpponentWins(1);
            sut.SetSOSRank(22);
            sut.SetOppAdjustedDPPG(high);

            double expected = -((sut.GetDefensePPGvsOppAvg() - high - 1) * ((129 - Convert.ToDouble(sut.GetSOSRank())) / 100)
                              + sut.GetDefensePPGvsOppAvg() + 11.5);

            Assert.AreEqual(expected, sut.GetOppAdjustedDPPG());
        }

        /// <summary>
        /// Tests that AddOpponent() adds the opponent to the team's schedule
        /// </summary>
        [Test]
        public void TestAddOpponent()
        {
            Team opponent = new Team("testTeam", "Test conference");
            sut.AddOpponent(opponent);

            Assert.That(sut.GetSchedule().Contains(opponent));
        }

        /// <summary>
        /// Tests that ClearPoints() sets the team's points to 0
        /// </summary>
        [Test]
        public void TestClearPoints()
        {
            sut.SetPoints(33);
            sut.ClearProjectedPoints();

            Assert.AreEqual(0, sut.GetPoints());
        }

        /// <summary>
        /// Tests that ClearFBSRank() sets the team's FBSRank to 0
        /// </summary>
        [Test]
        public void TestClearFBSRank()
        {
            sut.SetFBSRank(4);
            sut.ClearFBSRank();

            Assert.AreEqual(0, sut.GetFBSRank());
        }

        /// <summary>
        /// Tests that Clear() sets all values it should to 0
        /// </summary>
        [Test]
        public void TestClear()
        {
            sut.rating = 5;
            sut.SetFBSRank(20);
            sut.IncreaseWins();
            sut.IncreaseLosses();
            sut.IncreaseOpponentWins(4);
            sut.IncreaseOpponentLosses(2);
            sut.IncreaseTotalPoints(28);
            sut.IncreaseTotalPointsAllowed(21);
            sut.SetPoints(42);
            sut.IncreaseOpponentTotalPoints(35);
            sut.IncreaseOpponentTotalPointsAllowed(31);
            sut.SetRank(20);
            sut.SetStrength(5.55);
            sut.SetSOSRank(33);
            sut.AddOpponent(new Team("test team", "conference"));

            sut.Clear();

            Assert.AreEqual(0, sut.rating);
            Assert.AreEqual(0, sut.GetFBSRank());
            Assert.AreEqual(0, sut.GetWins());
            Assert.AreEqual(0, sut.GetLosses());
            Assert.AreEqual(0, sut.GetOpponentWins());
            Assert.AreEqual(0, sut.GetOpponentLosses());
            Assert.AreEqual(0, sut.GetTotalPoints());
            Assert.AreEqual(0, sut.GetTotalPointsAllowed());
            Assert.AreEqual(0, sut.GetPoints());
            Assert.AreEqual(0, sut.GetOpponentTotalPoints());
            Assert.AreEqual(0, sut.GetOpponentTotalPointsAllowed());
            Assert.AreEqual(0, sut.GetRank());
            Assert.AreEqual(0, sut.GetStrength());
            Assert.AreEqual(0, sut.GetSOSRank());
            Assert.AreEqual(0, sut.GetScheduleSize());
        }
    }
}