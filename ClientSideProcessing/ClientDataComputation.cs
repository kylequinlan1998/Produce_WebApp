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
			//totalProductivityLost.Add(GetBreakLoss(ComputedDoubleList[],ComputedDoubleList[]))
			//totalProductivityLost += GetBreakLoss(ComputedDoubleList[1],ComputedDoubleList[6]);
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
	}
}
