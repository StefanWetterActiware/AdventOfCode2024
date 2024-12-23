using System.Text.RegularExpressions;

class Day23 {
    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value)).ToList();
        // input = """
        //         """.Split('\n');
        long sumA=0;

        for (int i = 0; i < input.Count(); i++) {
            var comps = input[i].Split('-');

            recurse($"{comps[0]},{comps[1]}", input);

            var l1 = input.Skip(i + 1).Where(x => x.Contains(comps[0])).Select(x=>x.Replace(comps[0], "").Trim('-')).ToList();
            var l2 = input.Skip(i + 1).Where(x => x.Contains(comps[1])).Select(x=>x.Replace(comps[1], "").Trim('-')).ToList();
            var intersect = l1.Intersect(l2);
            if (intersect.Any()) {
                if (comps[0].StartsWith('t') || comps[1].StartsWith('t')) {
                    sumA += intersect.Count();
                } else {
                    sumA += intersect.Count(x=>x.StartsWith('t'));
                }
            }
        }

        var sortLong = longest.Split(',').ToList();
        sortLong.Sort();
        longest = string.Join(',', sortLong);


        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {longest}");
    }

    static private string longest="";
    static List<string>merk=new();

    static void recurse(string current, IEnumerable<string>input) {
        var cur = current.Split(',').ToList();
        cur.Sort();
        var merker = string.Join(',', cur);
        if (merk.Contains(merker))
            return;
        merk.Add(merker);


        List<string> meetAll=null;
        foreach (var c in cur) {
            var treffer = input.Where(x => x.Contains(c)).Select(x => x.Replace(c, "").Trim('-')).ToList();
            if (meetAll == null) {
                meetAll = treffer;
            } else {
                meetAll = meetAll.Intersect(treffer).ToList();
                if (!meetAll.Any())
                    return;
            }
        }
        if (meetAll != null)
            foreach (var m in meetAll) {
                var next = current + "," + m;
                if (next.Length > longest.Length) {
                    longest = next;
                    Console.WriteLine(longest);
                }
                recurse(next, input);
            }
    }
}
