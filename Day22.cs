using System.Text.RegularExpressions;

internal class Day22 {
    internal static void doit() {
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value)).Select(x=>long.Parse(x)).ToList();
        // input = """
        //         """.Split('\n');

        long sumB=0;

        for (int i = 0; i < 2000; i++) {
            for (int j = 0; j < input.Count; j++) {
                var x = ((input[j] * 64) ^ input[j]) % 16777216;
                x = ((long)Math.Floor((double)x/32) ^ x) % 16777216;
                x = ((x * 2048) ^ x) % 16777216;
                input[j] = x;
            }
        }

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"result: {input.Sum()}");
        Console.WriteLine($"result B: {sumB}");
    }
}
