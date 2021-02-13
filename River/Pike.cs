using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace River
{
    class Pike : Fish
    {


        public double Size { get; set; }
        

        public double Weight(double size)
        {
           Size += size;
            return Size;
        }

        public Pike()
        {
            Step = 5;
        }
    }
}
