using Produce_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp
{
	public static class DataDisplay
	{
		public static ComputedDataModel computedDataModel { get; set; }
		public static double Age { get; set; }
		public static double Weight { get; set; }
		public static double Height { get; set; }
		public static double Sleep { get; set; }
		public static double Hydration { get; set; }
		public static double Salary { get; set; }
		public static double Breaks { get; set; }
		public static double BreaksDeficit{get;set;}
		public static double HoursPerWeek { get; set; }
		//Store total amount of productivity lost here.
		public static double TotalProductivityLoss { get; set; }
		public static double HydrationLoss { get; set; }
		public static double SleepLoss { get; set; }
		public static double WeeklySalary { get; set; }
	}
}
