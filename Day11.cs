using System.Drawing;
using System.Text.RegularExpressions;

class Day11 {

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInput(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));
        
        long sumA=0;
        long sumB=0;

        List<long> intInput = input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToList();
        // Console.WriteLine($"run 0: {intInput.Count}");
        Console.WriteLine($"run {0}: {String.Join(' ', intInput.Select(x=>x.ToString()))}");

        for (int i = 0; i < 25; i++) {
            List<long> newL = new();

            foreach (long item in intInput) {                
                if (item == 0) {
                    newL.Add(1);
                } else if (int.IsEvenInteger(item.ToString().Length)) {
                    string inNo = item.ToString();
                    var halblang = (int)(item.ToString().Length / 2);
                    newL.Add(long.Parse(inNo.Substring(0,halblang)));
                    newL.Add(long.Parse(inNo.Substring(halblang)));
                } else {
                    newL.Add(item * 2024);
                }
            }
            intInput=newL;
        }


        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {intInput.Count}");
        Console.WriteLine($"result B: {sumB}");
    }
}