using System.Text.RegularExpressions;

class Day7 {
    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var lines = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));
        
        long sumA=0;
        long sumB=0;

        foreach (var line in lines.Where(a=> !string.IsNullOrWhiteSpace(a))) {
            long res = long.Parse(line.Split(':')[0]);
            int[] numbers = line.Split(':')[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select( a => int.Parse(a.Trim())).ToArray();
            int operatorCount = numbers.Length-1;
            for (int i = 0; i < Math.Pow(2, operatorCount); i++) {

                long testRes = numbers[0];
                for (int j = 0; j < operatorCount; j++) {
                    if ((i & (int)Math.Pow(2, j)) == 0) {
                        testRes += numbers[j+1];
                    } else {
                        testRes *= numbers[j+1];
                    }
                }

                if (testRes == res) {
                    sumA += res;
                    break;
                }
            }
        }


        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}