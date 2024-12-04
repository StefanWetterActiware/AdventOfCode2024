using System.Text.RegularExpressions;
using System.Linq;


class Day4 {

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var chars = Helper.getInputAsCharArray(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name).Value), false);
        
        long sumA=0;
        long sumB=0;

        for (int x = 0; x < chars[0].Length; x++)
        {
            for (int y = 0; y < chars.Length; y++)
            {
                if (chars[x][y] == 'X') {
                    for (int xp = -1; xp <= 1; xp++)
                    {
                        for (int yp = -1; yp <= 1; yp++)
                        {
                            if (x+(3*xp) >=0 && y+(3*yp) >= 0 && x+(3*xp) < chars[0].Length && y+(3*yp) < chars.Length) {
                                if ((chars[x+xp][y+yp] == 'M') && (chars[x+(2*xp)][y+(2*yp)] == 'A') && (chars[x+(3*xp)][y+(3*yp)] == 'S')) {
                                    sumA++;
                                }
                            }
                        }
                    }
                }
                if (chars[x][y] == 'A' && x > 0 && y > 0 && x < chars[0].Length-1 && y < chars.Length-1) {
                    string c = "";
                    c = "" + chars[x-1][y-1] + chars[x+1][y-1] + chars[x+1][y+1] + chars[x-1][y+1];
                    if (c.Replace("S","").Replace("M","").Equals("") && c.Replace("S","").Length == 2) {
                        //ok, 2M&2S, stehen sie auch richtig?
                        if (chars[x-1][y-1] != chars[x+1][y+1]) {
                            sumB++;
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