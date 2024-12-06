using System.Text.RegularExpressions;

class Day6 {

    class Waiter {
        public int x;
        public int y;
        public char direction;
    }
    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var lines = Helper.getInputAsCharArray(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));
        
        long sumA=0;
        long sumB=0;

        Waiter waiter=null;
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines.First().Length; x++)
            {
                if (lines[y][x] == '^') {
                    waiter = new Waiter{x=x, y=y, direction = '^'};
                }
            }
        }

        var isOnGrid = (int x, int y) => {
            return (x >= 0 && y >= 0 && y < lines.Length && x < lines.First().Length);
        };

        while (isOnGrid(waiter!.x, waiter.y)) {
            lines[waiter.y][waiter.x] = 'X';
            int mx = waiter.direction == '>' ? 1 : (waiter.direction == '<' ? -1 : 0);
            int my = waiter.direction == '^' ? -1 : (waiter.direction == 'v' ? 1 : 0);

            if (isOnGrid(waiter.y + my,waiter.x+mx)) {
                if (lines[waiter.y + my][waiter.x+mx] == '#') {
                    waiter.direction = waiter.direction == '<' ? '^' : (waiter.direction == '^' ? '>' : (waiter.direction == '>' ? 'v' : '<'));
                } else {
                    waiter.x += mx;
                    waiter.y += my;
                }
            } else break;
        }

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines.First().Length; x++)
            {
                if (lines[y][x] == 'X') {
                    sumA++;
                }
            }
        }

 
        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}