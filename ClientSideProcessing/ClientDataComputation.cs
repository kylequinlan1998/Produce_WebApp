using Produce_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.ClientSideProcessing
{
	public class ClientDataComputation
	{
		private string Sleepmessage;
		private string HydrationMessage;
		private string BreaksMessage;
		public ClientDataComputation()
		{

		}

		public ComputedDataModel TotalProductivityLost(ComputedDataModel computedDataModel)
		{
			//Add value to a list of doubles.
			computedDataModel.SleepProductivityLoss = (GetSleepLoss(computedDataModel.SleepDeficit));

			computedDataModel.WaterProductivityLoss = GetHydrationLoss(computedDataModel.WaterDeficit);
			computedDataModel.BreaksProductivityLoss = GetBreakLoss(computedDataModel.Breaks, computedDataModel.DailyHours);

			computedDataModel.TotalProductivityLoss = computedDataModel.WaterProductivityLoss;
			computedDataModel.TotalProductivityLoss += computedDataModel.BreaksProductivityLoss;
			computedDataModel.TotalProductivityLoss += computedDataModel.SleepProductivityLoss;

			computedDataModel = TotalMonetaryLoss(computedDataModel);
			computedDataModel = NormaliseValues(computedDataModel);

			computedDataModel = GetChartValues(computedDataModel);

			return computedDataModel;
		}
		private double GetSleepLoss(double Sleep)
		{
			if (Sleep <= -1)
			{
				// Less than 8 hours
				return 0.05;
			}

			else if (Sleep == 0)
			{
				// 8 hours
				return 0.00;
			}

			else
			{
				//More than 8 hours
				return 0.07;
			}

		}

		private double GetHydrationLoss(double Water)
		{
			if (Water <= -1)
			{
				HydrationMessage = "You are Dehydrated";
				return 0.10;
			}

			else if (Water == 0)
			{
				HydrationMessage = "You are at the recommended level of water intake";
				return 0;
			}

			else
			{
				HydrationMessage = "You have consumed more than the recommended water levels. Be careful";
				return 0;
			}
		}

		private double GetBreakLoss(double Breaks,double HoursPerDay)
		{
			if(Breaks < HoursPerDay)
			{
				BreaksMessage = "You have not Taken enough breaks";
				return 0.05;
			}
			else if (Breaks == HoursPerDay)
			{
				BreaksMessage = "You have taken the correct amount of breaks";
				return 0.00;
			}
			else
			{
				BreaksMessage = "You have taken More breaks than required,aim for one an hour.";
				return 0.00;
			}
		}

		private ComputedDataModel TotalMonetaryLoss(ComputedDataModel computedDataModel)
		{
			//Compute the total Monetary cost per week due to bad working conditions.
			//Hydration
			computedDataModel.WaterMonetaryLoss = (computedDataModel.WaterProductivityLoss * computedDataModel.WeeklySalary);
			computedDataModel.SleepMonetaryLoss= (computedDataModel.SleepProductivityLoss * computedDataModel.WeeklySalary);
			computedDataModel.BreaksMonetaryLoss = (computedDataModel.BreaksProductivityLoss * computedDataModel.WeeklySalary);
			computedDataModel.TotalWeeklyMonetaryLoss = (computedDataModel.WaterMonetaryLoss + computedDataModel.SleepMonetaryLoss + computedDataModel.BreaksMonetaryLoss);
			computedDataModel.TotalYearlyMonetaryLoss = (computedDataModel.Salary * computedDataModel.TotalProductivityLoss);
			computedDataModel.WaterAnnualLoss = (computedDataModel.WaterMonetaryLoss * 52);
			computedDataModel.SleepAnnualLoss = (computedDataModel.SleepMonetaryLoss * 52);
			computedDataModel.BreaksAnnualLoss = (computedDataModel.BreaksMonetaryLoss * 52);
			return computedDataModel;
		}

		private ComputedDataModel NormaliseValues(ComputedDataModel computedDataModel)
		{
			computedDataModel.Age = Math.Round(computedDataModel.Age, 0);
			computedDataModel.Breaks = Math.Round(computedDataModel.Breaks, 0);
			computedDataModel.BreaksMonetaryLoss = Math.Round(computedDataModel.BreaksMonetaryLoss, 2);
			computedDataModel.WeeklyHours = Math.Round(computedDataModel.WeeklyHours, 1);
			computedDataModel.DailyHours = Math.Round(computedDataModel.DailyHours, 2);
			computedDataModel.Salary = Math.Round(computedDataModel.Salary, 2);
			computedDataModel.WeeklySalary = Math.Round(computedDataModel.WeeklySalary, 2);
			computedDataModel.Sleep = Math.Round(computedDataModel.Sleep, 2);
			computedDataModel.SleepMonetaryLoss = Math.Round(computedDataModel.SleepMonetaryLoss, 2);
			computedDataModel.Water = Math.Round(computedDataModel.Water);
			computedDataModel.WaterMonetaryLoss = Math.Round(computedDataModel.WaterMonetaryLoss, 2);
			computedDataModel.TotalWeeklyMonetaryLoss = Math.Round(computedDataModel.TotalWeeklyMonetaryLoss, 2);
			computedDataModel.TotalYearlyMonetaryLoss = Math.Round(computedDataModel.TotalYearlyMonetaryLoss, 2);
			computedDataModel.WaterDeficit = Math.Round(computedDataModel.WaterDeficit);
			computedDataModel.SleepDeficit = Math.Round(computedDataModel.SleepDeficit);
			computedDataModel.SleepAnnualLoss = Math.Round(computedDataModel.SleepAnnualLoss, 2);
			computedDataModel.BreaksAnnualLoss = Math.Round(computedDataModel.BreaksAnnualLoss, 2);
			computedDataModel.WaterAnnualLoss = Math.Round(computedDataModel.WaterAnnualLoss, 2);

			return computedDataModel;
		}

		private void MessageGeneration(ComputedDataModel computedDataModel)
		{

		}

		private ComputedDataModel GetChartValues(ComputedDataModel computedDataModel)
		{
			//Get Break values for chart.
			computedDataModel.BreaksChart1 = computedDataModel.BreaksProductivityLoss*100;
			computedDataModel.BreaksChart2 = (100 - computedDataModel.BreaksChart1);
			//Get Sleep values for chart.
			computedDataModel.SleepChart1 = computedDataModel.SleepProductivityLoss*100;
			computedDataModel.SleepChart2 = 100 - computedDataModel.SleepChart1;
			//Get Water values for chart.
			computedDataModel.WaterChart1 = computedDataModel.WaterProductivityLoss*100;
			computedDataModel.WaterChart2 = 100 - computedDataModel.WaterChart1;

			return computedDataModel;
		}
		
	}
}
