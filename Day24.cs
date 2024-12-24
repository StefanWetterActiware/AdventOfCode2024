using System.Text.RegularExpressions;

class Day24 {
    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInput(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));
        // input = """
        //         x00: 1
        //         x01: 0
        //         x02: 1
        //         x03: 1
        //         x04: 0
        //         y00: 1
        //         y01: 1
        //         y02: 1
        //         y03: 1
        //         y04: 1
        //         
        //         ntg XOR fgs -> mjb
        //         y02 OR x01 -> tnw
        //         kwq OR kpj -> z05
        //         x00 OR x03 -> fst
        //         tgd XOR rvg -> z01
        //         vdt OR tnw -> bfw
        //         bfw AND frj -> z10
        //         ffh OR nrd -> bqk
        //         y00 AND y03 -> djm
        //         y03 OR y00 -> psh
        //         bqk OR frj -> z08
        //         tnw OR fst -> frj
        //         gnj AND tgd -> z11
        //         bfw XOR mjb -> z00
        //         x03 OR x00 -> vdt
        //         gnj AND wpb -> z02
        //         x04 AND y00 -> kjc
        //         djm OR pbm -> qhw
        //         nrd AND vdt -> hwm
        //         kjc AND fst -> rvg
        //         y04 OR y02 -> fgs
        //         y01 AND x02 -> pbm
        //         ntg OR kjc -> kwq
        //         psh XOR fgs -> tgd
        //         qhw XOR tgd -> z09
        //         pbm OR djm -> kpj
        //         x03 XOR y03 -> ffh
        //         x00 XOR y04 -> ntg
        //         bfw OR bqk -> z06
        //         nrd XOR fgs -> wpb
        //         frj XOR qhw -> z04
        //         bqk OR frj -> z07
        //         y03 OR x01 -> nrd
        //         hwm AND bqk -> z03
        //         tgd XOR rvg -> z12
        //         tnw OR pbm -> gnj
        //         """;
        
        var blocks = Helper.getBlocks(input);
        var inputs = blocks.First().ToDictionary(x => x.Split(':')[0], x => int.Parse(x.Split(':')[1]));
        var rules = blocks.Last();
        
        Regex ruleX = new(@"(?<in1>\S+) (?<op>\S+) (?<in2>\S+) -> (?<out>\S+)", RegexOptions.Compiled);

        while (true) {
            if (rules.Count == 0) break;
            var rule = rules.First();
            rules.Remove(rule);
            
            var m = ruleX.Match(rule);
            var in1 = m.Groups["in1"].Value;
            var in2 = m.Groups["in2"].Value;
            var op = m.Groups["op"].Value;
            var outwire = m.Groups["out"].Value;
            
            if (inputs.ContainsKey(in1) && inputs.ContainsKey(in2)) {
                var res = inputs[in1] | inputs[in2];
                if (op == "AND") res = inputs[in1] & inputs[in2];
                if (op == "XOR") res = inputs[in1] ^ inputs[in2];
                
                if (!inputs.ContainsKey(outwire))
                    inputs.Add(outwire, 0);
                inputs[outwire] = res;
            } else {
                //to the end again
                rules.Insert(rules.Count, rule);
            }
        }
        
        long sumA=0;
        long sumB=0;

        foreach (var inp in inputs.Where(x => x.Key.StartsWith('z'))) {
            var z = int.Parse(inp.Key[1..]);
            sumA += inp.Value == 0 ? 0 : (long)Math.Pow(2,z);
        }

        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}
