using System;
using System.Reflection;
using DPI.Interfaces;
 
namespace DPI.Components
{
	public class CLoader 
	{
		public static object LoadObject(string app, string target)
		{
			//  App is a dll containg target, and target is  a fully qualify class name 
			//  For instance, app =  "BusComp", target = "DPI.Components.CardApp";
			return Activator.CreateInstance(Assembly.Load(app).GetType(target));
		}
	}
}