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
			NormaliseValues(computedDataModel);
			return computedDataModel;
		}
		private double GetSleepLoss(double Sleep)
		{
			if (Sleep <= -1)
			{
				//If user has had less than 6 hours sleep.
				Sleepmessage = "Per night you sleep <= 6 hours per night.";
				return 0.05;
			}

			else if (Sleep == 0)
			{
				Sleepmessage = "You are receving 8 hours of sleep per night";
				return 0.00;
			}

			else
			{
				Sleepmessage = "you have had 8+ Hours of sleep";
				return 0.05;
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
			computedDataModel.TotalMonetaryLoss = (computedDataModel.WaterMonetaryLoss + computedDataModel.SleepMonetaryLoss + computedDataModel.BreaksMonetaryLoss);
			return computedDataModel;
		}

		private void NormaliseValues(ComputedDataModel computedDataModel)
		{
			computedDataModel.Age = Math.Round(computedDataModel.Age, 0);
			computedDataModel.Breaks = Math.Round(computedDataModel.Breaks, 0);
			computedDataModel.BreaksMonetaryLoss = Math.Round(computedDataModel.BreaksMonetaryLoss, 2);
			//Was going to put a breaks round here but there is currently no deficit.
			computedDataModel.WeeklyHours = Math.Round(computedDataModel.WeeklyHours, 1);
			computedDataModel.DailyHours = Math.Round(computedDataModel.DailyHours, 2);
			computedDataModel.Salary = Math.Round(computedDataModel.Salary, 2);
			computedDataModel.WeeklySalary = Math.Round(computedDataModel.WeeklySalary, 2);
			computedDataModel.Sleep = Math.Round(computedDataModel.Sleep, 2);
			computedDataModel.SleepMonetaryLoss = Math.Round(computedDataModel.SleepMonetaryLoss, 2);
			computedDataModel.Water = Math.Round(computedDataModel.Water);
			computedDataModel.WaterMonetaryLoss = Math.Round(computedDataModel.WaterMonetaryLoss, 2);
		}
	}
}
