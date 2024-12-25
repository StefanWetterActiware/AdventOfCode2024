using System.Text.RegularExpressions;

class Day25 {

    internal static void doit() {
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getBlocks(Helper.getInput(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value)));

//         input = Helper.getBlocks("""
//
//                                  """);

        long sumA = 0;
        long sumB = 0;
        
        var keys = input.Where(x => x.First().StartsWith('#')).ToList();
        var locks = input.Where(x => x.Last().StartsWith('#')).ToList();
        
        var iKeys = new List<List<int>>();
        foreach (var aKey in keys) {
            var iKey = new List<int>();
            for (int i = 0; i < aKey.First().Length; i++) {
                for (int j = 0; j < aKey.Count; j++) {
                    if (aKey[j][i] == '.') {
                        iKey.Add(j-1);
                        break;
                    }
                }
            }
            iKeys.Add(iKey);
        }

        var iLocks = new List<List<int>>();
        foreach (var aLock in locks) {
            var iLock = new List<int>();
            for (int i = 0; i < aLock.First().Length; i++) {
                for (int j = aLock.Count - 1; j >= 0; j--) {
                    if (aLock[j][i] == '.') {
                        iLock.Add(5-j);
                        break;
                    }
                }
            }
            iLocks.Add(iLock);
        }

        foreach (var aLock in iLocks) {
            foreach (var aKey in iKeys) {
                var passt = true;
                for (int i = 0; i < 5; i++) {
                    if (aKey[i] + aLock[i] > 5) {
                        passt = false;
                        break;
                    }
                }

                sumA += passt ? 1 : 0;
            }
        }

        Console.WriteLine($"result A: {sumA}");
        Console.WriteLine($"result B: {sumB}");
    }
}