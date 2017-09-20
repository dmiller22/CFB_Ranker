using System.Collections.Generic;
using NUnit.Framework;
using CFBPredictor.Service;

namespace CFBPredictor_Test.Service
{
    [TestFixture]
    class RankingGenerator_Test
    {
        private RankingGenerator sut;

        [SetUp]
        public void Before()
        {
            sut = new RankingGenerator(new CFBPredictor.Models.NCAA());
        }

        /// <summary>
        /// Tests that GenerateRankings() returns a list containing each team in Division1
        /// </summary>
        [Test]
        public void TestGenerateRankings()
        {
            List<CFBPredictor.Models.Team> rankings = sut.GenerateRankings();

            Assert.AreEqual(254, rankings.Count);
        }
    }
}
