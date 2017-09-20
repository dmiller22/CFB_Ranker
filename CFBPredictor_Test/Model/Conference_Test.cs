using System.Linq;
using CFBPredictor.Models;
using NUnit.Framework;

namespace CFBPredictor_Test.Model
{
    [TestFixture]
    class Conference_Test
    {
        Conference sut;

        [SetUp]
        public void Before()
        {
           sut = new Conference("Test Conference");
        }

        /// <summary>
        /// Tests the Conference's constructor
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            Assert.AreEqual("Test Conference", sut.GetConferenceName());
        }

        /// <summary>
        /// Tests that SetRank() correctly sets the conference's rank
        /// </summary>
        [Test]
        public void TestSetRank()
        {
            sut.SetRank(3);

            Assert.AreEqual(3, sut.GetRank());
        }

        /// <summary>
        /// Tests that AddMember() adds the team to the conference
        /// </summary>
        [Test]
        public void TestAddMember()
        {
            sut.AddMember(new Team("test team", "this conference"));

            Assert.AreEqual(1, sut.GetMembers().Count);
        }

        /// <summary>
        /// Tests that ClearMembers() clears all members from the conference
        /// </summary>
        [Test]
        public void TestClearMembers()
        {
            sut.AddMember(new Team("test team", "this conference"));
            sut.ClearMembers();

            Assert.AreEqual(0, sut.GetMembers().Count);
        }
    }
}
