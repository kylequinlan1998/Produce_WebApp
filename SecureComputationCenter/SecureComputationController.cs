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
		public SecureComputationController()
		{
			hydrationComputation = new HydrationComputation();
			sleepComputation = new SleepComputation();
			bmiComputation = new BMIComputation();
		}

		public EncryptedDataModel RunSecureComputation(EncryptedDataModel encryptedDataModel)
		{
			//Takes in an encryptedDataModel and returns and EncryptedDataModel
			//With Water computation complete.
			//EncryptedDataModel EncryptedResult = new EncryptedDataModel();
			//Find the water deficit.
			encryptedDataModel.WaterProductivityLoss = hydrationComputation.DehydrationComputation(encryptedDataModel.Water);
			//Find sleep deficit.
			encryptedDataModel.SleepProductivityLoss = sleepComputation.GetProductivityDeficit(encryptedDataModel.Sleep);

			//EncryptedResult.BMI = bmiComputation.GetHeightSquared(encryptedDataModel.Height);

			return encryptedDataModel;
		}
	}
}
