using System.Text.RegularExpressions;


class Day1 {
    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var lines = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name).Value));

        List<long> list1 = new();
        List<long> list2 = new();

        foreach(string l in lines){
            var s = l.Split(' ',StringSplitOptions.RemoveEmptyEntries);
            list1.Add(long.Parse(s[0]));
            list2.Add(long.Parse(s[1]));
        }

        list1.Sort();
        list2.Sort();

        long sumA=0;
        long sumB=0;
        for (int i = 0; i < list1.Count; i++){
            sumA+=Math.Abs(list2[i] - list1[i]);
            sumB+=(list1[i] * list2.Count(a=>a == list1[i]));
        }

        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}