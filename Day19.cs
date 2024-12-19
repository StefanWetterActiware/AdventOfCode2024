using System.Text.RegularExpressions;

class Day19 {

    internal static void doit() {
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getBlocks(Helper.getInput(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value)));

        // input = Helper.getBlocks("""
        //                          r, wr, b, g, bwu, rb, gb, br
        //                          
        //                          brwrr
        //                          bggr
        //                          gbbr
        //                          rrbgbr
        //                          ubwu
        //                          bwurrg
        //                          brgr
        //                          bbrgwb
        //                          """);

        var towels = input.First().Single().Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        var patterns = input.Skip(1).Take(1).Single();

        long sumA=0;
        long sumB=0;

        //PartA done by regex101 in 3:05, not running with c#-regex-machine... :(
        // So, let's go recursive with cache...

        foreach (var pattern in patterns) {
            Console.Write(pattern + ": ");

            countPatternPossibilities(pattern, towels, out long count);
            sumB += count;
            if (count > 0) sumA++;
            
            Console.WriteLine($"{count} possibilities");
        }

        Console.WriteLine($"result A: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }

    static SortedDictionary<string, long> countDict = new();

    static void countPatternPossibilities(string input, List<string> towels, out long count) {
        if (countDict.ContainsKey(input)) {
            count = countDict[input];
            return;
        }
        countPatternPossibilitiesDirect(input, towels, out long c);
        countDict.Add(input, c);
        count = c;
    }

    static void countPatternPossibilitiesDirect(string input, List<string>towels, out long count) {
        count = 0;
        
        if (string.IsNullOrWhiteSpace(input)) {
            count = 1;
            return;
        }
        
        foreach (var towel in towels) {
            if (!input.StartsWith(towel))
                continue;

            string next = input.Substring(towel.Length);
            countPatternPossibilities(next, towels, out long countA);
            count += countA;
        }
    }
}