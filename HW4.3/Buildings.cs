using System;
using System.Collections.Generic;
using System.Text;
using HW4._3;

namespace HW4._3
{
    public abstract class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public ResourceType ResourceType { get; set; }
        public int ResourceCount { get; set; }

        public Building(int id, string name, int price, ResourceType type, int count)
        {
            Id = id;
            Name = name;
            Price = price;
            ResourceType = type;
            ResourceCount = count;
        }

        // Еффект будівлі кожен місяць
        public virtual void ApplyEffect(City city)
        {
            if (ResourceType == ResourceType.Food)
                city.FoodProduction += ResourceCount;
            else if (ResourceType == ResourceType.Money)
                city.MoneyProduction += ResourceCount;
            else if (ResourceType == ResourceType.Happiness)
                city.Happiness += ResourceCount;
        }
    }

    public class Farm : Building
    {
        public Farm(int id) : base(id, "Farm", 1000, ResourceType.Food, 50) { }
    }

    public class Mine : Building
    {
        public Mine(int id) : base(id, "Mine", 1500, ResourceType.Money, 40) { }

        public override void ApplyEffect(City city)
        {
            base.ApplyEffect(city); // +40 грошиків
            city.Happiness -= 10;   // -10 щастя (бо нема грошиків)
        }
    }

    public class Market : Building
    {
        public Market(int id) : base(id, "Market", 800, ResourceType.Money, 20) { }
    }

    public class Bank : Building
    {
        public Bank(int id) : base(id, "Bank", 2000, ResourceType.Money, 0) { }

        //перевизначення класа
        public override void ApplyEffect(City city)
        {
            city.TaxBonusPercent += 5; // +5% до налогів
        }
    }

    public class Factory : Building
    {
        public Factory(int id) : base(id, "Factory", 2500, ResourceType.Money, 50) { }

        public override void ApplyEffect(City city)
        {
            base.ApplyEffect(city); // +50 грошиків
            city.Happiness -= 10;   // -10 щастя
        }
    }

    public class Theatre : Building
    {
        public Theatre(int id) : base(id, "Theatre", 1200, ResourceType.Happiness, 20) { }
    }

    public class Museum : Building
    {
        public Museum(int id) : base(id, "Museum", 1800, ResourceType.Happiness, 40) { }
    }
}
