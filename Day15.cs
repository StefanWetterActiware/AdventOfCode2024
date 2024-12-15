using System.Drawing;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

class Day15 {
    
    internal static bool move (ref char[][] grid, int X, int Y, int mx, int my) {
        char next = grid[Y + my][X + mx];
        if (next == '#') return false;
        if (next == 'O') {
            var canMove = move(ref grid, X + mx, Y + my, mx, my);
            if (canMove) {
                grid[Y + my][X + mx] = 'O';
                return true;
            } else {
                return false;
            }
        }
        grid[Y + my][X + mx] = 'O';
        return true;
    }

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getBlocks(Helper.getInput(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value)));
//         input = Helper.getBlocks("""
//                                  ##########
//                                  #..O..O.O#
//                                  #......O.#
//                                  #.OO..O.O#
//                                  #..O@..O.#
//                                  #O#..O...#
//                                  #O..O..O.#
//                                  #.OO.O.OO#
//                                  #....O...#
//                                  ##########
//
//                                  <vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
//                                  vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
//                                  ><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
//                                  <<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
//                                  ^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
//                                  ^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
//                                  >^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
//                                  <><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
//                                  ^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
//                                  v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^
//                                  """);
        
        long sumA=0;
        long sumB=0;

        var grid = input.First().Select(x => x.ToCharArray()).ToArray();
        var moves = string.Join("", input.Skip(1).First()).ToCharArray();

        Point rob = new();
        for (int y = 0; y < grid.Length; y++) {
            if (grid[y].Contains('@')) {
                for (int x = 0; x < grid[0].Length; x++) {
                    if (grid[y][x] == '@') {
                        rob = new Point(x, y);
                        break;
                    }
                }

                break;
            }
        }


        foreach (char m in moves) {
            int mx= m=='>'?1:m=='<'?-1:0;
            int my = m=='^'?-1:m=='v'?1:0;
            if (grid[rob.Y + my][rob.X + mx] == '#') {
                continue;
            } else if (grid[rob.Y + my][rob.X + mx] == 'O') {
                if (move(ref grid, rob.X + mx, rob.Y + my, mx, my)) {
                    grid[rob.Y][rob.X] = '.';
                    rob = new Point(rob.X + mx, rob.Y + my);
                    grid[rob.Y][rob.X] = '@';
                }
            } else {
                rob = new Point(rob.X + mx, rob.Y + my);
            }
        }

        for (int y = 0; y < grid.Length; y++) {
            if (grid[y].Contains('@')) {
                for (int x = 0; x < grid[0].Length; x++) {
                    if (grid[y][x] == 'O') {
                        sumA += 100*y+x;
                    }
                }
            }
        }


        Console.WriteLine($"result A: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}