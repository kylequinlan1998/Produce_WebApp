using Microsoft.Research.SEAL;
using Produce_WebApp.Encryption;
using Produce_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Produce_WebApp.DataFlowController
{
	public class FlowController
	{
		//Access to tools to encrypt My model.
		BfvEncryptor EncryptionTools;
		//used for encryption and decryption.
		public FlowController()
		{
			//Create an instance of bfv Encryption
			EncryptionTools = new BfvEncryptor();
		}

		public EncryptedDataModel ProcessDataModel(UserDataModel UserModel)
		{
			//Takes in a UserDataModel and Returns an EncryptedDataModel.
			EncryptedDataModel EncryptedData = new EncryptedDataModel();
			EncryptedData = EncryptionTools.EncryptModel(UserModel);

			return EncryptedData;
		}
	}
}
