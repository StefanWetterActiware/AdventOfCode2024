using System.Text.RegularExpressions;
using System.Linq;


class Day3 {
    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var lines = Helper.getInput(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name).Value), false);

        long sumA=0;
        long sumB=0;

        System.Text.RegularExpressions.Regex regex = new Regex(@"mul\((?<a>\d{1,3}),(?<b>\d{1,3})\)");
        regex.Matches(lines).ToList().ForEach(match => {
            sumA+=int.Parse(match.Groups["a"].Value)*int.Parse(match.Groups["b"].Value);
        });

        System.Text.RegularExpressions.Regex dodont = new Regex(@"(?:(?:don't|do)\(\)|^).+?(?=(?:don't\(\)|do\(\)|$))", RegexOptions.Singleline);
        dodont.Matches(lines).ToList().ForEach(match => {
            if (!match.Value.StartsWith("don't")) {
                regex.Matches(match.Value).ToList().ForEach(match2 => {
                    sumB+=int.Parse(match2.Groups["a"].Value)*int.Parse(match2.Groups["b"].Value);
                });
            }
        });

        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}