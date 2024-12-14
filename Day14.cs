using System.Drawing;
using System.Text.RegularExpressions;

class Day14 {
    class Robot {
        internal int X { get; set; }
        internal int Y { get; set; }
        internal int mX { get; set; }
        internal int mY { get; set; }

        internal void move() {
            X += mX;
            Y += mY;
            if (X < 0) X += 101;
            if (Y < 0) Y += 103;
            if (X > 100) X -= 101;
            if (Y > 102) Y -= 103;
        }
    }
    
    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));
        
        long sumA=0;
        long sumB=0;

        List<Robot> robots = new();
        var x = new Regex("p=(\\d+),(\\d+) v=(-?\\d+),(-?\\d+)");
        foreach (var line in input) {
            var r = x.Match(line);
            if (r.Success) {
                robots.Add(new Robot{X=int.Parse(r.Groups[1].Value), Y=int.Parse(r.Groups[2].Value), mX= int.Parse(r.Groups[3].Value), mY=int.Parse(r.Groups[4].Value)});
            }
        }

        for (int i = 0; i < 100; i++) {
            foreach (var robot in robots) {
                robot.move();
            }
        }
        
        var quadrants = new int[] { 0, 0, 0, 0 };

        foreach (var robot in robots) {
            if (robot.Y < 51) {
                if (robot.X < 50) {
                    quadrants[0]++;
                } else if (robot.X > 50) {
                    quadrants[1]++;
                }
            } else if (robot.Y > 51) {
                if (robot.X < 50) {
                    quadrants[2]++;
                } else if (robot.X > 50) {
                    quadrants[3]++;
                }
            }
        }

        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {quadrants[0]*quadrants[1]*quadrants[2]*quadrants[3]}");
        Console.WriteLine($"result B: {sumB}");
    }
}