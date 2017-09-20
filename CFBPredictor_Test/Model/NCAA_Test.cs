using CFBPredictor.Models;
using NUnit.Framework;

namespace CFBPredictor_Test.Model
{
    [TestFixture]
    class NCAA_Test
    {
        private NCAA sut = new NCAA();

        /// <summary>
        /// Tests that FBS contains the proper amount of teams
        /// </summary>
        [Test]
        public void TestFBSTeamCount()
        {
            Assert.AreEqual(130, sut.GetFBS().Count);
        }

        /// <summary>
        /// Tests that FCS contains the proper amount of teams
        /// </summary>
        [Test]
        public void TestFCSTeamCount()
        {
            Assert.AreEqual(124, sut.GetFCS().Count);
        }

        /// <summary>
        /// Tests that the proper amount of conferences are in Division1
        /// </summary>
        [Test]
        public void TestConferenceCount()
        {
            Assert.AreEqual(12, sut.GetConferences().Count);
        }
    }
}
