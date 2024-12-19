using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

class Day19 {

    internal static void doit() {
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getBlocks(Helper.getInput(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value)));

        var towels = input.First().Single().Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        var patterns = input.Skip(1).Take(1).Single();

        //         input = Helper.getBlocks("""
        //                                  """);

        long sumA=0;
        long sumB=0;

        //PartA done by regex101 in 3:05, not running with c#-regex-machine... :(
        // So, let's go recursive with cache...

        foreach (var pattern in patterns) {
            Console.WriteLine(pattern);

            if (canBuildFromPattern(pattern, towels))
                sumA++;
        }

        Console.WriteLine($"result A: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }

    static List<string> notWorking=new();

    static bool canBuildFromPattern(string input, List<string> towels) {
        if (string.IsNullOrEmpty(input)) return true;
        if (towels.Contains(input)) return true;
        if (notWorking.Contains(input)) return false;
        if (input.Length == 1) return false;

        foreach (var towel in towels) {
            var i = input.IndexOf(towel);
            if (i == -1) continue;

            string next1 = input.Substring(0,i);
            string next2 = input.Substring(i + towel.Length);
            if (canBuildFromPattern(next1, towels) && canBuildFromPattern(next2, towels)) return true;
        }
        notWorking.Add(input);
        return false;
    }
}