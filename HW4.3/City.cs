using System;
using System.Collections.Generic;
using System.Text;

namespace HW4._3
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CitySize Size { get; set; }
        public List<Building> Buildings { get; private set; } = new List<Building>();

        public int Population { get; set; }
        public int Happiness { get; set; } = 50; // Базове щастя
        public int TaxLevel { get; set; } = 10;  // 10% налог 

        // Допоміжні поля для розрахунку за 1 місяць
        public int FoodProduction { get; set; } = 0;
        public int MoneyProduction { get; set; } = 0;
        public int TaxBonusPercent { get; set; } = 0;

        public int MaxSlots
        {
            get
            {
                if (Size == CitySize.Small) return 12;
                if (Size == CitySize.Medium) return 20;
                return 36;
            }
        }

        public City(int id, string name, CitySize size, int population)
        {
            Id = id;
            Name = name;
            Size = size;
            Population = population;
        }

        public bool AddBuilding(Building building)
        {
            if (Buildings.Count >= MaxSlots)
            {
                Console.WriteLine($"[{Name}] Немає вільних слотів для {building.Name}!");
                return false;
            }
            Buildings.Add(building);
            return true;
        }

        // Симуляція одного місяця
        public void SimulateMonth(GovernmentType govType, ref int countryMoney, ref int countryFood)
        {
            // очищення
            FoodProduction = 0;
            MoneyProduction = 0;
            TaxBonusPercent = 0;

            // Застосовуємо бонуси будівель
            foreach (var b in Buildings)
                b.ApplyEffect(this);

            // Бонус від типу правління
            if (govType == GovernmentType.Dictatorship)
                Happiness += 20;

            // Щастя зменьшується від налогу
            Happiness -= (TaxLevel / 2);

            // Ограничиваем счастье от 0 до 100
            if (Happiness > 100) Happiness = 100;
            if (Happiness < 0) Happiness = 0;

            // Розрахунок їжі
            int foodNeeded = Population / 1000;
            int foodBalance = FoodProduction - foodNeeded;

            if (foodBalance >= 0)
            {
                countryFood += foodBalance; // Залишки їжі відходять в країну

                // якщо їжі вистачає населення зростає
                int growth = (Population * Happiness) / 5000;
                Population += growth;
            }
            else
            {
                Population += foodBalance * 500; // Вмирає 500 людей за кожну нехватку
                if (Population < 0) Population = 0;
                Happiness -= 10; // Голод понижує щастя
                Console.WriteLine($"[{Name}] Голод! Не хватает {-foodBalance} еды.");
            }

            // Розрахунок грошей
            if (Happiness > 0)
            {
                // Базовый дохід: 10 монет з кожної 1000 населення
                int baseIncome = (Population / 1000) * 10;

                // Застосовуємо налог
                int taxIncome = (baseIncome * (TaxLevel + TaxBonusPercent)) / 100;

                // Додаємо дохід від будівель
                int totalIncome = taxIncome + MoneyProduction;

                countryMoney += totalIncome;
                Console.WriteLine($"[{Name}] Зібрав податків: {totalIncome} грошей.");
            }
            else
            {
                Console.WriteLine($"[{Name}] Щастя 0! Місто не платить податки.");
            }

            Console.WriteLine($"[{Name}] Населення: {Population}, Щастя: {Happiness}, Їжа: {FoodProduction} (потрібно {foodNeeded})");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}
