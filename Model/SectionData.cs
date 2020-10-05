using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    // Class Diagram says to implement both Section (Class) and IParticipant (Interface)
    public class SectionData
    {
        public IParticipant Left;
        public int DistanceLeft;

        public IParticipant Right;
        public int DistanceRight;

        //public SectionData() { }
        public SectionData(IParticipant left, int distanceLeft, IParticipant right, int distanceRight)
        {
            this.Left = left;
            DistanceLeft = distanceLeft;
            this.Right = right;
            DistanceRight = distanceRight;
        }
    }
}
