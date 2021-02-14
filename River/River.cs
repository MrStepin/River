using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace River
{
    class River
    {
        public List<Pike> listOfPikes = new List<Pike>();
        public List<Rudd> listOfRudds = new List<Rudd>();
        public List<Pike> toDelListOfPikes = new List<Pike>();
        public List<Rudd> toDelListOfRudds = new List<Rudd>();
        public List<Rudd> toAddListOfRudds = new List<Rudd>();
        private Random Random = new Random();
        Rudd newRudd = new Rudd();

        private int _maxSpeedOfPike = 5;
        private int _maxSpeedOfRudd = 2;

        public void CreateFishs()
        {
            for (int i = 0; i < 10; i++)
            {
                listOfPikes.Add(new Pike());
                listOfRudds.Add(new Rudd());
            }
        }

        public void StartPositionOfPikes(int riverSize)
        {
            foreach (Pike pike in listOfPikes)
            {
                pike.StartCoordinates(riverSize);
                pike.Size = 1;
            }
        }

        public void StartPositionOfRudds(int riverSize)
        {
            foreach (Rudd rudd in listOfRudds)
            {
                rudd.StartCoordinates(riverSize);
            }
        }

        public bool ConditionForChangingCoordinates(int fish, int riverSize)
        {
            return (fish <= (riverSize - _maxSpeedOfPike)) && (fish >= _maxSpeedOfPike);
        }

        public bool ConditionForLifeOfFished(int fish, int otherFish, int riverSize)
        {
            return (fish < (otherFish + _maxSpeedOfPike)) && ((otherFish - _maxSpeedOfPike) < fish);

        }

        public void ChangeCoordinatesOfFishes<T>(List<T> list, int riverSize) where T : Fish
        {
            foreach (T fish in list)
            {
                if (ConditionForChangingCoordinates(fish.X, riverSize) &&
                    ConditionForChangingCoordinates(fish.Y, riverSize) &&
                    ConditionForChangingCoordinates(fish.Z, riverSize))
                {
                    fish.ChangeCoordinates();
                }
                else
                {
                    if (((fish.X >= (riverSize - _maxSpeedOfPike)) ||
                     (fish.Y >= (riverSize - _maxSpeedOfPike)) ||
                     (fish.Z >= (riverSize - _maxSpeedOfPike))))
                    {
                        fish.X -= 1;
                        fish.Y -= 1;
                        fish.Z -= 1;
                    }
                    if (((fish.X <= _maxSpeedOfPike) ||
                     (fish.Y <= _maxSpeedOfPike) ||
                     (fish.Z <= _maxSpeedOfPike)))
                    {
                        fish.Y += 1;
                        fish.Z += 1;
                        fish.X += 1;
                    }
                    continue;
                }
            }
        }

        public void LifeOfPikes(int riverSize)
        {
            foreach (Pike pike in listOfPikes)
            {
                double endWeight = 0;
                foreach (Rudd rudd in listOfRudds)
                {
                    if (ConditionForLifeOfFished(pike.X, rudd.X, riverSize) &&
                        ConditionForLifeOfFished(pike.Y, rudd.Y, riverSize) &&
                        ConditionForLifeOfFished(pike.Z, rudd.Z, riverSize))
                    {
                        pike.Weight(0.25);
                        endWeight = +1;
                        toDelListOfRudds.Add(rudd);
                    }

                }
                if (endWeight == 0)
                {
                    pike.Weight(-0.25);
                }
                if (pike.Weight(0) == 0)
                {
                    toDelListOfPikes.Add(pike);
                }
            }
        }

        public void LifeOfRudds(int riverSize)
        {
            newRudd.ChangeCoordinates();
            foreach (Rudd rudd in listOfRudds)
            {
                foreach (Rudd otherRudd in listOfRudds)
                {
                    if (rudd != otherRudd)
                    {
                        if (ConditionForLifeOfFished(rudd.X, otherRudd.X, riverSize) &&
                            ConditionForLifeOfFished(rudd.Y, otherRudd.Y, riverSize) &&
                            ConditionForLifeOfFished(rudd.Z, otherRudd.Z, riverSize))
                        {
                            toAddListOfRudds.Add(newRudd);
                        }
                        continue;
                    }
                    continue;
                }
            }
            toDelListOfRudds.Add(listOfRudds[Random.Next(0,listOfRudds.Count)]);
        }

        public void UpdateFishesInLists<T>(List<T> list, List<T> listOfFishesToDel, string name) where T:Fish
        {
            foreach (T fish in listOfFishesToDel)
            {
                 list.Remove(fish);
                 Console.WriteLine($"{name} is died");
            }
            listOfFishesToDel.Clear();
            Console.WriteLine($"Total {name}s:{list.Count}");
        }

        public void BorningOfRudds()
        {
            foreach (Rudd rudd in toAddListOfRudds)
            {
                listOfRudds.Add(rudd);
                Console.WriteLine("Rudd was born");
            }
            toAddListOfRudds.Clear();
        }
    }
}
