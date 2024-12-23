using System.Diagnostics;
using System.Text.RegularExpressions;

internal class Day22 {
    internal static void doit() {
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value)).Select(x=>long.Parse(x)).ToList();
        // input = """
        //         1
        //         2
        //         3
        //         2024
        //         """.Split('\n').Select(x=>long.Parse(x)).ToList();

        SortedDictionary<int, Queue<int>> lastPrices = new();
        SortedDictionary<int, long> lastSecrets = new();
        SortedDictionary<string, SortedDictionary<int, int>> soldBananas = new();

        for (int i = 0; i < 2000; i++) {
            for (int j = 0; j < input.Count; j++) {
                lastSecrets[j] = input[j];
                var x = ((input[j] * 64) ^ input[j]) % 16777216;
                x = ((long)Math.Floor((double)x/32) ^ x) % 16777216;
                x = ((x * 2048) ^ x) % 16777216;

                if (i == 0) {
                    lastPrices[j] = new Queue<int>(4);
                }
                var lastPrice = int.Parse(lastSecrets[j].ToString().Last().ToString());
                var curPrice = int.Parse($"{x}".Last().ToString());
                lastPrices[j].Enqueue(curPrice-lastPrice);
                if (lastPrices[j].Count == 5) lastPrices[j].Dequeue();

                if (i >= 4) {
                    string seq = String.Join(",",lastPrices[j]);
                    if (seq == "-2,1,-1,3") Debugger.Break();
                    if (!soldBananas.ContainsKey(seq)) {
                        soldBananas.Add(seq, new SortedDictionary<int, int>{{j, curPrice}});
                    } else if (!soldBananas[seq].ContainsKey(j)) {
                        soldBananas[seq].Add(j, curPrice);
                    }
                }

                input[j] = x;
            }
        }

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"result: {input.Sum()}");
        Console.WriteLine($"result B: {soldBananas.Values.Select(x => x.Values.Sum()).Max()}");
    }
}
