using System.Drawing;
using System.Text.RegularExpressions;

class Day20 {
    class RacePoint {
        public int cheats = 0;
        public int X;
        public int Y;
        public RacePoint previous;
        public int currentScore;
    }

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInputAsCharArray(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));
        
        long sumA=0;
        long sumB=0;

        Point start = new();
        Point end = new();
        for (int y = 0; y < input.Length; y++) {
            for (int x = 0; x < input[0].Length; x++) {
                if (input[y][x] == 'S') start = new Point(x, y);
                if (input[y][x] == 'E') start = new Point(x, y);
            }
        }

        Queue<RacePoint> toCalculate = new();
        List<RacePoint> calculatedRacePoints = new();
        
        toCalculate.Enqueue(new RacePoint{cheats=0, X = start.X, Y = start.Y});

        while (toCalculate.TryDequeue(out var rP)) {
            if (rP.X == end.X && rP.Y == end.Y) {
                calculatedRacePoints.Add(rP);
                continue;
            }

            if (calculatedRacePoints.Any(x => x.X == rP.X && x.Y == rP.Y && x.cheats == rP.cheats && x.currentScore <= rP.cheats)) {
                continue;
            }

            calculatedRacePoints.RemoveAll(x => x.X == rP.X && x.Y == rP.Y && x.cheats == rP.cheats);
            calculatedRacePoints.Add(rP);
        }

        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}