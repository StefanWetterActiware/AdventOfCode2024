using System.Text.RegularExpressions;
using System.Linq;


class Day2 {
    static bool doLine(List<int> intParts) {
        var compare = intParts[0]>intParts[1] ? -1 : 1;
        for (int j=0; j<intParts.Count-1; j++){
            var diff = intParts[j+1] - intParts[j];
            if (Math.Abs(diff) >3){return false;}
            if (Math.Sign(diff) != Math.Sign(compare)) return false;
        }
        return true;
    }

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var lines = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name).Value));

        long sumA=0;

        foreach (var line in lines){
            var parts=line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length == 0){break;}
            List<int> intParts = new();
            parts.ToList().ForEach( a=> intParts.Add(int.Parse(a)));

            if (doLine(intParts)) {
                sumA++;
            }
        }


        Console.WriteLine($"result: {sumA}");
        //Console.WriteLine($"result B: {sumB}");
    }
}