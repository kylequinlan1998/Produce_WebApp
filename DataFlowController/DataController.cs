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
	public class DataController
	{
		//used for encryption and decryption.
		public BfvEncryption encryption;
		public DataController()
		{
			//Create an instance of bfv Encryption
		    encryption = new BfvEncryption();
		}

		public EncryptedDataModel ProcessDataModel(UserDataModel UserModel)
		{
			//Takes in a UserDataModel and Returns an EncryptedDataModel.
			EncryptedDataModel EncryptedData = new EncryptedDataModel();
			EncryptedData = encryption.EncryptModel(UserModel);

			return EncryptedData;
		}
	}
}
