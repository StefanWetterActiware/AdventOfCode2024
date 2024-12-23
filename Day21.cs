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
            
            string vertical;
            if (destPos.Y > currentPosition.Y) {
                vertical = new string('v', destPos.Y - currentPosition.Y);
            } else {
                vertical = new string('^', currentPosition.Y - destPos.Y);
            }

            string horizontal;
            if (destPos.X > currentPosition.X) {
                horizontal = new string('>', destPos.X - currentPosition.X);
            } else {
                horizontal = new string('<', currentPosition.X - destPos.X);
            }

            if (horizontal.Contains('<') && destPos.X == 0 && currentPosition.Y == getPos(keys, '#').Y) {
                return vertical + horizontal;
            } else if (horizontal.Contains('<')) {
                return horizontal + vertical;
            } else if (horizontal.Contains('>') && currentPosition.X == 0 && destPos.Y == getPos(keys, '#').Y) {
                return horizontal + vertical;
            } else if (horizontal.Contains('>')) {
                return vertical + horizontal;
            } else
                return vertical + horizontal;

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
        //input = """
        //        029A
        //        980A
        //        179A
        //        456A
        //        379A
        //        """.Split('\n').Select(x => x.Trim('\r'));
        long sumA=0;
        long sumB=0;
        
        char[][] numericKb = new []{new []{'7', '8', '9'}, new []{'4', '5', '6'}, new []{'1', '2', '3'}, new []{'#', '0', 'A'}};
        char[][] directionKb = new[] { new[] { '#', '^', 'A' }, new[] { '<', 'v', '>' } };
        
        var numKb = new KeyboardRobot(numericKb);
        var dirK1 = new KeyboardRobot(directionKb, numKb);
        var dirK2 = new KeyboardRobot(directionKb, dirK1);
        var dirK3 = new KeyboardRobot(directionKb, dirK2);
        var dirK4 = new KeyboardRobot(directionKb, dirK3);
        var dirK5 = new KeyboardRobot(directionKb, dirK4);
        var dirK6 = new KeyboardRobot(directionKb, dirK5);
        var dirK7 = new KeyboardRobot(directionKb, dirK6);
        var dirK8 = new KeyboardRobot(directionKb, dirK7);
        var dirK9 = new KeyboardRobot(directionKb, dirK8);
        var dirK10 = new KeyboardRobot(directionKb, dirK9);

        foreach (var line in input) {
            //Console.Write(line + ": ");
            //Console.WriteLine(numKb.whatIsNeededToPress(line));

            var mustPress1 = dirK1.whatIsNeededToPress(line);
            var mustPress = dirK2.whatIsNeededToPress(line);
            var mustPress3 = dirK3.whatIsNeededToPress(line);
            var mustPress4 = dirK4.whatIsNeededToPress(line);
            var mustPress5 = dirK5.whatIsNeededToPress(line);
            var mustPress10 = dirK10.whatIsNeededToPress(line);

            var nos = Regex.Match(line, "[1-9]\\d*");
            var numericPart = int.Parse(nos.Value);
            var length = mustPress.Length;
            Console.WriteLine($"{line}: Length: {length} (numeric Part: {numericPart}), Length1: {mustPress1.Length},Length1: {mustPress.Length},Length1: {mustPress3.Length},Length4: {mustPress4.Length},Length5: {mustPress5.Length},Length10: {mustPress10.Length}");
            sumA += numericPart*length;
        }
        
        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result A: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}