using System;
using System.Collections.Generic;
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

        //Getter for _positions
        public Dictionary<Section, SectionData> GetPosition()
        {
            return _positions;
        }

        // Might be right? Check Tasker level 2 task 4
        public SectionData GetSectionData(Section section)
        {
            if (section == null)
            {
                _positions.Add(null, new SectionData(null, 0, null, 0));
            }
            else
            {
                return _positions[section];
            }
        }
    }
}
