using System.Drawing;
using System.Text.RegularExpressions;

class Day9 {

    internal static void doit(){
        Regex dayNoR = new(@"\d*$");
        var input = Helper.getInput(int.Parse(dayNoR.Match(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.Name).Value));
        
        long sumA=0;
        long sumB=0;

        List<int> list=new List<int>();

        for (int i = 0; i < input.Length; i+=2) {
            for (int j = 0; j < int.Parse($"{input[i]}"); j++) {
                list.Add(((int)i/2));
            }
            if (i < input.Length-1)
                for (int j = 0; j < int.Parse($"{input[i+1]}"); j++) {
                    list.Add(-1);
                }
        }
        List<int> list2=list.ToArray().ToList();

        while (list.Contains(-1)) {
            var ind=list.IndexOf(-1);
            var last = list.Last();
            list=list.Take(list.Count-1).ToList();
            
            //Letzten Punkt hinten abfagnen
            if (!list.Contains(-1)) break;

            list[ind]=last;
            // Console.WriteLine(fileData);
        }
        for (int i = 0; i < list.Count; i++) {
            sumA += list[i]*i;
        }
        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result: {sumA}");

        var highestID = list2.Last(x=>x!=-1);
        for (int i = highestID; i >0; i--) {
            int count = list2.Count(x=>x==i);
            int searchIdx=0;
            var found=false;
            while (list2.IndexOf(-1,searchIdx+1) >-1) {
                searchIdx = list2.IndexOf(-1,searchIdx+1);
                if (list2.Skip(searchIdx).Take(count).Count(x=>x == -1) == count) {
                    found=true;
                    break;
                }
            }
            if (found) {
                int startIdxOfI = list2.IndexOf(i);
                if (startIdxOfI > -1 && searchIdx < startIdxOfI) {
                    for (int j = 0; j < count; j++) {
                        list2[searchIdx+j] = i;
                        list2[startIdxOfI+j] = -1;
                    }
                }
            }
        }
        for (int i = 0; i < list2.Count; i++) {
            if (list2[i] > 0)
                sumB += list2[i]*i;
        }


        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine($"result B: {sumB}");
    }
}