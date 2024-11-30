﻿
//Day17.doit();return;

var lastDayT = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(a => a.Name.StartsWith("Day")).OrderByDescending(a => int.Parse(a.Name.Replace("Day",""))).First();
//var lastDay = System.Activator.CreateInstance(lastDayT);

var doitM = lastDayT.GetMethod("doit", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
doitM.Invoke(null,null);
