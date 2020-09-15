using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants;
        public Queue<Track> Tracks;

        public Competition(List<IParticipant> participants, Queue<Track> tracks)
        {

        }

        public Track NextTrack()
        {
            return null;
        }
    }
}
