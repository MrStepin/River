using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace River
{
    class Pike : Fish
    {

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public double Size { get; set; }
        

        public double Weight(double size)
        {
           Size += size;
            return Size;
        }

        private Random _random;

        public Pike(Random random)
        {
            _random = random;
        }

        public (int, int, int) StartCoordinates(Random random)
        {
            X = random.Next(0, 101);
            Y = random.Next(0, 101);
            Z = random.Next(0, 101);
            return (X, Y, Z);
        }

        public override (int, int, int) ChangeCoordinates(Random random)
        {
            X += random.Next(-5, 6);
            Y += random.Next(-5, 6);
            Z += random.Next(-5, 6);
            return (X, Y, Z);

        }
}
}
