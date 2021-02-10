using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace River
{
    class Program
    {
        static void Main(string[] args)
        {
            River river = new River();

            river.CreateFishs();

            int qtyOfPikes = river.listOfPikes.Count;
            int qtyOfRudds = river.listOfRudds.Count;

            river.StartPositionOfPikes();

            river.StartPositionOfRudds();

            while ((qtyOfPikes > 0) && (qtyOfRudds > 0))
            {
                Thread.Sleep(500);

                river.ChangeCoordinatesOfPikes();
                river.ChangeCoordinatesOfRudds();
                
                river.LifeOfPikes();
                river.LifeOfRudds();
                river.UpdateFishesInLists();

                qtyOfPikes = river.listOfPikes.Count;
                qtyOfRudds = river.listOfRudds.Count;
            }

            if ((qtyOfPikes == 0))
            {
                Console.WriteLine("All pikes are died");
            }
            else
            {
                Console.WriteLine("All rudds are died");
            }
            Console.ReadKey();
        }
    }
}
