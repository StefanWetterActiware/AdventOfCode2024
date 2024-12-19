using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

class Day18 {

    class MyPoint {
        public int x;
        public int y;
        public int cost;
        public MyPoint? previous;
    }

    internal static void doit() {
        Regex dayNoR = new(@"\d*$");
        var lines = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value)).ToList();

        
        
        //         input = Helper.getBlocks("""
        //                                  """);

        char[][] grid = new char[71][];
        for (int i = 0; i < 71; i++) {
            grid[i] = new char[71];
        }

        for (int i = 0; i < 1023; i++) {
            var p = lines[i].Split(",");
            var x = int.Parse(p[0]);
            var y = int.Parse(p[1]);
            grid[y][x] = '#';
        }

        for (int i = 1023; i < lines.Count; i++) {
            var p = lines[i].Split(",");
            var x = int.Parse(p[0]);
            var y = int.Parse(p[1]);
            grid[y][x] = '#';

            Queue<MyPoint> toCalculate = new();
            SortedDictionary < int,  SortedDictionary<int, MyPoint> > calculated = new();
            toCalculate.Enqueue(new MyPoint { cost = 0, x = 0, y = 0 });

            while (toCalculate.TryDequeue(out MyPoint todo)) {
                if (todo.x < 0)
                    continue;
                if (todo.y < 0)
                    continue;
                if (todo.x > 70)
                    continue;
                if (todo.y > 70)
                    continue;
                if (grid[todo.y][todo.x] == '#')
                    continue;

                if (!calculated.ContainsKey(todo.x)) {
                    calculated.Add(todo.x, new());
                }
                if (calculated[todo.x].ContainsKey(todo.y) && calculated[todo.x][todo.y].cost <= todo.cost) {
                    continue;
                }
                if (!calculated[todo.x].ContainsKey(todo.y)) {
                    calculated[todo.x].Add(todo.y, todo);
                }

                if (todo.x == 71 && todo.y == 71)
                    continue;

                toCalculate.Enqueue(new MyPoint { x = todo.x + 1, y = todo.y, previous = todo, cost = todo.cost + 1 });
                toCalculate.Enqueue(new MyPoint { x = todo.x - 1, y = todo.y, previous = todo, cost = todo.cost + 1 });
                toCalculate.Enqueue(new MyPoint { x = todo.x, y = todo.y + 1, previous = todo, cost = todo.cost + 1 });
                toCalculate.Enqueue(new MyPoint { x = todo.x, y = todo.y - 1, previous = todo, cost = todo.cost + 1 });
            }

            if (i == 1024) {
                Console.WriteLine($"result A: {calculated[70][70].cost}");
            }
            if (!calculated.ContainsKey(70) || !calculated[70].ContainsKey(70)) {
                Console.WriteLine($"result B: {lines[i]}");
                return;
            }
        }

    }
}