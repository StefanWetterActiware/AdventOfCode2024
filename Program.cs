
var lastDayT = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(a => a.Name.StartsWith("Day")).OrderByDescending(a => int.Parse(a.Name.Replace("Day",""))).First();

if (Environment.GetCommandLineArgs().Any() && Environment.GetCommandLineArgs().Any(x => x.StartsWith("Day"))) {
	try {
		lastDayT = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Single(a => a.Name.Equals(Environment.GetCommandLineArgs().First(x => x.StartsWith("Day"))));
		Console.WriteLine("Loaded Class from CmdLineArgs");
	} catch {}
}

var doitM = lastDayT.GetMethod("doit", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
doitM?.Invoke(null,null);
