using Microsoft.Research.SEAL;
using Produce_WebApp.Encryption;
using Produce_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Produce_WebApp.SecureComputationCenter;
using Produce_WebApp.ClientSideProcessing;

namespace Produce_WebApp.DataFlowController
{
	public class FlowController
	{
		//Access to tools to encrypt My model.
		CKKSEncryptor EncryptionTools;
		CKKSDecryptor DecryptionTools;
		private SecureComputationController secureComputation;
		private ClientDataComputation clientComputation;
		//used for encryption and decryption.
		public FlowController()
		{
			//Create an instance of bfv Encryption
			EncryptionTools = new CKKSEncryptor();
			(PublicKey,SecretKey) Keys = EncryptionTools.GetKeys();
			//Begins an instance of Decrypto by passding in seal context and Secret Key.
			DecryptionTools = new CKKSDecryptor(EncryptionTools.context,Keys.Item2);
			secureComputation = new SecureComputationController();
			clientComputation = new ClientDataComputation();
		}

		public void StartDataProcessing(InputDataModel UserDataPlain)
		{
			//The encrypted DataModel
			var EncryptedDataModel = EncryptDataModel(UserDataPlain);

			//The Model with secure computation performed.
			var intermediate = secureComputation.RunSecureComputation(EncryptedDataModel);

			//The decrypted List of doubles after performing computatio.
			var DecryptdListOfDoubles = DecryptDataModel(intermediate);

			//Pass the decrypted resuts to client side computation center.
			//Bmi has the value of height squared at this point in the code.

			//userDataModel = DecryptDataModel(EncryptedDataModel);
		}

		public EncryptedDataModel EncryptDataModel(InputDataModel UserModel)
		{
			//Takes in a UserDataModel and Returns an EncryptedDataModel.
			EncryptedDataModel EncryptedData = new EncryptedDataModel();
			EncryptedData = EncryptionTools.EncryptModel(UserModel);

			return EncryptedData;
		}

		public List<double> DecryptDataModel(EncryptedDataModel encryptedDataModel)
		{
			//Takes in an encryptedDataModel and returns a List of Doubles.
			var DoubleList = DecryptionTools.DecryptModel(encryptedDataModel);

			return DoubleList;
		}
	}
}
