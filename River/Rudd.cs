using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace River
{
    class Rudd : Fish
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        private Random _random;

        public Rudd(Random random)
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
            X += random.Next(-2, 3);
            Y += random.Next(-2, 3);
            Z += random.Next(-2, 3);
            return (X, Y, Z);

        }
    }
}
