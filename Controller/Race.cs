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

            // Initiate _positions
            _positions = new Dictionary<Section, SectionData>();

            PlaceParticipantsOnTrack(track, participants);

        }

        public SectionData GetSectionData(Section section)
        {
            if (_positions.TryGetValue(section, out SectionData data)) 
            {
                return data;
                //return _positions[section];
            }
            else
            {
               _positions.Add(section, new SectionData());
               //_positions.Add(section, new SectionData());
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

        public void PlaceParticipantsOnTrack(Track track, List<IParticipant> participants) {
            foreach(Section section in track.Sections) {
                if (participants.Count < 1) {
                    continue;
                }
                if (section.SectionType == SectionTypes.StartGrid) {
                    SectionData s = GetSectionData(section);
                    foreach (IParticipant participant in Participants) {
                        if (s.left == null) {
                            s.left = participant;
                            s.DistanceLeft = 0;
                            continue;
                        }
                        s.right = participant;
                        s.DistanceRight = 0;
                    }
                }
            }
        }
    }
}
