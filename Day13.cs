using System.Drawing;
using System.Text.RegularExpressions;

class Day13 {

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getBlocks(Helper.getInput(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value)));
        
        long sumA=0;
        long sumB=0;

        foreach (var machine in input) {
            var ax = int.Parse(machine[0].Split(':')[1].Split(',')[0].Trim().Replace("X+",""));
            var ay = int.Parse(machine[0].Split(':')[1].Split(',')[1].Trim().Replace("Y+",""));
            var bx = int.Parse(machine[1].Split(':')[1].Split(',')[0].Trim().Replace("X+",""));
            var by = int.Parse(machine[1].Split(':')[1].Split(',')[1].Trim().Replace("Y+",""));
            var prizeX = int.Parse(machine[2].Split(':')[1].Split(',')[0].Trim().Replace("X=",""));
            var prizeY = int.Parse(machine[2].Split(':')[1].Split(',')[1].Trim().Replace("Y=",""));

            for (int i = 100; i >= 0; i--) {
                if (i*bx <= prizeX) {
                    var restX = prizeX-i*bx;
                    if (restX % ax == 0) {
                        var numA = restX / ax;
                        if (numA*ay + i*by == prizeY) {
                            sumA+=(numA*3+i);
                            break;
                        }
                    }
                }
            }
        }
        


        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}