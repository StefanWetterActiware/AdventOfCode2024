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
                if (input[y][x] == 'E') end = new Point(x, y);
            }
        }

        Queue<RacePoint> toCalculate = new();
        List<RacePoint> calculatedRacePoints = new();
        
        toCalculate.Enqueue(new RacePoint{cheats=0, X = start.X, Y = start.Y});

        while (toCalculate.TryDequeue(out var rP)) {
            if (!Helper.isOnGrid(input, rP.X, rP.Y)) continue;
            if (input[rP.Y][rP.X] == '#') continue;
            
            if (rP.X == end.X && rP.Y == end.Y) {
                calculatedRacePoints.Add(rP);
                continue;
            }

            if (calculatedRacePoints.Any(x => x.X == rP.X && x.Y == rP.Y && x.cheats == rP.cheats && x.currentScore <= rP.currentScore)) {
                continue;
            }

            calculatedRacePoints.RemoveAll(x => x.X == rP.X && x.Y == rP.Y && x.cheats == rP.cheats);
            calculatedRacePoints.Add(rP);
            
            toCalculate.Enqueue(new RacePoint{cheats=0, X = rP.X+1, Y = rP.Y, currentScore = rP.currentScore+1, previous = rP});
            toCalculate.Enqueue(new RacePoint{cheats=0, X = rP.X-1, Y = rP.Y, currentScore = rP.currentScore+1, previous = rP});
            toCalculate.Enqueue(new RacePoint{cheats=0, X = rP.X, Y = rP.Y+1, currentScore = rP.currentScore+1, previous = rP});
            toCalculate.Enqueue(new RacePoint{cheats=0, X = rP.X, Y = rP.Y-1, currentScore = rP.currentScore+1, previous = rP});
        }

        var shortest = calculatedRacePoints.Single(x => x.X == end.X && x.Y == end.Y).currentScore;
        Console.WriteLine($"shortest: {shortest}");

        calculatedRacePoints.Sort((x, y) => x.currentScore.CompareTo(y.currentScore));
        for (int i = 0; i < calculatedRacePoints.Count; i++) {
            var pA = calculatedRacePoints[i];
            sumA += calculatedRacePoints.Skip(i+1).Count(x => Math.Abs(x.X - pA.X) + Math.Abs(x.Y-pA.Y) == 2 && Math.Abs(pA.currentScore-x.currentScore) > 101);
        }

        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}