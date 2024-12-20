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
            
            //Part B, with mathematics
            /*
               Xr = k*Xa + j*Xb
               Yr = k*Ya + j*Yb
               Yr - j*Yb = k*Ya
               (Yr - j*Yb)/Ya = k                  <==
               Xr = ((Yr - j*Yb)/Ya)*Xa + j*Xb
               Xr - j*Xb = ((Yr - j*Yb)/Ya)*Xa
               (Xr - j*Xb)/Xa = (Yr - j*Yb)/Ya
               Xa/(Xr - j*Xb) = Ya/(Yr - j*Yb)
               Xa*(Yr - j*Yb) = Ya*(Xr - j*Xb)
               Xa*Yr - j*Yb*Xa = Ya*Xr - j*Ya*Xb
               Xa*Yr = Ya*Xr + j*Yb*Xa - j*Ya*Xb
               Xa*Yr - Ya*Xr = j*Yb*Xa - j*Ya*Xb
               Xa*Yr - Ya*Xr = j*(Yb*Xa - Ya*Xb)
               (Xa*Yr - Ya*Xr)/(Yb*Xa - Ya*Xb) = b  <==
             */
            long Xr = 10000000000000 + prizeX;
            long Yr = 10000000000000 + prizeY;
            var j = (ax * Yr - ay * Xr) / (by * ax - ay * bx);
            var k = (Yr - j * by) / ay;
            
            //Probe, ob es aufgeht
            if ((Xr == k*ax + j*bx) && (Yr == k*ay + j*by))
                sumB += k * 3 + j;
        }
        

        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
        //158125581557512 is too high
    }
}