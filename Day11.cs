using System.Drawing;
using System.Text.RegularExpressions;

class Day11 {

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInput(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));
        
        long sumA=0;
        long sumB=0;

        List<long> intInput = input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToList();
        
        SortedDictionary<long, List<long>> cache = new();
        
        var getListAfter5Direct = (long input) => {
            var singleNumbersList = new List<long>();
            singleNumbersList.Add(input);

            for (int i = 0; i < 5; i++) {
                var newL = new List<long>();
                foreach (long item in singleNumbersList) {
                    if (item == 0) {
                        newL.Add(1);
                    } else if (int.IsEvenInteger(item.ToString().Length)) {
                        string inNo = item.ToString();
                        var halblang = (int)(item.ToString().Length / 2);
                        newL.Add(long.Parse(inNo.Substring(0, halblang)));
                        newL.Add(long.Parse(inNo.Substring(halblang)));
                    } else {
                        newL.Add(item * 2024);
                    }
                }

                singleNumbersList = newL;
            }
            return singleNumbersList;
        };

        var getListAfter5 = (long input) => {
            if (!cache.ContainsKey(input)) {
                cache.Add(input,getListAfter5Direct(input));
            }
            return cache[input];
        };

        var getListAfter25 = (long input) => {
            var input2 = new List<long>();
            input2.Add(input);
            for (int i = 0; i < 5; i++) {
                List<long> newList = new List<long>();
                foreach (long srcItem in input2) {
                    newList.AddRange(getListAfter5(srcItem));
                }
                input2 = newList;
            }
            return input2;
        };

        for (int i = 0; i < 5; i++) {
            List<long> newList = new List<long>();
            foreach (long srcItem in intInput) {
                newList.AddRange(getListAfter5(srcItem));
            }
            intInput = newList;
        }
        Console.WriteLine($"result: {intInput.Count}");
        
        var groupsAfter25 = intInput.GroupBy(x => x);
        var after50 = groupsAfter25.ToDictionary(x => x.Key, x=>getListAfter25(x.Key));

        var somethingAfter50 = new SortedDictionary<long, long>();
        foreach (var oneOfAfter50 in after50) {
            foreach (var a in oneOfAfter50.Value.GroupBy(x => x)) {
                var b = a.Count();
                if (groupsAfter25.Any(g => g.Key == a.Key)) {
                    b *= groupsAfter25.Single(g => g.Key == a.Key).Count();
                }

                if (!somethingAfter50.ContainsKey(a.Key)) {
                    somethingAfter50.Add(a.Key, 0);
                }
                somethingAfter50[a.Key] += b;
            }
        }
        
        var after75 = somethingAfter50.ToDictionary(x => x.Key, x=>getListAfter25(x.Key));
        var somethingAfter75 = new SortedDictionary<long, long>();
        foreach (var oneOfAfter75 in after75) {
            foreach (var a in oneOfAfter75.Value.GroupBy(x => x)) {
                somethingAfter75.Add(a.Key, a.Count());
                if (somethingAfter50.Any(g => g.Key == a.Key)) {
                    somethingAfter75[a.Key] *= somethingAfter50[a.Key];
                }
            }
        }
        
        // var inputAfter50 = 
        //
        // var groupsAfter50 = after50.GroupBy(x => x);
        // var after75 = groupsAfter50.ToDictionary(x => x.Key, x=>getListAfter25(x.Key));
        //
        // Console.WriteLine($"resultAfter50: {after75.Count}");


        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {somethingAfter75.Values.Sum()}");
        Console.WriteLine($"result B: {sumB}");
    }
}