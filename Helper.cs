using System.Diagnostics;
using System.Reflection.Metadata;

static class Helper
{
    private static string cachefileTempl = "input/day{0}";

    [DebuggerHidden]
    private static string getCacheFileName(int tag, bool test = false) {
        return string.Format(cachefileTempl, tag) + (test ? "test":"");
    }
    
    [DebuggerHidden]
    public static void loadInput(int tag)
    {
        string urltempl = "https://adventofcode.com/2024/day/{0}/input";

        System.IO.Directory.CreateDirectory("input");

        if (System.IO.File.Exists(getCacheFileName(tag))) return;

        string url = string.Format(urltempl, tag);

        HttpClient httpClient = new();
        var sessionCookieFileName = "sessionCookie";
        if (!System.IO.File.Exists(sessionCookieFileName)) {
            sessionCookieFileName = "../../../sessionCookie";
        }
        httpClient.DefaultRequestHeaders.Add("Cookie", "session=" + File.ReadAllText(sessionCookieFileName));

        System.IO.File.WriteAllText(getCacheFileName(tag), httpClient.GetStringAsync(url).GetAwaiter().GetResult());
    }

    [DebuggerHidden]
    public static IEnumerable<string> getInputAsLines(int tag, bool test = false){
        loadInput(tag);
        return System.IO.File.ReadAllLines(getCacheFileName(tag,test)).Where(a=> !String.IsNullOrWhiteSpace(a));
    }

    [DebuggerHidden]
    public static char[][] getInputAsCharArray(int tag, bool test = false){
        var lines = getInputAsLines(tag, test);
        char[][] res = new char[lines.First().Length][];
        for (int i = 0; i < lines.Count(); i++)
        {
            res[i] = lines.Skip(i).First().ToCharArray();
        }
        return res;
    }

    [DebuggerHidden]
    public static string getInput(int tag, bool test = false){
        loadInput(tag);
        return System.IO.File.ReadAllText(getCacheFileName(tag, test)).TrimEnd("@\r\n".ToCharArray());
    }

    [DebuggerHidden]
    public static IEnumerable<IEnumerable<string>> getBlocks(IEnumerable<string> lines) {
        return getBlocks(String.Join("\n", lines));
    }
    [DebuggerHidden]
    public static IEnumerable<List<string>> getBlocks(string input) {
        return input.Split("\n\n").Select(a => a.Split("\n").ToList());
    }
    [DebuggerHidden]
    public static List<string> getLines(string input) {
        return input.Trim('\n').Split("\n").Select(a=>a.TrimEnd('\r')).ToList();
    }

    [DebuggerHidden]
    public static bool isOnGrid(char[][] lines, int x, int y){
        return (x >= 0 && y >= 0 && y < lines.Length && x < lines.First().Length);
    }

}