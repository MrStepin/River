using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace River
{
    class River
    {

        Random random = new Random();

        public List<Pike> listOfPikes = new List<Pike>();
        public List<Rudd> listOfRudds = new List<Rudd>();
        public List<Pike> ToDelListOfPikes = new List<Pike>();
        public List<Rudd> ToDelListOfRudds = new List<Rudd>();
        public List<Rudd> ToAddListOfRudds = new List<Rudd>();

        public void CreateFishs()
        {
            for (int i = 0; i < 10; i++)
            {
                listOfPikes.Add(new Pike(random));
                listOfRudds.Add(new Rudd(random));
            }
        }

        public void StartPositionOfPikes()
        {
        foreach (Pike pike in listOfPikes)
            {
                pike.StartCoordinates(random);
                pike.Size = 1;
            }
        }

        public void StartPositionOfRudds()
        {
            foreach (Rudd rudd in listOfRudds)
            {
                rudd.StartCoordinates(random);
            }
        }

        public void ChangeCoordinatesOfPikes()
        {
            foreach (Pike pike in listOfPikes)
            {
                if (((pike.X <= 94) && (pike.X >= 6) &&
                     (pike.Y <= 94) && (pike.Y >= 6) &&
                     (pike.Z <= 94) && (pike.Z >= 6)))
                {
                    pike.ChangeCoordinates(random);
                }
                else
                {
                    if (((pike.X >= 94) || 
                     (pike.Y >= 94) ||
                     (pike.Z >= 94)))
                    {
                        pike.X -= 1;
                        pike.Y -= 1;
                        pike.Z -= 1;
                    }
                    if (((pike.X <= 6) ||
                     (pike.Y <= 6) ||
                     (pike.Z <= 6)))
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
                if (((rudd.X <= 98) && (rudd.X >= 2) &&
                   (rudd.Y <= 98) && (rudd.Y >= 2) &&
                   (rudd.Z <= 98) && (rudd.Z >= 2)))
                {
                    rudd.ChangeCoordinates(random);
                }
                else
                {
                    if (((rudd.X >= 98) ||
                     (rudd.Y >= 98) ||
                     (rudd.Z >= 98)))
                    {
                        rudd.X -= 1;
                        rudd.Y -= 1;
                        rudd.Z -= 1;
                    }
                    if (((rudd.X <= 2) ||
                     (rudd.Y <= 2) ||
                     (rudd.Z <= 2)))
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
                    if ((pike.X < (rudd.X + 5)) && ((rudd.X - 5) < pike.X) &&
                        (pike.Y < (rudd.Y + 5)) && ((rudd.Y - 5) < pike.Y) &&
                        (pike.Z < (rudd.Z + 5)) && ((rudd.Z - 5) < pike.Z))
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
                        if ((rudd.X < (otherRudd.X + 2)) && ((otherRudd.X - 2) < rudd.X) &&
                        (rudd.Y < (otherRudd.Y + 2)) && ((otherRudd.Y - 2) < rudd.Y) &&
                        (rudd.Z < (otherRudd.Z + 2)) && ((otherRudd.Z - 2) < rudd.Z))
                        {
                            ToAddListOfRudds.Add(new Rudd(random));
                        }
                        else
                        {
                            ToDelListOfRudds.Add(rudd);
                        }

                    }
     
                }   
            }
        }

        public void UpdateFishesInLists()
        {
            foreach (Rudd rudd in ToDelListOfRudds)
            {
                foreach (Rudd realRudd in listOfRudds)
                if (rudd == realRudd)
                    {
                        listOfRudds.Remove(realRudd);
                        Console.WriteLine("Rudd is died");
                        Console.WriteLine($"Total rudds:{listOfRudds.Count}");
                    }
            }
            ToDelListOfRudds.Clear();

            foreach (Pike pike in ToDelListOfPikes)
            {
                foreach (Pike realPike in listOfPikes)
                    if (pike == realPike)
                    {
                        listOfPikes.Remove(realPike);
                        Console.WriteLine("Pike is died");
                        Console.WriteLine($"Total pikes:{listOfPikes.Count}");
                    }
            }
            ToDelListOfPikes.Clear();

            foreach (Rudd rudd in ToAddListOfRudds)
            {
                listOfRudds.Add(rudd);
                Console.WriteLine("Rudd was born");
                Console.WriteLine($"Total rudds:{listOfRudds.Count}");
                    
            }
            ToAddListOfRudds.Clear();
        }
    
    }
}
