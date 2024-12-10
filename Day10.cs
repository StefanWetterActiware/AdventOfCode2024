using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

class Day10 {

    internal static int count9(char[][] input, int x, int y, List<Point> visited, List<Point> visitedTrailheads, bool ignorevisited) {
        Point curPoint = new Point(x, y);
        int curVal = int.Parse($"{input[y][x]}");
        if (curVal == 9) {
            if (ignorevisited) return 1;
            if (!visitedTrailheads.Contains(curPoint)) {
                visitedTrailheads.Add(curPoint);
                return 1;
            }
            return 0;
        }

        var newV = new List<Point>();
        newV.AddRange(visited);
        newV.Add(curPoint);

        var res = 0;
        for (int i = 1; i < 5; i++) {
            int mx=0;
            int my=0;
            if (i==1) {mx=0; my=1;}
            if (i==2) {mx=0; my=-1;}
            if (i==3) {mx=1; my=0;}
            if (i==4) {mx=-1; my=0;}
            if (Helper.isOnGrid(input, x+mx, y+my)) {
                var steig = int.Parse($"{input[y+my][x+mx]}") - curVal;
                if (steig > 0 && steig < 2 && !visited.Contains(new Point(x+mx, y+my))) {
                    res += count9(input, x+mx, y+my, newV, visitedTrailheads, ignorevisited);
                }
            }
        }
        return res;
    }

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInputAsCharArray(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value), false);
        
        var trailheads = new List<Point>();

        for (int y = 0; y < input.Length; y++) {
            for (int x = 0; x < input[0].Length; x++)
            {
                if (input[y][x] == '0'){
                    trailheads.Add(new Point(x,y));
                }
            }
        }

        var trailscores = new Dictionary<Point, int>();
        var trailscoresB = new Dictionary<Point, int>();

        foreach (var t in trailheads) {
            var dummy= new List<Point>();
            int score = count9(input, t.X, t.Y, new(), dummy, false);
            trailscores.Add(t, score);
        }
        foreach (var t in trailheads) {
            var dummy= new List<Point>();
            int score = count9(input, t.X, t.Y, new(), dummy, true);
            trailscoresB.Add(t, score);
        }
        
        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {trailscores.Values.Sum()}");
        Console.WriteLine($"result B: {trailscoresB.Values.Sum()}");
    }
}