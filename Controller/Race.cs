using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Model;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        private Random _random;
        private Dictionary<Section, SectionData> _positions;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            // This kind of random is more random than new Random()
            _random = new Random(DateTime.Now.Millisecond);

        }



        //Getter for _positions
        public Dictionary<Section, SectionData> GetPosition()
        {
            return _positions;
        }

        // Might be right? Check Tasker level 2 task 4
        public SectionData GetSectionData(Section section)
        {
            if (_positions.ContainsKey(section)) 
            {
                return _positions[section];
            }
            else
            {
               _positions.Add(section, new SectionData());
                return _positions[section];
            }
        }

        public void RandomizeEquipment()
        {
            Random rand = new Random();
            foreach (IParticipant participant in Participants)
            {
                participant.Equipment.Performance = rand.Next(1, 10);
                participant.Equipment.Quality = rand.Next(1, 10);
            }
        }
    }
}
