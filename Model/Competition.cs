using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants;
        public Queue<Track> Tracks;

        //public Competition(List<IParticipant> participants, Queue<Track> tracks)
        //{

        //}

        public Competition()
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
        }

        public Track NextTrack()
        {
            if (Tracks.Count > 0)
            {
                // First track in Queue
                return Tracks.Dequeue();
            }
            // if queue is empty, return Null
            return null;
        }
    }
}
