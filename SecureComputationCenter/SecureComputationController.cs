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
		public HydrationComputation sleepComputation;
		public SecureComputationController()
		{
			sleepComputation = new HydrationComputation();
		}

		public EncryptedDataModel RunSecureComputation(EncryptedDataModel EncryptedDataModel)
		{
			//Takes in an encryptedDataModel and returns and EncryptedDataModel
			//With Water computation complete.
			EncryptedDataModel EncryptedResult = new EncryptedDataModel();
			EncryptedResult.Water = sleepComputation.DehydrationComputation(EncryptedDataModel.Water);

			return EncryptedResult;
		}
	}
}
