using System;
using System.Collections.Generic;


public class Menu
{
    static void Main()
    {
        int choice = 0;
        RuchDrogowy ruchDrogowy = new RuchDrogowy();
        ruchDrogowy.GenerateRandomVehicles();

        do
        {
            Console.WriteLine("System ruchu ulicznego w mieście:");
            Console.WriteLine("1. Wyszukaj pojazd po numerze rejestracyjnym");
            Console.WriteLine("2. Wyświetl gdzie jest teraz najwięcej i najmniej pojazdów");
            Console.WriteLine("3. Wyświetl listę wszystkich pojazdów");
            Console.WriteLine("4. Wyjdź z programu");
            Console.Write("Wpisz opcję: ");

            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Wpisz numer od 1 do 3");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Podaj numer rejestracyjny pojazdu: ");
                    int numerRejestracyjny = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        string vehicleLocation = ruchDrogowy.GetVehicleLocation(numerRejestracyjny);
                        Console.WriteLine(vehicleLocation);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 2:
                    ruchDrogowy.DisplayVehicleCounts();
                    break;
                case 3:
                    ruchDrogowy.DisplayAllVehicles();
                    break;
                case 4:
                    Console.WriteLine("Zamykanie programu..");
                    break;
                default:
                    Console.WriteLine("Wpisz numer od 1 do 4");
                    break;
            }

            Console.WriteLine();

        } while (choice != 4);
    }
}

public class Pojazd
{
    public int NumerRejestracyjny;
    public string CurrentUlica;
    public string CurrentDzielnica;

    public Pojazd(int numerRejestracyjny, string currentUlica, string currentDzielnica)
    {
        this.NumerRejestracyjny = numerRejestracyjny;
        this.CurrentUlica = currentUlica;
        this.CurrentDzielnica = currentDzielnica;
    }
}

public class Ulica
{
    public string NazwaUlicy;
    public string JakaDzielnica;
    public List<Pojazd> Pojazdy;


    public Ulica(string NazwaUlicy, string JakaDzielnica)
    {
        this.NazwaUlicy = NazwaUlicy;
        this.JakaDzielnica = JakaDzielnica;
        this.Pojazdy = new List<Pojazd>();
    }
}

public class Skrzyżowanie
{
    public string NazwaSkrzyżowania;
    public List<Ulica> UliceSkrzyżowania;

    public Skrzyżowanie(string nazwaSkrzyżowania)
    {
        this.NazwaSkrzyżowania = nazwaSkrzyżowania;
        this.UliceSkrzyżowania = new List<Ulica>();
    }
}

public class Dzielnica
{
    public string NazwaDzielnicy;
    public List<Ulica> UliceDzielnicy;

    public Dzielnica (string NazwaDzielnicy)
    {
        this.NazwaDzielnicy = NazwaDzielnicy;
        this.UliceDzielnicy = new List<Ulica>();
    }
}

public class RuchDrogowy
{
    public List<Skrzyżowanie> Skrzyżowania;
    public List<Dzielnica> Dzielnice;

    public RuchDrogowy()
    {
        this.Skrzyżowania = new List<Skrzyżowanie>();
        this.Dzielnice = new List<Dzielnica>();

        Ulica ulica1 = new Ulica("Wolna", "Retkinia");
        Ulica ulica2 = new Ulica("Krótka", "Retkinia");
        Ulica ulica3 = new Ulica("Piotrkowska", "Teofilów");
        Ulica ulica4 = new Ulica("Pabianicka", "Teofilów");
        Ulica ulica5 = new Ulica("Narutowicza", "Olechów");
        Ulica ulica6 = new Ulica("Limanowskiego", "Olechów");


        Dzielnica retkinia = new Dzielnica("Retkinia");
        retkinia.UliceDzielnicy.Add(ulica1);
        retkinia.UliceDzielnicy.Add(ulica2);

        Dzielnica teofilów = new Dzielnica("Teofilów");
        teofilów.UliceDzielnicy.Add(ulica3);
        teofilów.UliceDzielnicy.Add(ulica4);

        Dzielnica olechów = new Dzielnica("Olechów");
        olechów.UliceDzielnicy.Add(ulica5);
        olechów.UliceDzielnicy.Add(ulica6);

        Skrzyżowanie skrzyżowanie1 = new Skrzyżowanie("Wolna-Krótka");
        skrzyżowanie1.UliceSkrzyżowania.Add(ulica1);
        skrzyżowanie1.UliceSkrzyżowania.Add(ulica2);

        Skrzyżowanie skrzyżowanie2 = new Skrzyżowanie("Piotrkowska-Pabianicka");
        skrzyżowanie2.UliceSkrzyżowania.Add(ulica3);
        skrzyżowanie2.UliceSkrzyżowania.Add(ulica4);

        Skrzyżowanie skrzyżowanie3 = new Skrzyżowanie("Narutowicza-Limanowskiego");
        skrzyżowanie3.UliceSkrzyżowania.Add(ulica5);
        skrzyżowanie3.UliceSkrzyżowania.Add(ulica6);

        Dzielnice.Add(retkinia);
        Dzielnice.Add(teofilów);
        Dzielnice.Add(olechów);
        Skrzyżowania.Add(skrzyżowanie1);
        Skrzyżowania.Add(skrzyżowanie2);
        Skrzyżowania.Add(skrzyżowanie3);

    }

    public void GenerateRandomVehicles()
    {
        Random random = new Random();
        int numberOfVehicles = random.Next(30, 50);

        for (int i = 0; i < numberOfVehicles; i++)
        {
            int numerRejestracyjny = random.Next(1, 1000);

            Dzielnica randomDzielnica = Dzielnice[random.Next(Dzielnice.Count)];

            Ulica randomUlica = randomDzielnica.UliceDzielnicy[random.Next(randomDzielnica.UliceDzielnicy.Count)];

            string currentDzielnica = randomDzielnica.NazwaDzielnicy;
            string currentUlica = randomUlica.NazwaUlicy;

            Pojazd pojazd = new Pojazd(numerRejestracyjny, currentUlica, currentDzielnica);

            randomUlica.Pojazdy.Add(pojazd);
        }
    }

    public string GetVehicleLocation(int numerRejestracyjny)
    {
        foreach (Dzielnica dzielnica in Dzielnice)
        {
            foreach (Ulica ulica in dzielnica.UliceDzielnicy)
            {
                foreach (Pojazd pojazd in ulica.Pojazdy)
                {
                    if (pojazd.NumerRejestracyjny == numerRejestracyjny)
                    {
                        return $"Pojazd {numerRejestracyjny} jest na ulicy {ulica.NazwaUlicy} w dzielnicy {dzielnica.NazwaDzielnicy}.";
                    }
                }
            }
        }
        throw new Exception("Pojazd o podanym numerze rejestracyjnym nie został znaleziony.");
    }

    public void DisplayVehicleCounts()
    {
        Dictionary<string, int> vehicleCounts = new Dictionary<string, int>();

        foreach (Dzielnica dzielnica in Dzielnice)
        {
            foreach (Ulica ulica in dzielnica.UliceDzielnicy)
            {
                int count = ulica.Pojazdy.Count;

                if (!vehicleCounts.ContainsKey(ulica.NazwaUlicy))
                {
                    vehicleCounts[ulica.NazwaUlicy] = count;
                }
                else
                {
                    vehicleCounts[ulica.NazwaUlicy] += count;
                }
            }
        }

        foreach (Skrzyżowanie skrzyzowanie in Skrzyżowania)
        {
            int count = 0;

            foreach (Ulica ulica in skrzyzowanie.UliceSkrzyżowania)
            {
                if (vehicleCounts.ContainsKey(ulica.NazwaUlicy))
                {
                    count += vehicleCounts[ulica.NazwaUlicy];
                }
            }

            if (!vehicleCounts.ContainsKey(skrzyzowanie.NazwaSkrzyżowania))
            {
                vehicleCounts[skrzyzowanie.NazwaSkrzyżowania] = count;
            }
            else
            {
                vehicleCounts[skrzyzowanie.NazwaSkrzyżowania] += count;
            }
        }

        string mostVehiclesUlica = "";
        string leastVehiclesUlica = "";
        string mostVehiclesSkrzyzowanie = "";
        string leastVehiclesSkrzyzowanie = "";
        int maxCount = 0;
        int minCount = int.MaxValue;
        int maxCountSkrzyzowanie = 0;
        int minCountSkrzyzowanie = int.MaxValue;

        foreach (KeyValuePair<string, int> entry in vehicleCounts)
        {
            string location = entry.Key;
            int count = entry.Value;

            if (count > maxCount && !Skrzyżowania.Exists(s => s.NazwaSkrzyżowania == location))
            {
                maxCount = count;
                mostVehiclesUlica = location;
            }

            if (count < minCount && !Skrzyżowania.Exists(s => s.NazwaSkrzyżowania == location))
            {
                minCount = count;
                leastVehiclesUlica = location;
            }

            if (count > maxCountSkrzyzowanie && Skrzyżowania.Exists(s => s.NazwaSkrzyżowania == location))
            {
                maxCountSkrzyzowanie = count;
                mostVehiclesSkrzyzowanie = location;
            }

            if (count < minCountSkrzyzowanie && Skrzyżowania.Exists(s => s.NazwaSkrzyżowania == location))
            {
                minCountSkrzyzowanie = count;
                leastVehiclesSkrzyzowanie = location;
            }
        }
        Console.WriteLine($"Najwięcej pojazdów jest na ulicy: {mostVehiclesUlica} ({maxCount} pojazdów)");
        Console.WriteLine($"Najmniej pojazdów jest na ulicy: {leastVehiclesUlica} ({minCount} pojazdów)");
        Console.WriteLine($"Najwięcej pojazdów jest na skrzyżowaniu: {mostVehiclesSkrzyzowanie} ({maxCountSkrzyzowanie} pojazdów)");
        Console.WriteLine($"Najmniej pojazdów jest na skrzyżowaniu: {leastVehiclesSkrzyzowanie} ({minCountSkrzyzowanie} pojazdów)");
    }

    public void DisplayAllVehicles()
    {
        foreach (Dzielnica dzielnica in Dzielnice)
        {
            foreach (Ulica ulica in dzielnica.UliceDzielnicy)
            {
                foreach (Pojazd pojazd in ulica.Pojazdy)
                {
                    Console.WriteLine($"Numer rejestracyjny: {pojazd.NumerRejestracyjny}");
                }
            }
        }
    }
}



