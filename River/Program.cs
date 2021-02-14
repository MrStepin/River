using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace River
{
    class Program
    {
        static void Main(string[] args)
        {
            River river = new River();

            Console.WriteLine("Enter size of river:");
            int riverSize = Convert.ToInt32(Console.ReadLine());
            int fish = 0;
            int otherFish = 0;
            int maxSpeed = 0;

            river.CreateFishs();

            int qtyOfPikes = river.listOfPikes.Count;
            int qtyOfRudds = river.listOfRudds.Count;

            river.StartPositionOfPikes(riverSize);
            river.StartPositionOfRudds(riverSize);

            while ((qtyOfPikes > 0) && (qtyOfRudds > 0))
            {
                Thread.Sleep(500);

                river.ConditionForChangingCoordinates(fish, riverSize, maxSpeed);
                river.ConditionForLifeOfFished(fish, otherFish, riverSize, maxSpeed);

                river.ChangeCoordinatesOfFishes(river.listOfPikes, riverSize, river.MaxSpeedOfPike);
                river.ChangeCoordinatesOfFishes(river.listOfRudds, riverSize, river.MaxSpeedOfRudd);

                river.LifeOfPikes(riverSize);
                river.LifeOfRudds(riverSize);
                river.UpdateFishesInLists(river.listOfPikes, river.toDelListOfPikes, "pike");
                river.UpdateFishesInLists(river.listOfRudds, river.toDelListOfRudds, "rudd");
                river.BorningOfRudds();

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
