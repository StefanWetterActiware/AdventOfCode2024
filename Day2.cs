using System.Text.RegularExpressions;
using System.Linq;


class Day2 {
    static bool doLine(List<int> intParts, int allowedErrors, out int removedIndex) {
        removedIndex = -1;

        var compare = intParts[0]>intParts[1] ? -1 : 1;
        var didTestWithoutFirst = false;
        for (int j=0; j<intParts.Count-1; j++){
            var diff = intParts[j+1] - intParts[j];
            if (Math.Abs(diff) >3 || Math.Sign(diff) != Math.Sign(compare)) {
                if (allowedErrors == 0) return false;

                if (!didTestWithoutFirst) {
                    var test2 = intParts.ToList();
                    test2.RemoveAt(0);
                    var withoutFirst = doLine(test2,(allowedErrors-1), out int dummy);
                    if (withoutFirst) {
                        removedIndex=0;
                        return true;
                    }
                    didTestWithoutFirst=true;
                }
                var test = intParts.ToList();
                test.RemoveAt(j+1);
                removedIndex=j+1;
                if (doLine(test,(allowedErrors-1), out int dummy2)) return true;

                var test3 = intParts.ToList();
                test3.RemoveAt(j);
                removedIndex=j;
                return doLine(test3,(allowedErrors-1), out int dummy3);
            }
        }
        return true;
    }

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var lines = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name).Value), false);

        long sumA=0;
        long sumB=0;

        foreach (var line in lines){
            var parts=line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length == 0){break;}
            List<int> intParts = new();
            parts.ToList().ForEach( a=> intParts.Add(int.Parse(a)));

            if (doLine(intParts,0, out int dummy)) {
                sumA++;
            }
            if (doLine(intParts,1, out int removedIndex)) {
                sumB++;
                if (removedIndex > -1) {
                    Console.ForegroundColor=ConsoleColor.Green;
                    Console.Write(string.Join(' ', intParts.Take(removedIndex).ToArray()));
                    Console.ForegroundColor=ConsoleColor.Red;
                    Console.Write($"{(removedIndex>0?" ":"")}{intParts[removedIndex]} ");
                    Console.ForegroundColor=ConsoleColor.Green;
                    Console.WriteLine(string.Join(' ', intParts.Skip(removedIndex+1).ToArray()));
                } else {
                    Console.ForegroundColor=ConsoleColor.Green;
                    Console.WriteLine(string.Join(' ', intParts.ToArray()));
                }
            } else {
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine(string.Join(' ', intParts.ToArray()));
            }
        }

        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}