using System.Drawing;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

class Day17 {
    
    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getBlocks(Helper.getInput(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value)));

          // input = Helper.getBlocks("""
          //                          Register A: 2024
          //                          Register B: 0
          //                          Register C: 0
          //                          
          //                          Program: 0,1,5,4,3,0
          //                          """);
        
        long sumA=0;
        long sumB=0;

        var registers = input.First();
        var programList = input.Last();

        var program = programList.Where(x => !String.IsNullOrWhiteSpace(x)).Single();
        program = program.Split(':',StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1];

        int regA = int.Parse(registers[0].Split(':')[1]);
        int regB = int.Parse(registers[1].Split(':')[1]);
        int regC = int.Parse(registers[2].Split(':')[1]);

        var outs = new List<int>();

        var p = program.Split(',');
        for (int i = 0; i < p.Length; i+=2) {
            var opcode = p[i];
            var param = int.Parse(p[i+1]);
            var combo = param;
            switch (param) {
                case 4: combo = regA; break;
                case 5: combo = regB; break;
                case 6: combo = regC; break;
                // case 7: continue;
            }
            switch (opcode) {
                case "0": regA =(int)Math.Floor(regA/Math.Pow(2, combo)); break;
                case "1": regB =regB^param; break;
                case "2": regB = combo % 8; break;
                case "3":
                    if (regA != 0) {
                        i = param - 2;
                    }
                    break;
                case "4": regB =regB^regC; break;
                case "5": outs.Add(combo%8); break;
                case "6": regB =(int)Math.Floor(regA/Math.Pow(2, combo)); break;
                case "7": regC =(int)Math.Floor(regA/Math.Pow(2, combo)); break;
            }
        }
        
        
        
        

        Console.WriteLine($"result A: {String.Join(",",outs)}");
        Console.WriteLine($"result B: {sumB}");
    }
}