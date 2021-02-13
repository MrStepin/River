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
        public List<Pike> ToDelListOfPikes = new List<Pike>();
        public List<Rudd> ToDelListOfRudds = new List<Rudd>();
        public List<Rudd> ToAddListOfRudds = new List<Rudd>();

        private Random Random = new Random();
        private int riverSize = 30;
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

        public void StartPositionOfPikes()
        {
        foreach (Pike pike in listOfPikes)
            {
                pike.StartCoordinates();
                pike.Size = 1;
            }
        }

        public void StartPositionOfRudds()
        {
            foreach (Rudd rudd in listOfRudds)
            {
                rudd.StartCoordinates();
            }
        }

        public void ChangeCoordinatesOfPikes()
        {
            foreach (Pike pike in listOfPikes)
            {
                if (((pike.X <= (riverSize - maxSpeedOfPike)) && (pike.X >= maxSpeedOfPike) &&
                     (pike.Y <= (riverSize - maxSpeedOfPike)) && (pike.Y >= maxSpeedOfPike) &&
                     (pike.Z <= (riverSize - maxSpeedOfPike)) && (pike.Z >= maxSpeedOfPike)))
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

        public void ChangeCoordinatesOfRudds()
        {
            foreach (Rudd rudd in listOfRudds)
            {
                if (((rudd.X <= riverSize- maxSpeedOfRudd) && (rudd.X >= maxSpeedOfRudd) &&
                   (rudd.Y <= riverSize - maxSpeedOfRudd) && (rudd.Y >= maxSpeedOfRudd) &&
                   (rudd.Z <= riverSize - maxSpeedOfRudd) && (rudd.Z >= maxSpeedOfRudd)))
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

        public void LifeOfPikes()
        {
            foreach (Pike pike in listOfPikes)
            {
                double endWeight = 0;
                foreach (Rudd rudd in listOfRudds)
                {
                    if ((pike.X < (rudd.X + maxSpeedOfPike)) && ((rudd.X - maxSpeedOfPike) < pike.X) &&
                        (pike.Y < (rudd.Y + maxSpeedOfPike)) && ((rudd.Y - maxSpeedOfPike) < pike.Y) &&
                        (pike.Z < (rudd.Z + maxSpeedOfPike)) && ((rudd.Z - maxSpeedOfPike) < pike.Z))
                    {
                        pike.Weight(0.25);
                        endWeight = +1;
                        ToDelListOfRudds.Add(rudd);
                    }

                }
                if (endWeight == 0)
                {
                    pike.Weight(-0.25);
                }
                if (pike.Weight(0) == 0)
                {
                    ToDelListOfPikes.Add(pike);
                }
            }
        }

        public void LifeOfRudds()
        {
            foreach (Rudd rudd in listOfRudds)
            {
                foreach (Rudd otherRudd in listOfRudds)
                {
                    if (rudd != otherRudd)
                    {
                        if ((rudd.X < (otherRudd.X + maxSpeedOfRudd)) && ((otherRudd.X - maxSpeedOfRudd) < rudd.X) &&
                        (rudd.Y < (otherRudd.Y + maxSpeedOfRudd)) && ((otherRudd.Y - maxSpeedOfRudd) < rudd.Y) &&
                        (rudd.Z < (otherRudd.Z + maxSpeedOfRudd)) && ((otherRudd.Z - maxSpeedOfRudd) < rudd.Z))
                        {
                            ToAddListOfRudds.Add(new Rudd());
                        }
                    }
                }
            }
            ToDelListOfRudds.Add(listOfRudds[Random.Next(0,listOfRudds.Count)]);
        }

        public void UpdateFishesInLists()
        {
            foreach (Rudd rudd in ToDelListOfRudds)
                    {
                        listOfRudds.Remove(rudd);
                        Console.WriteLine("Rudd is died");
                    }
            ToDelListOfRudds.Clear();
            Console.WriteLine($"Total rudds:{listOfRudds.Count}");

            foreach (Pike pike in ToDelListOfPikes)
            {
                        listOfPikes.Remove(pike);
                        Console.WriteLine("Pike is died");       
            }
            ToDelListOfPikes.Clear();
            Console.WriteLine($"Total pikes:{listOfPikes.Count}");

            foreach (Rudd rudd in ToAddListOfRudds)
            {
                listOfRudds.Add(rudd);
                Console.WriteLine("Rudd was born");     
            }
            ToAddListOfRudds.Clear();
            Console.WriteLine($"Total rudds:{listOfRudds.Count}");
        }
    
    }
}
