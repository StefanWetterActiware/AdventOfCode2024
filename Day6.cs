using System.Text.RegularExpressions;

class Day6 {

    class Waiter {
        public int x;
        public int y;
        public char direction;
        public void turnRight() {
            this.direction = this.direction == '<' ? '^' : (this.direction == '^' ? '>' : (this.direction == '>' ? 'v' : '<'));
        }
        public Waiter clone() {
            return new Waiter {x=this.x, y=this.y,direction=this.direction};
        }
    }
    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var lines = Helper.getInputAsCharArray(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value), true);
        
        long sumA=0;
        long sumB=0;

        Waiter waiter=null;
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines.First().Length; x++)
            {
                if (lines[y][x] == '^') {
                    waiter = new Waiter{x=x, y=y, direction = '^'};
                    lines[y][x] = '.';
                }
            }
        }
        Waiter startPoint = waiter!.clone();

        var isOnGrid = (int x, int y) => {
            return (x >= 0 && y >= 0 && y < lines.Length && x < lines.First().Length);
        };

        var printGrid = (char[][] grid, int offsetY = 0) => {
            int start = Math.Max(0, offsetY-40);
            if (start > (129-55)) start=129-55;
            for (int i = start; i < start+55; i++) {
                Console.WriteLine(grid[i]);
            }
        };

        var checkInfiniteLoop = (Waiter rekCheckWaiter, char[][] rekCheckGrid) => {
            while (isOnGrid(rekCheckWaiter!.x, rekCheckWaiter.y)) {
                int mx = rekCheckWaiter.direction == '>' ? 1 : (rekCheckWaiter.direction == '<' ? -1 : 0);
                int my = rekCheckWaiter.direction == '^' ? -1 : (rekCheckWaiter.direction == 'v' ? 1 : 0);

                if (isOnGrid(rekCheckWaiter.y + my,rekCheckWaiter.x+mx)) {
                    if (rekCheckGrid[rekCheckWaiter.y][rekCheckWaiter.x] >= 'Z') {
                        Console.Clear();
                        printGrid(rekCheckGrid, rekCheckWaiter.y);
                        Thread.Sleep(2);
                        return true;
                    } else if (rekCheckGrid[rekCheckWaiter.y + my][rekCheckWaiter.x+mx] == '#') {
                        rekCheckWaiter.turnRight();
                    } else {
                        if (rekCheckGrid[rekCheckWaiter.y][rekCheckWaiter.x] == '.')
                            rekCheckGrid[rekCheckWaiter.y][rekCheckWaiter.x] = 'X';
                        else if (rekCheckGrid[rekCheckWaiter.y][rekCheckWaiter.x] == 'X' || rekCheckGrid[rekCheckWaiter.y][rekCheckWaiter.x] == 'Y')
                            rekCheckGrid[rekCheckWaiter.y][rekCheckWaiter.x]++;
                        // Console.Clear();
                        // printGrid(rekCheckGrid, rekCheckWaiter.y);
                        // Thread.Sleep(2);
                        rekCheckWaiter.x += mx;
                        rekCheckWaiter.y += my;
                    }
                } else
                    return false;
            }
            return false;
        };

        List<string> b=new();

        while (isOnGrid(waiter!.x, waiter.y)) {
            int mx = waiter.direction == '>' ? 1 : (waiter.direction == '<' ? -1 : 0);
            int my = waiter.direction == '^' ? -1 : (waiter.direction == 'v' ? 1 : 0);

            if (isOnGrid(waiter.y + my,waiter.x+mx)) {
                if (lines[waiter.y + my][waiter.x+mx] == '#') {
                    waiter.turnRight();
                } else {
                    //Part2
                    var rekCheckWaiter = waiter.clone();
                    char[][] rekCheckGrid = new char[lines.Length][];
                    for (int i = 0; i < lines.Length; i++) {
                        rekCheckGrid[i] = (char[])lines[i].Clone();
                    }
                    rekCheckGrid[rekCheckWaiter.y + my][rekCheckWaiter.x+mx] = 'O';

                    rekCheckWaiter.turnRight();
                    if (checkInfiniteLoop(rekCheckWaiter, rekCheckGrid)) {
                        b.Add($"y:{rekCheckWaiter.y},x:{rekCheckWaiter.x}");
                        // rekCheckGrid[rekCheckWaiter.y + my][rekCheckWaiter.x+mx] = 'O';
                        // printGrid(rekCheckGrid);
                        sumB++;
                    }

                    lines[waiter.y][waiter.x] = 'X';
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
        // foreach (var item in b) {
        //     Console.WriteLine(item.ToString());
        // }
        Console.WriteLine($"result B: {sumB}");
    }
}