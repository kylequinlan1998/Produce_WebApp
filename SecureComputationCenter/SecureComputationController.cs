using Microsoft.Research.SEAL;
using Produce_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.SecureComputationCenter
{
	public class SecureComputationController
	{
		private HydrationComputation hydrationComputation;
		private SleepComputation sleepComputation;
		private BMIComputation bmiComputation;
		private SalaryComputation salaryComputation;
		private BreaksComputation breaksComputation;
		public SecureComputationController(RelinKeys Keys)
		{
			hydrationComputation = new HydrationComputation();
			sleepComputation = new SleepComputation();
			bmiComputation = new BMIComputation(Keys);
			salaryComputation = new SalaryComputation();
			breaksComputation = new BreaksComputation(Keys);
		}

		public EncryptedDataModel RunSecureComputation(EncryptedDataModel encryptedDataModel)
		{
			
			// Sleep Computation
			encryptedDataModel.SleepProductivityLoss = sleepComputation.GetProductivityDeficit(encryptedDataModel.Sleep);
			// Hydration Comutation.
			encryptedDataModel.WaterProductivityLoss = hydrationComputation.DehydrationComputation(encryptedDataModel.Water);
			// Weekly Salary Computation
			encryptedDataModel.WeeklySalary = salaryComputation.GetWeeklySalary(encryptedDataModel.Salary);
			// Total Break Time Computation.
			encryptedDataModel.TotalBreakTime = breaksComputation.GetBreaks(encryptedDataModel.Breaks);
			// Hours per day computation
			encryptedDataModel.HoursPerDay = breaksComputation.GetDailyHours(encryptedDataModel.HoursWeek);
			
			return encryptedDataModel;
		}
	}
}
