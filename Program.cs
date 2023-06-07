internal class Program
{
    private static void Main(string[] args)
    {
        #region AnonymousTypes
        var venus = new
        {
            Name = "Венера",
            Number = 1,
            EquatorLength = 38025
        };
        var earth = new
        {
            Name = "Земля",
            Number = 2,
            EquatorLength = 40075,
            PreviousPlanet = venus
        };
        var mars = new
        {
            Name = "Марс",
            Number = 3,
            EquatorLength = 21344,
            PreviousPlanet = earth
        };
        var venusCopy = new
        {
            Name = "Венера",
            Number = 1,
            EquatorLength = 38025
        };

        System.Console.WriteLine($"Планета '{venus.Name}', порядковый номер '{venus.Number}', длина экватора '{venus.EquatorLength}'");
        System.Console.WriteLine($"Планета '{earth.Name}', порядковый номер '{earth.Number}', длина экватора '{earth.EquatorLength}', предыдущая планета '{earth.PreviousPlanet.Name}'");
        System.Console.WriteLine($"Планета '{mars.Name}', порядковый номер '{mars.Number}', длина экватора '{mars.EquatorLength}', предыдущая планета '{mars.PreviousPlanet.Name}'");
        System.Console.WriteLine($"Планета '{venusCopy.Name}', порядковый номер '{venusCopy.Number}', длина экватора '{venusCopy.EquatorLength}'");
        System.Console.WriteLine($"venus.Equals(venusCopy): {venus.Equals(venusCopy)}");
        #endregion

        #region Tuples
        var catalog = new PlanetCatalog();
        var requests = new[] { "Земля", "Лимония", "Марс" };
        foreach (var request in requests)
        {
            var planet = catalog.GetPlanet(request);
            if (planet.Error is not null)
            {
                System.Console.WriteLine(planet.Error);
                continue;
            }
            System.Console.WriteLine($"Планета {request} по счету от солнца на {planet.Number} месте. Длина экватора {planet.EquatorLength}км.");
        }
        #endregion

        #region Lambda
        var countCall = 0;
        foreach (var request in requests)
        {
            var planet = catalog.GetPlanetLambda(request, name =>
            {
                countCall++;
                if (countCall > 2)
                {
                    countCall = 0;
                    return "Вы спрашиваете слишком часто";
                }
                return null;
            });
            if (planet.Error is not null)
            {
                System.Console.WriteLine(planet.Error);
                continue;
            }
            System.Console.WriteLine($"Планета {request} по счету от солнца на {planet.Number} месте. Длина экватора {planet.EquatorLength}км.");
        }
        #endregion

        #region Lambda*
        requests = new[] {"Земля", "Лимония", "Марс"};
        countCall = 0;
        foreach (var request in requests)
        {
            var planet = catalog.GetPlanetLambda(request, name =>
            {
                if(name == "Лимония")
                    return "Это запретная планета";
                return null;
            });
            if (planet.Error is not null)
            {
                System.Console.WriteLine(planet.Error);
                continue;
            }
            System.Console.WriteLine($"Планета {request} по счету от солнца на {planet.Number} месте. Длина экватора {planet.EquatorLength}км.");
        }
        #endregion
        Console.ReadKey();
    }
}

internal class Planet
{
    public string Name { get; set; }
    public int Number { get; set; }
    public int EquatorLength { get; set; }
    public Planet? PreviousPlanet { get; set; }

    public Planet(string name, int number, int equatorLength)
    {
        Name = name;
        Number = number;
        EquatorLength = equatorLength;
    }
}

internal class PlanetCatalog
{
    private List<Planet> _planets;
    private int countCall = 0;

    public PlanetCatalog()
    {
        var venus = new Planet("Венера", 2, 38025);
        var earth = new Planet("Земля", 3, 40075) { PreviousPlanet = venus };
        var mars = new Planet("Марс", 4, 21344) { PreviousPlanet = earth };
        _planets = new(3);
        _planets.Add(venus);
        _planets.Add(earth);
        _planets.Add(mars);
    }

    public (int? Number, int? EquatorLength, string? Error) GetPlanet(string name)
    {
        countCall++;
        if (countCall > 2)
        {
            countCall = 0;
            return (null, null, "Вы спрашиваете слишком часто");
        }

        var planet = _planets.FirstOrDefault(x => x.Name == name);
        if (planet is null)
            return (null, null, "Не удалось найти планету");

        return (planet.Number, planet.EquatorLength, null);
    }

    public (int? Number, int? EquatorLength, string? Error) GetPlanetLambda(string name, Func<string, string?> funcPlanetValidator)
    {
        string? error = funcPlanetValidator(name);
        if (error is not null)
            return (null, null, error);

        var planet = _planets.FirstOrDefault(x => x.Name == name);
        if (planet is null)
            return (null, null, "Не удалось найти планету");

        return (planet.Number, planet.EquatorLength, null);
    }
}