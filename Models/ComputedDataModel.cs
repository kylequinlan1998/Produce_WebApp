using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.Models
{
	public class ComputedDataModel
	{
		public double Age { get; set; }
		public double Height { get; set; }
		public double Weight { get; set; }
		public double Salary { get; set; }
		public double WeeklySalary{get;set;}
		public double WeeklyHours { get; set; }
		public double DailyHours { get; set; }
		public double Water { get; set; }
		public double WaterDeficit { get; set; }
		public double WaterProductivityLoss { get; set; }
		public double WaterMonetaryLoss { get; set; }
		public double Sleep { get; set; }
		public double SleepDeficit { get; set; }
		public double SleepProductivityLoss { get; set; }
		public double SleepMonetaryLoss { get; set; }
		public double Breaks { get; set; }
		public double BreaksDeficit { get; set; }
		public double BreaksProductivityLoss { get; set; }
		public double BreaksMonetaryLoss { get; set; }
		public double TotalBreakTime { get; set; }
		public double TotalWeeklyMonetaryLoss { get; set; }
		public double TotalProductivityLoss { get; set; }
		public double TotalYearlyMonetaryLoss { get; set; }
	}
}
