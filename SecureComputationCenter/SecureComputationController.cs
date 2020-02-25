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
		public SecureComputationController()
		{
			hydrationComputation = new HydrationComputation();
			sleepComputation = new SleepComputation();
			bmiComputation = new BMIComputation();
			salaryComputation = new SalaryComputation();
		}

		public EncryptedDataModel RunSecureComputation(EncryptedDataModel encryptedDataModel)
		{
			
			//Find sleep deficit and store in SleepProductivityLoss attribute of the cnryptedDataModel structure.
			encryptedDataModel.SleepProductivityLoss = sleepComputation.GetProductivityDeficit(encryptedDataModel.Sleep);

			//Hydration Computation.
			encryptedDataModel.WaterProductivityLoss = hydrationComputation.DehydrationComputation(encryptedDataModel.Water);

			encryptedDataModel.WeeklySalary = salaryComputation.GetWeeklySalary(encryptedDataModel.Salary);
			//BMI not working throwing error.
			//encryptedDataModel.BMI = bmiComputation.ComputeBMI(encryptedDataModel.Height, encryptedDataModel.Weight);
			return encryptedDataModel;
		}
	}
}
