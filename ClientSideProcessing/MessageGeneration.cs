using Produce_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.ClientSideProcessing
{
	public class MessageGeneration
	{
		public MessageGeneration()
		{
			//Constructor
		}

		public ComputedDataModel GetHealthMessage(ComputedDataModel computedDataModel)
		{
			computedDataModel = GetSummaryMessage(computedDataModel);
			computedDataModel = GetHydrationMessage(computedDataModel);
			return computedDataModel;
		}

		private ComputedDataModel GetSummaryMessage(ComputedDataModel computedDataModel)
		{
			if (computedDataModel.TotalProductivityLoss == 0)
			{
				computedDataModel.SummaryMessage = "Congrats! You are operating at the most Productive you can be. Your water intake is healthy as you have drank " + computedDataModel.Water + " Glasses of water today!";
				computedDataModel.SummaryMessage += " You have also taken at least one break every Hour in your " + computedDataModel.DailyHours + " Hour working Day.";
				computedDataModel.SummaryMessage += "This coupled with Nightly sleep of " + computedDataModel.Sleep + " Hours means you are functioning at full capacity.";
				computedDataModel.SummaryMessage += "For more detailed information click one of the subheadings from the navigation bar!";
				return computedDataModel;
			}
			else
			{
				computedDataModel.SummaryMessage = "Oops! Unfortunatley you are not working at full capacity due to your physical habits. As a result of this you are Loosing " + (computedDataModel.TotalProductivityLoss * 100) +
					"% of your productivity. This can easily be remedied by by simple means such as more sleep, breaks and water. For a more detailed analysis click one of the subheadings from the navigation bar!";
				return computedDataModel;
			}
		}

		private ComputedDataModel GetHydrationMessage(ComputedDataModel computedDataModel)
		{
			if(computedDataModel.WaterDeficit < 0)
			{
				computedDataModel.WaterMessage = "You have not consumed enough water to perform at full capacity, this can result in lack of concentration and efficiency. You have only consumed " +
					computedDataModel.Water + "Glasses of water. This is below the recommended amount.";
				return computedDataModel;
			}
			else if (computedDataModel.WaterDeficit == 0)
			{
				computedDataModel.WaterMessage = "Awesome! You have drank the correct amount of water, This means you are more alert and feeling better. Your intake of " +
					computedDataModel.Water + "Glasses of water is inline with the recommended daily amount.";
				return computedDataModel;
			}
			else
			{
				computedDataModel.WaterMessage = "Woah! You have gone above and beyond. You are consuming more than the recommended amount,This will contribute to mood and efficiency. Your intake of" +
					computedDataModel.Water + " Glasses of water is above average. Be careful not to over do it as too much water can be dangerous.";
				return computedDataModel;
			}
		}

		private ComputedDataModel GetSleepMessage(ComputedDataModel computedDataModel)
		{
			if (computedDataModel.SleepDeficit < 0)
			{
				computedDataModel.SleepMessage = "You are unfortunatley,Sleep deprived. By only sleeping for " + computedDataModel.Sleep +
					" Hours you are reducing your cognitive ability and efficiency at tasks. This can be remedied by increasing your nightly sleep.";
				computedDataModel.SleepMessage += "You are currently operating on a sleep deficit of " + computedDataModel.SleepDeficit;
				return computedDataModel;
			}
			else if (computedDataModel.SleepDeficit == 0)
			{
				computedDataModel.SleepMessage = "Perfect! You are receiving the correct amount of sleepy per night. This has many benefits such as a larger working memory,increased multitasking ability and more efficient output.";
				computedDataModel.SleepMessage = "Receiving " + computedDataModel.Sleep + "Also both increase energy mood and wellbeing. Good work!";
				return computedDataModel;
			}
			else
			{
				computedDataModel.SleepMessage = "Zzzzz. You are no stanger to sleeping! Getting a good nights sleep is essential to efficient work and a better mood.";
				computedDataModel.SleepMessage += "By sleeping for " + computedDataModel.Sleep + " Hours per night you are being well rested. You have a sleep surplus of" + computedDataModel.SleepDeficit;
				return computedDataModel;
			}
		}

	}
}
