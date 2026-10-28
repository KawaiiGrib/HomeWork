using System;
namespace HW4._3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Створюємо країну
            Country myCountry = new Country(1, "Україна", GovernmentType.Democracy);

            // Створюємо міста
            City kyiv = new City(1, "Київ", CitySize.Large, 5000);
            City lviv = new City(2, "Львів", CitySize.Medium, 3000);
            City odesa = new City(3, "Одеса", CitySize.Small, 2000);

            myCountry.AddCity(kyiv);
            myCountry.AddCity(lviv);
            myCountry.AddCity(odesa);

            // Будуємо доміки
            Console.WriteLine("\n--- Будівництво ---");
            myCountry.BuildInCity(kyiv, new Theatre(1)); 
            myCountry.BuildInCity(kyiv, new Market(2));  
            myCountry.BuildInCity(kyiv, new Bank(3));    

            myCountry.BuildInCity(lviv, new Farm(4));   
            myCountry.BuildInCity(lviv, new Museum(5));  

            myCountry.BuildInCity(odesa, new Mine(6));   
            myCountry.BuildInCity(odesa, new Factory(7));

            // Запускаемо симуляцію
            for (int month = 1; month <= 3; month++)
            {
                Console.WriteLine($"\n>>>> МІСЯЦЬ {month} <<<<");
                myCountry.SimulateMonth();
            }

            Console.ReadLine();
        }
    }
}
