using System.Drawing;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

class Day16 {

    class D16Point {
        internal char direction { get; set; }
        internal int X { get; set; }
        internal int Y { get; set; }
        internal long currentScore { get; set; }
        internal D16Point? previous { get; set; }
    }

    internal static void doit() {
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInputAsCharArray(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));

        long sumA=0;
        long sumB=0;

        var toRight = (char dir) => {
            switch (dir)
            {
                case '>': return 'v';
                case 'v': return '<';
                case '<': return '^';
                case '^': return '>';
            }
            throw new ArgumentException();
        };
        var toLeft = (char dir) => { return toRight(toRight(toRight(dir))); };

        var startX = 1;
        var startY = input.Length-2;

        int endX=0;
        int endY = 0;

        SortedDictionary<int, SortedDictionary<int,SortedDictionary<char, D16Point>>> calculated = new();

        Queue<D16Point> toCalculate = new();
        toCalculate.Enqueue(new D16Point{X=startX, Y=startY, direction = '>', currentScore=0, previous=null});

        while (toCalculate.TryDequeue(out var d16)) {
            if (input[d16.Y][d16.X] == '#')
                continue;

            if (!calculated.ContainsKey(d16.Y))
                calculated.Add(d16.Y, new());

            if (!calculated[d16.Y].ContainsKey(d16.X))
                calculated[d16.Y].Add(d16.X, new());

            if (!calculated[d16.Y][d16.X].ContainsKey(d16.direction))
                calculated[d16.Y][d16.X].Add(d16.direction, d16);
            else if (calculated[d16.Y][d16.X][d16.direction].currentScore < d16.currentScore)
                continue;
            else
                calculated[d16.Y][d16.X][d16.direction] = d16;

            if (input[d16.Y][d16.X] == 'E') {
                endX = d16.X;
                endY= d16.Y;
                continue;
            }

            int mx = d16.direction=='>' ? +1 : d16.direction == '<' ? -1 : 0;
            int my = d16.direction=='v' ? +1 : d16.direction == '^' ? -1 : 0;

            toCalculate.Enqueue(new D16Point { X = d16.X + mx, Y = d16.Y + my, currentScore = d16.currentScore + 1, direction = d16.direction, previous = d16 });
            toCalculate.Enqueue(new D16Point { X = d16.X, Y = d16.Y, currentScore = d16.currentScore + 1000, direction = toRight(d16.direction), previous = d16 });
            toCalculate.Enqueue(new D16Point { X = d16.X, Y = d16.Y, currentScore = d16.currentScore + 1000, direction = toLeft(d16.direction), previous = d16 });
        }

        var allEs = calculated[endY][endX];

        Console.WriteLine($"result A: {String.Join(",", allEs.Select(x => x.Value.currentScore).Min())}");
        Console.WriteLine($"result B: {sumB}");
    }
}