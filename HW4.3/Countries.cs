using System;
using System.Collections.Generic;
using System.Text;

namespace HW4._3
{
    public class Country
    {
        static private int autoInc = 1;
        protected int Id;
        private void generateId()
        {
            this.Id = autoInc++;
        }
        public string Name { get; set; }
        public GovernmentType Government { get; set; }

        public int Money { get; set; }
        public int Food { get; set; }
        public int Happiness { get; set; } // Середній рівень по країні

        public List<City> Cities { get; private set; } = new List<City>();

        public Country(int id, string name, GovernmentType gov)
        {
            Id = id;
            Name = name;
            Government = gov;
            Money = 10000; // Стартовий капітал
            Food = 5000;   // Стартовий запас їжі
        }

        public void AddCity(City city)
        {
            Cities.Add(city);
            Console.WriteLine($"Місто {city.Name} приєднано до {Name}.");
        }
        public bool BuildInCity(City city, Building building)
        {
            if (Money < building.Price)
            {
                Console.WriteLine($"Недостатньо грошей для будівництва {building.Name} (потрібно {building.Price})");
                return false;
            }

            if (city.AddBuilding(building))
            {
                Money -= building.Price;
                Console.WriteLine($"У місті {city.Name} побудовано {building.Name}!");
                return true;
            }
            return false;
        }

        public void SimulateMonth()
        {
            Console.WriteLine($"\n=== МІСЯЦЬ (Тип правління: {Government}) ===");

            int totalFood = Food;
            int totalMoney = Money;
            int totalHappiness = 0;

            foreach (var city in Cities)
            {
                // Місто додає ресурси в загальний пул
                city.SimulateMonth(Government, ref totalMoney, ref totalFood);
                totalHappiness += city.Happiness;
            }

            Money = totalMoney;
            Food = totalFood;

            if (Cities.Count > 0)
                Happiness = totalHappiness / Cities.Count;

            Console.WriteLine($"\n[КАЗНА] Гроші: {Money}, Їжа: {Food}, Середнє щастя: {Happiness}");
            Console.WriteLine("==================================================");
        }
    }
}
