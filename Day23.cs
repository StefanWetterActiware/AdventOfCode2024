using System.Drawing;
using System.Text.RegularExpressions;

class Day23 {
    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value)).ToList();
        // input = """
        //         """.Split('\n');
        long sumA=0;
        long sumB=0;

        List<string> sets = new();

        for (int i = 0; i < input.Count(); i++) {
            var comps = input[i].Split('-');
            var l1 = input.Skip(i + 1).Where(x => x.Contains(comps[0])).Select(x=>x.Replace(comps[0], "").Trim('-')).ToList();
            var l2 = input.Skip(i + 1).Where(x => x.Contains(comps[1])).Select(x=>x.Replace(comps[1], "").Trim('-')).ToList();
            var intersect = l1.Intersect(l2);
            if (intersect.Any()) {
                if (comps[0].StartsWith('t') || comps[1].StartsWith('t')) {
                    sumA += intersect.Count();
                } else {
                    sumA += intersect.Count(x=>x.StartsWith('t'));
                }
            }
        }
        
        
        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}