using System.Drawing;
using System.Text.RegularExpressions;

class Day8 {

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var lines = Helper.getInputAsCharArray(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));
        
        long sumA=0;
        long sumB=0;

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

        foreach (var freqKVP in allAntennas) {
            var freq=freqKVP.Key;
            var antennas=freqKVP.Value;

            foreach (var ant1 in antennas) {
                foreach (var ant2 in antennas) {
                    if (!ant1.Equals(ant2)) {
                        var newAnti = new Point(ant2.X + (ant2.X-ant1.X), ant2.Y + (ant2.Y-ant1.Y));
                        if (newAnti.X >= 0 && newAnti.Y >= 0 && newAnti.X < lines[0].Length && newAnti.Y < lines.Length && !allAnti.Contains(newAnti)) {
                            allAnti.Add(newAnti);
                        }
                    }
                }
            }
        }


        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {allAnti.Count}");
        Console.WriteLine($"result B: {sumB}");
    }
}