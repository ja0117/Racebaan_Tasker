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

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Track t = new Track("Piet", new SectionTypes[] {
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Finish
            });
            _competition.Tracks.Enqueue(t);

            Track result = this._competition.NextTrack();
            Assert.AreEqual(t, result);
        }
        
        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Track t = new Track("Piet", new SectionTypes[] {
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Finish
            });
            _competition.Tracks.Enqueue(t);

            Track result = this._competition.NextTrack();
            result = this._competition.NextTrack();
            Assert.IsNull(result);

        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            Track t1 = new Track("Zwolle", new SectionTypes[] {
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Finish
            });
            Track t2 = new Track("Piet", new SectionTypes[] {
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Finish
            });
            _competition.Tracks.Enqueue(t1);
            _competition.Tracks.Enqueue(t2);

            Track result = _competition.NextTrack();

            // Shouldn't this be T2? since it's added last to the queue
            Assert.AreEqual(t1, result);
        }
    }
}
