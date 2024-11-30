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
        httpClient.DefaultRequestHeaders.Add("Cookie", "session=" + File.ReadAllText("sessionCookie"));

        System.IO.File.WriteAllText(getCacheFileName(tag), httpClient.GetStringAsync(url).GetAwaiter().GetResult());
    }

    [DebuggerHidden]
    public static IEnumerable<string> getInputAsLines(int tag, bool test = false){
        loadInput(tag);
        return System.IO.File.ReadAllLines(getCacheFileName(tag,test)).Where(a=> !String.IsNullOrWhiteSpace(a));
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
}