﻿using System;
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
		public double WaterAnnualLoss { get; set; }
		public string WaterMessage { get; set; }
		public double Sleep { get; set; }
		public double SleepDeficit { get; set; }
		public double SleepProductivityLoss { get; set; }
		public double SleepMonetaryLoss { get; set; }
		public double SleepAnnualLoss { get; set; }
		public string SleepMessage { get; set; }
		public double Breaks { get; set; }
		public double BreaksDeficit { get; set; }
		public double BreaksProductivityLoss { get; set; }
		public double BreaksAnnualLoss { get; set; }
		public string BreaksMessage { get; set; }
		public double BreaksMonetaryLoss { get; set; }
		public double TotalBreakTime { get; set; }
		public double TotalWeeklyMonetaryLoss { get; set; }
		public double TotalProductivityLoss { get; set; }
		public double TotalYearlyMonetaryLoss { get; set; }
		public string SummaryMessage { get; set; }
		public double SleepChart1 { get; set; }
		public double SleepChart2 { get; set; }
		public double BreaksChart1 { get; set; }
		public double  BreaksChart2 { get; set; }
		public double WaterChart1 { get; set; }
		public double WaterChart2 { get; set; }

	}
}
