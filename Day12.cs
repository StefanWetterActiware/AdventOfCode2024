using System.Drawing;
using System.Text.RegularExpressions;

class Day12 {

    internal static void addAll (char[][] input, Point start, ref List<Point> all) {
            for (int i = 0; i < 4; i++) {
                int mx = i==0 ? 1 : i==2 ? -1 : 0;
                int my = i==1 ? 1 : i==3 ? -1 : 0;

                if (Helper.isOnGrid(input, start.X+mx, start.Y+my)) {
                    if (input[start.Y][start.X] == input[start.Y+my][start.X+mx]) {
                        var newP = new Point(start.X+mx, start.Y+my);
                        if (!all.Contains(newP)) {
                            all.Add(newP);
                            addAll(input, newP, ref all);
                        }
                    }
                }
            }
        }

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInputAsCharArray(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));
        
        long sumA=0;
        long sumB=0;

        Dictionary<Point, int> fencesPerPoint = new();

        for (int y = 0; y < input.Length; y++) {
            for (int x = 0; x < input.First().Length; x++) {
                int countFences=0;
                for (int i = 0; i < 4; i++) {
                    int mx = i==0 ? 1 : i==2 ? -1 : 0;
                    int my = i==1 ? 1 : i==3 ? -1 : 0;

                    if (!Helper.isOnGrid(input, x+mx, y+my)) {
                        countFences++;
                    } else if (input[y][x] != input[y+my][x+mx]) {
                        countFences++;
                    }
                }
                fencesPerPoint.Add(new Point(x,y), countFences);
            }
        }

        var areas = new Dictionary<Point, List<Point>>();
        for (int y = 0; y < input.Length; y++) {
            for (int x = 0; x < input.First().Length; x++) {
                var curP = new Point(x,y);
                if (!areas.Any(x=> x.Value.Contains(curP))) {
                    var thisAreaPoints = new List<Point>();
                    thisAreaPoints.Add(curP);
                    addAll(input, curP, ref thisAreaPoints);
                    areas.Add(curP, thisAreaPoints);
                }
            }   
        }

        foreach (var area in areas) {
            var areaOfArea = area.Value.Count();
            var totalFences = area.Value.Select(x => fencesPerPoint[x]).Sum();
            sumA+=areaOfArea*totalFences;
        }


        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}