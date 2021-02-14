using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace River
{
    abstract class Fish
    {
        protected static Random Random = new Random();

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        protected int Step;

        public (int, int, int) StartCoordinates(int riverSize)
        {
            X = Random.Next(0, riverSize);
            Y = Random.Next(0, riverSize);
            Z = Random.Next(0, riverSize);
            return (X, Y, Z);
        }

        public (int, int, int) ChangeCoordinates()
        {
            X += Random.Next(-Step, Step + 1);
            Y += Random.Next(-Step, Step + 1);
            Z += Random.Next(-Step, Step + 1);
            return (X, Y, Z);
        }
    }
}
