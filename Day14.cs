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

        internal int DistFromMiddle {
            get {
                return Math.Abs(50 - X) + Math.Abs(51 - Y);
            }
        }
    }
    
    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));
        
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

        var draw = (long num = -1) => {
            Console.Clear();
            foreach (var bot in robots.Where(bot=>bot.Y>20)) {
                if (bot.X < Console.BufferWidth && bot.Y-20 < Console.BufferHeight) {
                    Console.SetCursorPosition(bot.X, bot.Y-20);
                    Console.Write('#');
                }
            }
            Console.SetCursorPosition(0,Console.BufferHeight - 1);
            Console.Write("Enter für weiter");
            if (num > -1) Console.Write($" Runde: {num}");
            Console.ReadLine();
        };

        var isTree = () => {
            var q = robots.Where(bot => bot.X == 50).Select(bot=>bot.Y).Distinct().ToList();
            if (!q.Any()) return false;
            
            q.Sort();
            var r = q.Where(y => y > 51).ToList();
            q = q.Where(y => y <= 51).ToList();
            if (!q.Any()) return false;
            if (!r.Any()) return false;
            if (q.Count() < 15) return false;
            if (r.Count() < 15) return false;

            q.Reverse();
            if (r.Skip(15).Take(1).Single() == 66 && q.Skip(15).Take(1).Single() == 37) {
                draw();
                return true;
            }

            //Default
            return false;
        };

        int minSumOfAll = 100000;
        sumB = 100;
        while (true) {
            int sumAllDist = robots.Select(bot => bot.DistFromMiddle).Sum();
            Console.WriteLine($"No: {sumB}, SumOfAllDienstances: {sumAllDist}");

            if (sumAllDist < minSumOfAll) {
                minSumOfAll = sumAllDist;
                draw(sumB);
            } else if (sumAllDist < 10000) {
                draw(sumB);
            }
            
            if (isTree()) break;
            
            robots.ForEach(robot => robot.move());
            sumB++;
        }
        //Result found by manually watching those found-before-low-distance-record-pictures... :)
        Console.WriteLine($"result B: {sumB}");
    }
}