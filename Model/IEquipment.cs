using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    // ? Might be Abstract? Check Class Diagram!
    // ! Italic Interfaces are just Interfaces, Not Abstract!
    public interface IEquipment
    {
        public int Quality { get; set; }
        public int Performance { get; set; }

        public int Speed { get; set; }
        public bool IsBroken { get; set; }
    }
}
