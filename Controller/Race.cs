using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Model;

// Lvl 5 task 1
using System.Timers;
using System.Linq;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        private Random _random;
        private Dictionary<Section, SectionData> _positions;

        private Timer t;

        public event EventHandler<DriversChangedEventArgs> DriversChanged;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            // This kind of random is more random than new Random()
            _random = new Random(DateTime.Now.Millisecond);

            // Initiate _positions
            _positions = new Dictionary<Section, SectionData>();

            PlaceParticipantsOnTrack(track, participants);

            t = new Timer(200);
            t.Elapsed += OnTimedEvent;

        }

        public SectionData GetSectionData(Section section)
        {
            if (_positions.TryGetValue(section, out SectionData data)) 
            {
                return data;
            }
            else
            {
               _positions.Add(section, new SectionData(null, 0, null, 0));
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
                        if (s.Left == null) {
                            s.Left = participant;
                            continue;
                        }
                        s.Right = participant;
                    }
                }
            }
        }

        public void StartTimer() {
            this.t.Start();
        }

        // Runs constantly whenever timer is started
        public void OnTimedEvent(object source, ElapsedEventArgs e) {
            int sectionSize = 100;
            for (int i = _positions.Count - 1; i >= 0; i--) {
                var sData = _positions.ElementAt(i).Value;
                if (sData.Left != null) {
                    var distanceTraveled = sData.Left.Equipment.Performance * sData.Left.Equipment.Speed;
                    sData.DistanceLeft += distanceTraveled;

                    if (sData.DistanceLeft >= sectionSize) {
                        var nextSection = _positions.Count <= i + 1 ? _positions.ElementAt(0).Key : _positions.ElementAt(i + 1).Key;
                        ToNextSection(sData, 0, nextSection);
                    }
                }
                if (sData.Right != null) {
                    var distanceTraveled = sData.Right.Equipment.Performance * sData.Right.Equipment.Speed;
                    sData.DistanceRight += distanceTraveled;

                    if (sData.DistanceRight >= sectionSize) {
                        var nextSection = _positions.Count <= i + 1 ? _positions.ElementAt(0).Key : _positions.ElementAt(i + 1).Key;

                        ToNextSection(sData, 1, nextSection);
                    }
                }
            }
            var eventArgs = new DriversChangedEventArgs { Track = Data.CurrentRace.Track };
            DriversChanged?.Invoke(source, eventArgs);

            //Console.WriteLine($"Timer Elapsed: {e.SignalTime.Ticks}");
        }

        public void ToNextSection(SectionData sectionData, int side, Section nextSection) {
            if (side == 0) {
                var nextSData = GetSectionData(nextSection);
                if (nextSData.Left != null) return;
                nextSData.Left = sectionData.Left;
                sectionData.Left = null;
                sectionData.DistanceLeft = 0;
            }
            else {
                var nextSData = GetSectionData(nextSection);
                if (nextSData.Right != null) return;
                nextSData.Right = sectionData.Right;
                sectionData.Right = null;
                sectionData.DistanceRight = 0;
            }
        }
    }
}
