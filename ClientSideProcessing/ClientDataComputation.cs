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
		public ClientDataComputation()
		{

		}

		private double GetSleepLoss(UserDataModel userData)
		{
			if (userData.Sleep <= -2)
			{
				//If user has had less than 6 hours sleep.
				Sleepmessage = "Per night you sleep <= 6 hours per night.";
				return 0.05;
			}

			else if (userData.Sleep == 0)
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

		private double GetHydrationLoss(UserDataModel userData)
		{
			if (userData.WaterPerDay < -1)
			{
				HydrationMessage = "You are severley Dehydrated";
				return 0.10;
			}

			else if (userData.WaterPerDay == 0)
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

		public int DivideWeightByHeight(UserDataModel userData)
		{
			var value = Convert.ToInt32(userData.Weight / userData.Height);
			return  value ;
		}
	}
}
