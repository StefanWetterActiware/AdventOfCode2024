using System.Drawing;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

class Day8 {

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var lines = Helper.getInputAsCharArray(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value), false);

        SortedDictionary<char, List<Point>> allAntennas = new();
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines.First().Length; x++)
            {
                var ant = lines[y][x];
                if (ant != '.') {
                    if (!allAntennas.ContainsKey(ant))
                        allAntennas.Add(ant,new());
                    allAntennas[ant].Add(new(x,y));
                }
            }
        }

        List<Point> allAnti = new();
        List<Point> allAntiB = new();

        foreach (var freqKVP in allAntennas) {
            var freq=freqKVP.Key;
            var antennas=freqKVP.Value;

            foreach (var ant1 in antennas) {
                foreach (var ant2 in antennas) {
                    if (!ant1.Equals(ant2)) {
                        var newAnti = new Point(ant2.X + (ant2.X-ant1.X), ant2.Y + (ant2.Y-ant1.Y));
                        if (Helper.isOnGrid(lines, newAnti.X, newAnti.Y) && !allAnti.Contains(newAnti)) {
                            allAnti.Add(newAnti);
                        }

                        do {
                            if (Helper.isOnGrid(lines, newAnti.X, newAnti.Y)) {
                                if (!allAntiB.Contains(newAnti)) allAntiB.Add(newAnti);
                                newAnti = new Point(newAnti.X + (ant2.X-ant1.X), newAnti.Y + (ant2.Y-ant1.Y));
                            } else
                                break;
                        } while (true);

                        newAnti = new Point(ant1.X - (ant2.X-ant1.X), ant1.Y - (ant2.Y-ant1.Y));
                        do {
                            if (Helper.isOnGrid(lines, newAnti.X, newAnti.Y)) {
                                if (!allAntiB.Contains(newAnti)) allAntiB.Add(newAnti);
                                newAnti = new Point(newAnti.X - (ant2.X-ant1.X), newAnti.Y - (ant2.Y-ant1.Y));
                            } else
                                break;
                        } while (true);
                    }
                }
            }
        }

        foreach (var item in allAntennas.Where(x => x.Value.Count>1)) {
            item.Value.ForEach(x => {if (!allAntiB.Contains(x)) allAntiB.Add(x);});
        }
        


        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {allAnti.Count}");
        Console.WriteLine($"result B: {allAntiB.Count}");

    }
}