using System.Text.RegularExpressions;

class Day7 {

        static bool checkline(long goodRes, long start, IEnumerable<int> numbers, bool withAppend = false) {
            if (numbers.Any()) {
                var next = numbers.First();
                var others = numbers.Skip(1);

                if (checkline(goodRes, start+next, others, withAppend)) return true;
                if (checkline(goodRes, start*next, others, withAppend)) return true;
                if (withAppend && checkline(goodRes, long.Parse($"{start}{next}"), others, withAppend)) return true;
                return false;
            } else {
                return goodRes==start;
            }
        }

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var lines = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));
        
        long sumA=0;
        long sumB=0;

        foreach (var line in lines.Where(a=> !string.IsNullOrWhiteSpace(a))) {
            long res = long.Parse(line.Split(':')[0]);
            int[] numbers = line.Split(':')[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select( a => int.Parse(a.Trim())).ToArray();

            if (checkline(res, 0 , numbers)) {
                sumA += res;
                sumB += res;
            } else if (checkline(res, 0 , numbers, true)) {
                sumB += res;
            }
        }

        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}