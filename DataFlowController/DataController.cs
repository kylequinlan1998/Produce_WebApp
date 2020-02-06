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
		public BfvEncryption encryption;
		public CloudInterface cloudInterface;
		public DataController()
		{
			//Create an instance of bfv Encryption
		    encryption = new BfvEncryption();
		}

		public async Task RunEncryption(UserDataModel userData)
		{
			//cloudInterface = new CloudInterface();
			//Create instance of Encryption Class and call matrixCreation.
			Plaintext plain = new Plaintext();
			Ciphertext cipher = new Ciphertext();
			//Encode and integer.
			plain = encryption.encodeInt(10);
			//Encrypt integer
			cipher = encryption.encryptInt(plain);
			//Create an instance of the Cloud Interface.
			cloudInterface = new CloudInterface();
			await cloudInterface.CallApi(cipher);
			
		}

		
		public void DecryptResult(Ciphertext cipher)
		{
			//Ciphertext input = (Ciphertext)obj;
			Plaintext plainOutput = new Plaintext();
			int integerOutput;
			encryption.decryptor.Decrypt(cipher, plainOutput);
			integerOutput = encryption.encoder.DecodeInt32(plainOutput);
			Debug.WriteLine(integerOutput);
		}
		
	}
}
