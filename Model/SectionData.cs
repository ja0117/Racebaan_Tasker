using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    // Class Diagram says to implement both Section (Class) and IParticipant (Interface)
    public class SectionData
    {
        public IParticipant left;
        public int DistanceLeft;

        public IParticipant right;
        public int DistanceRight;

        //public SectionData() { }
        //public SectionData(IParticipant left, int distanceLeft, IParticipant right, int distanceRight)
        //{
        //    this.left = left;
        //    DistanceLeft = distanceLeft;
        //    this.right = right;
        //    DistanceRight = distanceRight;
        //}
    }
}
