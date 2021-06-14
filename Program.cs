using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace kursovarabota
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = Console.ReadLine();
            List<string> galaxies = new List<string>();
            List<string> stars = new List<string>();
            List<string> planets = new List<string>();
            List<string> moons = new List<string>();
            List<object> galaxyList = new List<object>();
            Dictionary<string, List<object>> starDic = new Dictionary<string, List<object>>();
            Dictionary<string, List<object>> planetDic = new Dictionary<string, List<object>>();
            Dictionary<string, List<object>> moonDic = new Dictionary<string, List<object>>();

            Dictionary<string, int> stats = new Dictionary<string, int>(4);
            stats.Add("Galaxies", 0);
            stats.Add("Stars", 0);
            stats.Add("Planets", 0);
            stats.Add("Moons", 0);


            while (command != "exit")
            {
                string[] commandTokens = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (commandTokens[0] == "add")
                {
                    if (commandTokens[1] == "galaxy")
                    {
                        Stack<int> brackets = new Stack<int>();
                        string galaxyName = "";
                        string toSplit = "";
                        for (int i = 0; i < command.Length; i++)
                        {
                            if (command[i] == '[')
                            {
                                brackets.Push(i);
                            }
                            if (command[i] == ']')
                            {
                                int idx = brackets.Pop();
                                galaxyName = command.Substring(idx + 1, i - idx - 1);
                                galaxies.Add(galaxyName);
                                stats["Galaxies"]++;
                                i++;
                                toSplit = command.Substring(i, command.Length - i);

                            }
                        }
                        string[] glaxyData = toSplit.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        Galaxies galaxy = new Galaxies
                        {
                            age = glaxyData[1],
                            name = galaxyName,

                            type = glaxyData[0]
                        };
                        galaxyList.Add(galaxy);


                    }
                    else if (commandTokens[1] == "star")
                    {

                        Stack<int> brackets = new Stack<int>();
                        string starName = "";
                        string galaxyName = "";
                        string toSplit = "";

                        int counter = -1;
                        for (int i = 0; i < command.Length; i++)
                        {
                            if (command[i] == '[')
                            {
                                brackets.Push(i);
                            }

                            if (command[i] == ']')
                            {
                                int idx = brackets.Pop();
                                counter++;
                                if (counter == 0)
                                {

                                    galaxyName = command.Substring(idx + 1, i - idx - 1);
                                }
                                else
                                {
                                    starName = command.Substring(idx + 1, i - idx - 1);
                                    stars.Add(starName);
                                    stats["Stars"]++;
                                }

                                i++;
                                toSplit = command.Substring(i, command.Length - i);

                            }
                        }

                        string[] starData = toSplit.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        Stars star = new Stars
                        {
                            name = starName,
                            mass = double.Parse(starData[0]),
                            size = double.Parse(starData[1]),
                            temp = double.Parse(starData[2]),
                            luminosity = double.Parse(starData[3])
                        };



                        if (star.temp >= 30.00 && star.luminosity >= 30000 && star.mass >= 16
                             && star.size >= 6.6)
                        {
                            star.classification = 'O';
                        }

                        else if ((star.temp >= 10000 && star.temp < 30000)
                      && (star.luminosity >= 25 && star.luminosity < 30000)
                      && (star.mass >= 2.1 && star.mass < 16)
                             && (star.size >= 1.8 && star.size < 6.6))
                        {
                            star.classification = 'B';
                        }

                        else if ((star.temp >= 7500 && star.temp < 10000)
                      && (star.luminosity >= 5 && star.luminosity < 25)
                      && (star.mass >= 1.4 && star.mass < 2.1)
                             && (star.size >= 1.4 && star.size < 1.8))
                        {
                            star.classification = 'A';
                        }

                        else if ((star.temp >= 6000 && star.temp < 7500)
                      && (star.luminosity >= 1.5 && star.luminosity < 5)
                      && (star.mass >= 1.04 && star.mass < 1.4)
                             && (star.size >= 1.15 && star.size < 1.4))
                        {
                            star.classification = 'F';
                        }

                        else if ((star.temp >= 5200 && star.temp < 6000)
                     && (star.luminosity >= 0.6 && star.luminosity < 1.5)
                     && (star.mass >= 0.8 && star.mass < 1.04)
                            && (star.size >= 0.96 && star.size < 1.15))
                        {
                            star.classification = 'G';
                        }

                        else if ((star.temp >= 3700 && star.temp < 5200)
                    && (star.luminosity >= 0.08 && star.luminosity < 0.6)
                    && (star.mass >= 0.45 && star.mass < 0.8)
                           && (star.size >= 0.7 && star.size < 0.96))
                        {
                            star.classification = 'K';
                        }

                        else if ((star.temp >= 2400 && star.temp < 3700)
                  && (star.luminosity <= 0.08)
                  && (star.mass >= 0.08 && star.mass < 0.45)
                         && (star.size <= 0.7))
                        {
                            star.classification = 'M';
                        }

                        if (starDic.ContainsKey(galaxyName))
                        {
                            starDic[galaxyName].Add(star);

                        }
                        else
                        {

                            starDic.Add(galaxyName, new List<object>());
                            starDic[galaxyName].Add(star);
                        }

                    }

                    else if (commandTokens[1] == "planet")
                    {
                        Stack<int> brackets = new Stack<int>();
                        string planetName = "";
                        string starName = "";
                        string toSplit = "";
                        int counter = -1;
                        for (int i = 0; i < command.Length; i++)
                        {
                            if (command[i] == '[')
                            {
                                brackets.Push(i);
                            }
                            if (command[i] == ']')
                            {
                                int idx = brackets.Pop();
                                counter++;
                                if (counter == 0)
                                {

                                    starName = command.Substring(idx + 1, i - idx - 1);
                                }
                                else
                                {
                                    planetName = command.Substring(idx + 1, i - idx - 1);
                                    planets.Add(planetName);
                                    stats["Planets"]++;
                                }

                                i++;
                                toSplit = command.Substring(i, command.Length - i);

                            }
                        }
                        string[] planetData = toSplit.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        Planets planet = new Planets
                        {
                            name = planetName,
                            type = planetData[0],
                            supportLife = planetData[1]


                        };
                        if (planetDic.ContainsKey(starName))
                        {
                            planetDic[starName].Add(planet);

                        }
                        else
                        {

                            planetDic.Add(starName, new List<object>());
                            planetDic[starName].Add(planet);
                        }

                    }
                    else if (commandTokens[1] == "moon")
                    {
                        Stack<int> brackets = new Stack<int>();
                        string moonName = "";
                        string planetName = "";
                        string toSplit = "";
                        int counter = -1;
                        for (int i = 0; i < command.Length; i++)
                        {
                            if (command[i] == '[')
                            {
                                brackets.Push(i);
                            }
                            if (command[i] == ']')
                            {
                                int idx = brackets.Pop();
                                counter++;
                                if (counter == 0)
                                {

                                    planetName = command.Substring(idx + 1, i - idx - 1);
                                }
                                else
                                {
                                    moonName = command.Substring(idx + 1, i - idx - 1);
                                    moons.Add(moonName);
                                    stats["Moons"]++;
                                }

                                i++;
                                toSplit = command.Substring(i, command.Length - i);

                            }
                        }
                        string[] moonData = toSplit.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        Moons moon = new Moons
                        {
                            name = moonName
                        };

                        if (moonDic.ContainsKey(planetName))
                        {
                            moonDic[planetName].Add(moon);

                        }

                        else
                        {

                            moonDic.Add(planetName, new List<object>());
                            moonDic[planetName].Add(moon);
                        }
                    }


                }
                else if (commandTokens[0] == "list")
                {
                    if (commandTokens[1] == "galaxies")
                    {
                        Console.WriteLine("--- List of all researched galaxies ---");
                        Console.WriteLine(string.Join(",", galaxies));
                        Console.WriteLine("--- End of galaxies list ---");
                    }

                    else if (commandTokens[1] == "stars")
                    {
                        Console.WriteLine("--- List of all researched stars ---");
                        Console.WriteLine(string.Join(",", stars));
                        Console.WriteLine("--- End of stars list ---");
                    }

                    else if (commandTokens[1] == "planets")
                    {
                        Console.WriteLine("--- List of all researched planets ---");
                        Console.WriteLine(string.Join(",", planets));
                        Console.WriteLine("--- End of planets list ---");
                    }

                    else if (commandTokens[1] == "moons")
                    {
                        Console.WriteLine("--- List of all researched moons ---");
                        Console.WriteLine(string.Join(",", moons));
                        Console.WriteLine("--- End of moons list ---");
                    }

                }
                else if (commandTokens[0] == "stats")
                {
                    Console.WriteLine("--- Stats ---");
                    foreach (var kvp in stats)
                    {
                        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                    }

                    Console.WriteLine("--- End of stats ---");
                }



                else if (commandTokens[0] == "print")
                {
                    Stack<int> brackets = new Stack<int>();

                    string galaxyToPrint = "";


                    int counter = -1;
                    for (int i = 0; i < command.Length; i++)
                    {
                        if (command[i] == '[')
                        {
                            brackets.Push(i);
                        }
                        if (command[i] == ']')
                        {
                            int idx = brackets.Pop();
                            counter++;
                            if (counter == 0)
                            {

                                galaxyToPrint = command.Substring(idx + 1, i - idx - 1);
                            }
                        }
                    }

                    Console.WriteLine($"--- Data for {galaxyToPrint} galaxy ---");
                    foreach (Galaxies item in galaxyList)
                    {
                        if (item.name == galaxyToPrint)
                        {
                            Console.WriteLine($"Type: {item.type}");
                            Console.WriteLine($"Age: {item.age}");
                            Console.WriteLine("Stars:");
                            foreach (var kvp in starDic)
                            {
                                if (kvp.Key == galaxyToPrint)
                                {
                                    foreach (Stars star in kvp.Value)
                                    {
                                        Console.WriteLine($"    -   Name: {star.name}");
                                        Console.WriteLine($"        Class: {star.classification} ({star.mass}, {star.size}, {star.temp}, {star.luminosity})");
                                        Console.WriteLine($"        Planets:");


                                        foreach (var kvp2 in planetDic)
                                        {
                                            if (kvp2.Key == star.name)
                                            {
                                                foreach (Planets planet in kvp2.Value)
                                                {
                                                    Console.WriteLine($"           o Name: {planet.name}");
                                                    Console.WriteLine($"             Type: {planet.type}");
                                                    Console.WriteLine($"             Support life: {planet.supportLife}");
                                                    Console.WriteLine("             Moons:");
                                                    foreach (var kvp3 in moonDic)
                                                    {
                                                        if (kvp3.Key == planet.name)
                                                        {
                                                            foreach (Moons moon in kvp3.Value)
                                                            {

                                                                Console.WriteLine($"              § {moon.name}");
                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                        }

                                    }
                                }
                            }


                        }

                    }
                    Console.WriteLine("--- End of data for Milky way galaxy ---");
                }
                command = Console.ReadLine();
            }
        }

    }
}
