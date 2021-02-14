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

        public int MaxSpeedOfPike = 5;
        public int MaxSpeedOfRudd = 2;

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

        public bool ConditionForChangingCoordinates(int fish, int riverSize, int _maxSpeed)
        {
            return (fish <= (riverSize - _maxSpeed)) && (fish >= _maxSpeed);
        }

        public bool ConditionForLifeOfFished(int fish, int otherFish, int riverSize, int _maxSpeed)
        {
            return (fish < (otherFish + _maxSpeed)) && ((otherFish - _maxSpeed) < fish);

        }

        public void ChangeCoordinatesOfFishes<T>(List<T> list, int riverSize, int _maxSpeed) where T : Fish
        {
            foreach (T fish in list)
            {
                if (ConditionForChangingCoordinates(fish.X, riverSize, _maxSpeed) &&
                    ConditionForChangingCoordinates(fish.Y, riverSize, _maxSpeed) &&
                    ConditionForChangingCoordinates(fish.Z, riverSize, _maxSpeed))
                {
                    fish.ChangeCoordinates();
                }
                else
                {
                    if (((fish.X >= (riverSize - _maxSpeed)) ||
                     (fish.Y >= (riverSize - _maxSpeed)) ||
                     (fish.Z >= (riverSize - _maxSpeed))))
                    {
                        fish.X -= 1;
                        fish.Y -= 1;
                        fish.Z -= 1;
                    }
                    if (((fish.X <= _maxSpeed) ||
                     (fish.Y <= _maxSpeed) ||
                     (fish.Z <= _maxSpeed)))
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
                    if (ConditionForLifeOfFished(pike.X, rudd.X, riverSize, MaxSpeedOfPike) &&
                        ConditionForLifeOfFished(pike.Y, rudd.Y, riverSize, MaxSpeedOfPike) &&
                        ConditionForLifeOfFished(pike.Z, rudd.Z, riverSize, MaxSpeedOfPike))
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
            foreach (Rudd rudd in listOfRudds)
            {
                foreach (Rudd otherRudd in listOfRudds)
                {
                    if (rudd != otherRudd)
                    {
                        if (ConditionForLifeOfFished(rudd.X, otherRudd.X, riverSize, MaxSpeedOfRudd) &&
                            ConditionForLifeOfFished(rudd.Y, otherRudd.Y, riverSize, MaxSpeedOfRudd) &&
                            ConditionForLifeOfFished(rudd.Z, otherRudd.Z, riverSize, MaxSpeedOfRudd))
                        {
                            newRudd.ChangeCoordinates();
                            toAddListOfRudds.Add(newRudd);
                        }
                    }
                }
            }
            toDelListOfRudds.Add(listOfRudds[Random.Next(0,(listOfRudds.Count))]);
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
