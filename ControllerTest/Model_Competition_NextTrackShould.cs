using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        } 

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Track result = this._competition.NextTrack();
            if (result == null)
            {
                Assert.IsNull(result);
            }
        }


    }
}
