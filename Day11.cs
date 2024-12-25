using System.Drawing;
using System.Text.RegularExpressions;

class Day11 {

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInput(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));
        
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

        var getGroupedAfter25 = (long input) => {
            var r = getListAfter25(input);
            return r.GroupBy(x=>x).ToDictionary(x => x.Key, y => (long)y.Count());
        };

        var getListAfter25FromList = (List<long> input) => {
            var res = new SortedDictionary<long, long>();
            foreach (var i in intInput) {
                var r = getListAfter25(i);
                var grouped = r.GroupBy(x=>x).ToDictionary(x => x.Key, y => (long)y.Count());
                foreach (var x in grouped) {
                    if (!res.ContainsKey(x.Key)) res.Add(x.Key, 0);
                    res[x.Key] += x.Value;
                }
            }

            return res;
        };
        
        var zwischenergebnis = getListAfter25FromList(intInput);
        Console.WriteLine($"result: {zwischenergebnis.Values.Sum()}");

        for (int i = 0; i < 2; i++) {
            var n = new SortedDictionary<long, long>();
            foreach (var a in zwischenergebnis) {
                foreach (var kvpzn in getGroupedAfter25(a.Key)) {
                    if (!n.ContainsKey(kvpzn.Key)) n.Add(kvpzn.Key, 0);
                    n[kvpzn.Key] += a.Value*kvpzn.Value;
                }
            }

            zwischenergebnis = n;
        }        


        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result B: {zwischenergebnis.Values.Sum()}");
    }
}