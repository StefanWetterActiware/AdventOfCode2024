using System.Text.RegularExpressions;

class Day5 {

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var lines = Helper.getInputAsLines(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value), false);
        
        long sumA=0;
        long sumB=0;

        SortedDictionary<int,List<int>> rules = new();

        List<List<int>> incorr = new();

        foreach (var line in lines)
        {
            if (line.IndexOf("|")>-1) {
                //rulesBefore.Add(int.Parse(line.Split("|")[1]), int.Parse(line.Split("|")[0]));
                int bef = int.Parse(line.Split("|")[0]);
                int af = int.Parse(line.Split("|")[1]);
                if (rules.ContainsKey(bef)) {
                    rules[bef].Add(af);
                } else {
                    rules.Add(bef, new List<int>{af});
                }
                continue;
            }

            if (string.IsNullOrWhiteSpace(line)) {continue;}

            var strs = line.Split(",");
            List<int> nums = strs.Select(x => int.Parse(x)).ToList();
            bool correct=true;
            for (int i = 0; i < nums.Count(); i++)
            {
                if (rules.ContainsKey(nums[i])) {
                    for (int j = 0; j < i; j++)
                    {
                        if (rules[nums[i]].Contains(nums[j])) {
                            correct=false;
                        }
                    }
                }
            }
            if (correct) {
                sumA += nums[nums.Count/2];
            }else {
                incorr.Add(nums);
            }
        }
        

        foreach (var nums in incorr)
        {
            for (int k = 0;k < nums.Count; k++) {
                if (rules.ContainsKey(nums[k])) {
                    if (rules[nums[k]].Any(a => nums.Take(k).Contains(a))){
                        var first = nums.Take(k).First(a => rules[nums[k]].Contains(a));
                        var curNum = nums[k];
                        nums.RemoveAt(k);
                        nums.Insert(nums.IndexOf(first), curNum);
                    }
                }
            }
            Console.WriteLine(string.Join(',',nums));
            sumB += nums[nums.Count/2];
        }

        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}