using System.Drawing;
using System.Text.RegularExpressions;

class Day21 {
    class RobotPanic : Exception {}
    internal class KeyboardRobot(char[][] keys, KeyboardRobot next = null) {
        static private Point getInitPos(char[][] keys) {
            return getPos(keys, 'A');
        }

        static private Point getPos(char[][] keys, char c) {
            for (int y = 0; y < keys.Length; y++) {
                for (int x = 0; x < keys[0].Length; x++) {
                    if (keys[y][x] == c)
                        return new Point(x, y);
                }
            }
            
            throw new ArgumentException();
        }

        private Point currentPosition = getInitPos(keys);

        char currentKey {
            get {
                return keys[currentPosition.Y][currentPosition.X];
            }
        }

        internal void move(char direction) {
            currentPosition.X += direction == '>' ? 1 : direction == '<' ? -1 : 0;
            currentPosition.Y += direction == 'v' ? 1 : direction == '^' ? -1 : 0;
            if (currentKey == '#') {
                throw new RobotPanic();
            }
        }

        private SortedDictionary<string, string> fastestMovesCache = new();

        class CalcPoint {
            internal int X;
            internal int Y;
            internal CalcPoint previous;
            internal int currentCost = 0;
            internal char currentDirection;
        }

        private string movesNeededTo(char c) {
            var cacheKey = $"{currentKey}{c}";
            if (cacheKey == "37") return "<<^^";
            if (!fastestMovesCache.ContainsKey(cacheKey)) {
                fastestMovesCache.Add($"{currentKey}{c}", movesNeededToDirect(c));
            }
            currentPosition = getPos(keys, c);
            return fastestMovesCache[cacheKey];
        }

        private string movesNeededToDirect(char c) {
            var destPos = getPos(keys, c);

            if (destPos == currentPosition)
                return "";
            
            Queue<CalcPoint> toCalculate = new Queue<CalcPoint>();
            List<CalcPoint> calculated = new();
            toCalculate.Enqueue(new CalcPoint{X=currentPosition.X, Y=currentPosition.Y, currentCost = 0});

            while (toCalculate.TryDequeue(out CalcPoint point)) {
                if (!Helper.isOnGrid(keys, point.X, point.Y))
                    continue;
                if (keys[point.Y][point.X] == '#')
                    continue;
                
                if (calculated.Any(x => x.X == point.X && x.Y == point.Y && x.currentCost <= point.currentCost))
                    continue;
                calculated.Add(point);
                
                if (point.X == destPos.X && point.Y == destPos.Y) continue;
                
                toCalculate.Enqueue(new CalcPoint {X = point.X + 1 , Y = point.Y, currentCost = point.currentCost + 1, previous = point});
                toCalculate.Enqueue(new CalcPoint {X = point.X, Y = point.Y + 1, currentCost = point.currentCost + 1, previous = point});
                toCalculate.Enqueue(new CalcPoint {X = point.X - 1 , Y = point.Y, currentCost = point.currentCost + 1, previous = point});
                toCalculate.Enqueue(new CalcPoint {X = point.X, Y = point.Y - 1, currentCost = point.currentCost + 1, previous = point});
            }

            var shortestDestPoint = calculated.Single(x => x.X == destPos.X && x.Y == destPos.Y);
            string wayToDest = "";
            var previous = shortestDestPoint;
            var cur = shortestDestPoint.previous;
            while (true) {
                wayToDest = (previous.X -1 == cur.X ? ">" : previous.X +1 == cur.X ? "<" : previous.Y +1 == cur.Y ? "^" : "v") + wayToDest;
                
                if (cur.previous == null)
                    break;
                previous = cur;
                cur=cur.previous;
            }
            
            //Its all about the order: https://www.reddit.com/r/adventofcode/comments/1hja685/2024_day_21_here_are_some_examples_and_hints_for/
            //TODO
            var asArrayList = wayToDest.ToCharArray().ToList();
            asArrayList.Sort();
            asArrayList.Reverse();
            wayToDest = String.Join("", asArrayList);
            if (wayToDest.Contains(">")) {
                var l = wayToDest.Length;
                wayToDest = wayToDest.Replace(">", "").PadLeft(l, '>');
            }

            return wayToDest;
        }

        internal string whatIsNeededToPress(string s) {
            var res = "";
            foreach (var c in s) {
                res+=whatIsNeededToPress(c);
            }

            return res;
        }

        internal string whatIsNeededToPress(char c) {
            if (next != null) {

                var lala = next.whatIsNeededToPress(c);
                var res = "";
                foreach (var n in lala) {
                    res+=movesNeededTo(n) + 'A';
                }
                
                return res;
            }
            
            return movesNeededTo(c) + 'A';
        }

        internal void press() {
            if (next != null) {
                if (currentKey == 'A') {
                    next.press();
                } else {
                    next.move(currentKey);
                }
            } else {
                Console.Write(currentKey);
            }
        }
    }

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));
        input = """
                029A
                980A
                179A
                456A
                379A
                """.Split('\n');
        long sumA=0;
        long sumB=0;
        
        char[][] numericKb = new []{new []{'7', '8', '9'}, new []{'4', '5', '6'}, new []{'1', '2', '3'}, new []{'#', '0', 'A'}};
        char[][] directionKb = new[] { new[] { '#', '^', 'A' }, new[] { '<', 'v', '>' } };
        
        var numKb = new KeyboardRobot(numericKb);
        var dirK1 = new KeyboardRobot(directionKb, numKb);
        var dirK2 = new KeyboardRobot(directionKb, dirK1);

        //>^ -> >^A>^A>^A>^A>^A>^A>^A>^A>^A
        //^> -> ^>A^>A^>A^>A^>A^>A^>A^>A^>A
        
        foreach (var line in input) {
            Console.Write(line + ": ");
            Console.WriteLine(numKb.whatIsNeededToPress(line));

            var mustPress = dirK2.whatIsNeededToPress(line);
            var nos = System.Text.RegularExpressions.Regex.Match(line, "[1-9]\\d*");
            var numericPart = int.Parse(nos.Value);
            var length = mustPress.Length;
            Console.WriteLine($"{line}: Length: {length} (numeric Part: {numericPart})");
            sumA += numericPart*length;
        }
        
        //Don't get it. 5th example is 4 too long...
        //real result is too high as well: 223770

        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}