using Microsoft.Research.SEAL;
using Produce_WebApp.Encryption;
using Produce_WebApp.Models;
using Produce_WebApp.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace Produce_WebApp.DataFlowController
{
	public class DataController
	{
		public DataController(UserDataModel userData )
		{
			//Encode and Encrypt UserDataModel in a matrix.
			//RunEncryption(userData);
			TestRequest req = new TestRequest();
		}

		public void RunEncryption(UserDataModel userData)
		{
			//Create instance of Encryption Class and call matrixCreation.
			BfvEncryption bfv = new BfvEncryption();
			bfv.testing(userData);


		}

		
	}
}
