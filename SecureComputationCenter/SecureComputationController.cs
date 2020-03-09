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
			
			//Find sleep deficit and store in SleepProductivityLoss attribute of the cnryptedDataModel structure.
			encryptedDataModel.SleepProductivityLoss = sleepComputation.GetProductivityDeficit(encryptedDataModel.Sleep);

			//Hydration Computation.
			encryptedDataModel.WaterProductivityLoss = hydrationComputation.DehydrationComputation(encryptedDataModel.Water);

			encryptedDataModel.WeeklySalary = salaryComputation.GetWeeklySalary(encryptedDataModel.Salary);

			encryptedDataModel.TotalBreakTime = breaksComputation.GetBreaks(encryptedDataModel.Breaks);
			encryptedDataModel.HoursPerDay = breaksComputation.GetDailyHours(encryptedDataModel.HoursWeek);
			//encryptedDataModel.HoursPerDay = breaksComputation.GetDailyHours(encryptedDataModel.HoursWeek);
			//BMI not working throwing error.
			encryptedDataModel.BMI = bmiComputation.ComputeBMI(encryptedDataModel.HeightOverOne, encryptedDataModel.Weight);
			return encryptedDataModel;
		}
	}
}
