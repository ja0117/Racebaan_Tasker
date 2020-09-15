using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Car : IEquipment
    {

        // Not Sure if this is correct. Check Class Diagram!
        public int Quality { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Performance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsBroken { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Car(int quality, int performance, int speed, bool isBroken)
        {
            Quality = quality;
            Performance = performance;
            Speed = speed;
            IsBroken = isBroken;
        }
    }
}
