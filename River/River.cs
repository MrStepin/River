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

        int maxSpeedOfPike = 5;
        int maxSpeedOfRudd = 2;

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
           if ((fish <= (riverSize - maxSpeedOfPike)) && (fish >= maxSpeedOfPike))
            {
                return true;
            }
           else
            {
                return false;
            }
        }

        public bool ConditionForLifeOfFished(int fish, int otherFish, int riverSize)
        {
            if ((fish < (otherFish + maxSpeedOfPike)) && ((otherFish - maxSpeedOfPike) < fish))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ChangeCoordinatesOfPikes(int riverSize)
        {
            foreach (Pike pike in listOfPikes)
            {
                if (ConditionForChangingCoordinates(pike.X, riverSize) && 
                    ConditionForChangingCoordinates(pike.Y, riverSize) && 
                    ConditionForChangingCoordinates(pike.Z, riverSize))
                {
                    pike.ChangeCoordinates();
                }
                else
                {
                    if (((pike.X >= (riverSize - maxSpeedOfPike)) || 
                     (pike.Y >= (riverSize - maxSpeedOfPike)) ||
                     (pike.Z >= (riverSize - maxSpeedOfPike))))
                    {
                        pike.X -= 1;
                        pike.Y -= 1;
                        pike.Z -= 1;
                    }
                    if (((pike.X <= maxSpeedOfPike) ||
                     (pike.Y <= maxSpeedOfPike) ||
                     (pike.Z <= maxSpeedOfPike)))
                    {
                        pike.X += 1;
                        pike.Y += 1;
                        pike.Z += 1;
                    }
                    continue;
                }

            }
        }

        public void ChangeCoordinatesOfRudds(int riverSize)
        {
            foreach (Rudd rudd in listOfRudds)
            {
                if (ConditionForChangingCoordinates(rudd.X, riverSize) &&
                    ConditionForChangingCoordinates(rudd.Y, riverSize) &&
                    ConditionForChangingCoordinates(rudd.Z, riverSize))
                {
                    rudd.ChangeCoordinates();
                }
                else
                {
                    if (((rudd.X >= riverSize - maxSpeedOfRudd) ||
                     (rudd.Y >= riverSize - maxSpeedOfRudd) ||
                     (rudd.Z >= riverSize - maxSpeedOfRudd)))
                    {
                        rudd.X -= 1;
                        rudd.Y -= 1;
                        rudd.Z -= 1;
                    }
                    if (((rudd.X <= maxSpeedOfRudd) ||
                     (rudd.Y <= maxSpeedOfRudd) ||
                     (rudd.Z <= maxSpeedOfRudd)))
                    {
                        rudd.X += 1;
                        rudd.Y += 1;
                        rudd.Z += 1;
                    }
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
                            toAddListOfRudds.Add(new Rudd());
                        }
                    }
                }
            }
            toDelListOfRudds.Add(listOfRudds[Random.Next(0,listOfRudds.Count)]);
        }

        public void UpdateFishesInLists()
        {
            foreach (Rudd rudd in toDelListOfRudds)
                    {
                        listOfRudds.Remove(rudd);
                        Console.WriteLine("Rudd is died");
                    }
            toDelListOfRudds.Clear();
            Console.WriteLine($"Total rudds:{listOfRudds.Count}");

            foreach (Pike pike in toDelListOfPikes)
            {
                        listOfPikes.Remove(pike);
                        Console.WriteLine("Pike is died");       
            }
            toDelListOfPikes.Clear();
            Console.WriteLine($"Total pikes:{listOfPikes.Count}");

            foreach (Rudd rudd in toAddListOfRudds)
            {
                listOfRudds.Add(rudd);
                Console.WriteLine("Rudd was born");     
            }
            toAddListOfRudds.Clear();
            Console.WriteLine($"Total rudds:{listOfRudds.Count}");
        }
    
    }
}
