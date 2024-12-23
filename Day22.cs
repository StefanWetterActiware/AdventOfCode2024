using System.Text.RegularExpressions;

internal class Day22 {
    internal static void doit() {
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value)).Select(x=>long.Parse(x)).ToList();
        // input = """
        //         """.Split('\n');

        SortedDictionary<int, Stack<int>> lastPrices = new();
        SortedDictionary<string, List<int>> soldBananas = new();

        for (int i = 0; i < 2000; i++) {
            for (int j = 0; j < input.Count; j++) {
                var x = ((input[j] * 64) ^ input[j]) % 16777216;
                x = ((long)Math.Floor((double)x/32) ^ x) % 16777216;
                x = ((x * 2048) ^ x) % 16777216;

                if (i == 0) {
                    lastPrices[j] = new Stack<int>(4);
                } else {
                    var lastPrice = int.Parse($"{input[j]}".Last().ToString());
                    var curPrice = int.Parse($"{x}".Last().ToString());
                    lastPrices[j].Push(curPrice-lastPrice);
                }

                if (i >= 4) {
                    string seq = String.Join(",",lastPrices[j]);
                    if (!soldBananas.ContainsKey(seq)) {
                        soldBananas.Add(seq, new List<int>{j});
                    } else if (!soldBananas[seq].Contains(j)) {
                        soldBananas[seq].Add(j);
                    }
                }

                input[j] = x;
            }
        }

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"result: {input.Sum()}");
        Console.WriteLine($"result B: {soldBananas.Values.Select(x => x.Count).Max()}");
    }
}
