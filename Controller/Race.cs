using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Model;

// Lvl 5 task 1
using System.Timers;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

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

        //Score for participants, keeps track of the laps
        public static Dictionary<IParticipant, int> LapsFinished;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            // This kind of random is more random than new Random()
            _random = new Random(DateTime.Now.Millisecond);
            //RandomizeEquipment();
            // Initiate _positions
            _positions = new Dictionary<Section, SectionData>();

            PlaceParticipantsOnTrack(track, participants);

            t = new Timer(350);
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
                participant.Equipment.Performance = rand.Next(10, 20);
                participant.Equipment.Quality = rand.Next(10, 20);
            }
        }

        public void PlaceParticipantsOnTrack(Track track, List<IParticipant> participants) {
            int i = 0;
            foreach (Section section in track.Sections)
            {
                if (participants.Count < 1)
                {
                    continue;
                }
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    SectionData s = GetSectionData(section);
                    if (i < participants.Count)
                    {
                        if (s.Left == null)
                        {
                            s.Left = participants[i];
                            i++;
                        }
                        if (s.Right == null)
                        {
                            s.Right = participants[i];
                            i++;
                        }
                    }
                }
            }
        }

        public void StartTimer() {
            this.t.Start();
        }

        // Runs constantly whenever timer is started
        //public void OnTimedEvent(object source, ElapsedEventArgs e) {
        //    int sectionSize = 100;
        //    for (int i = _positions.Count - 1; i >= 0; i--) {
        //        var sData = _positions.ElementAt(i).Value;
        //        if (sData.Left != null) {
        //            var distanceTraveled = sData.Left.Equipment.Performance * sData.Left.Equipment.Speed;
        //            sData.DistanceLeft += distanceTraveled;

        //            if (sData.DistanceLeft >= sectionSize) {
        //                var nextSection = _positions.Count <= i + 1 ? _positions.ElementAt(0).Key : _positions.ElementAt(i + 1).Key;
        //                IsFinished(sData);
        //                ToNextSection(sData, 0, nextSection);
        //            }
        //        }
        //        if (sData.Right != null) {
        //            var distanceTraveled = sData.Right.Equipment.Performance * sData.Right.Equipment.Speed;
        //            sData.DistanceRight += distanceTraveled;

        //            if (sData.DistanceRight >= sectionSize) {
        //                var nextSection = _positions.Count <= i + 1 ? _positions.ElementAt(0).Key : _positions.ElementAt(i + 1).Key;
        //                IsFinished(sData);
        //                ToNextSection(sData, 1, nextSection);
        //            }
        //        }
        //    }
        //    var eventArgs = new DriversChangedEventArgs { Track = Data.CurrentRace.Track };
        //    DriversChanged?.Invoke(source, eventArgs);

        //    //Console.WriteLine($"Timer Elapsed: {e.SignalTime.Ticks}");
        //}

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            int sectionSize = 100;
            int iteration = 1;
            foreach (KeyValuePair<Section, SectionData> s in _positions)
            {
                if (s.Value.Left != null)
                {
                    int DistanceTraveled = s.Value.Left.Equipment.Performance * s.Value.Left.Equipment.Speed;
                    s.Value.DistanceLeft += DistanceTraveled;
                    if (s.Value.DistanceLeft >= sectionSize)
                    {
                        if (iteration > _positions.Count)
                        {
                            iteration = 0;
                        }
                        Section nextSection = _positions.ElementAt(iteration - 1).Key;
                        ToNextSection(s.Value, 0, nextSection);
                    }
                }
                if (s.Value.Right != null)
                {
                    int DistanceTraveled = s.Value.Right.Equipment.Performance * s.Value.Right.Equipment.Speed;
                    s.Value.DistanceRight += DistanceTraveled;
                    if (s.Value.DistanceRight >= sectionSize)
                    {
                        if (iteration > _positions.Count)
                        {
                            iteration = 0;
                        }
                        Section nextSection = _positions.ElementAt(iteration - 1).Key;
                        ToNextSection(s.Value, 1, nextSection);
                    }
                }
                iteration++;
            }
            //iteration = 1;
            var eventArgs = new DriversChangedEventArgs { Track = Data.CurrentRace.Track };
            DriversChanged?.Invoke(source, eventArgs);
        }

        public void IsFinished(SectionData sectionData)
        {
            if (true)
            {
                if (sectionData.Left != null && sectionData.Right != null)
                {
                    if (LapsFinished.ContainsKey(sectionData.Right))
                    {
                        LapsFinished[sectionData.Right] += 1;
                    }
                    if (LapsFinished.ContainsKey(sectionData.Left))
                    {
                        LapsFinished[sectionData.Left] += 1;
                    }
                }
                else if (sectionData.Left != null && sectionData.Right == null)
                {
                    if (LapsFinished.ContainsKey(sectionData.Left))
                    {
                        LapsFinished[sectionData.Left] += 1;
                    }
                }
                else if (sectionData.Right != null && sectionData.Left == null)
                {
                    if (LapsFinished.ContainsKey(sectionData.Right))
                    {
                        LapsFinished[sectionData.Right] += 1;
                    }
                }
            }
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
