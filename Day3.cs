using System.Text.RegularExpressions;
using System.Linq;


class Day3 {
    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var lines = Helper.getInput(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name).Value), false);

        long sumA=0;
        long sumB=0;

        System.Text.RegularExpressions.Regex regex = new Regex(@"mul\((?<a>\d{1,3}),(?<b>\d{1,3})\)");
        SortedDictionary<int,int> muls = new();
        regex.Matches(lines).ToList().ForEach(match => {
            sumA+=int.Parse(match.Groups["a"].Value)*int.Parse(match.Groups["b"].Value);
        });

        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}