using Microsoft.Research.SEAL;
using Produce_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.Encryption
{
	public class CKKSDecryptor
	{
		double scale;
		SEALContext context;
		Decryptor decryptor;
		CKKSEncoder encoder;
		public CKKSDecryptor(SEALContext Context,SecretKey privateKey)
		{
			//Takes in a context and private key and creates instance of decoder and Context.
			context = Context;
			scale = Math.Pow(2.0, 30);
			decryptor = new Decryptor(Context, privateKey);
			encoder = new CKKSEncoder(Context);
		}
		
		public List<double> DecryptModel(EncryptedDataModel encryptedDataModel)
		{
			//Takes in a Ciphertext and returns a List of Plaintext.
			Plaintext decrypted = new Plaintext();
			UserDataModel userData = new UserDataModel();
			List<double> doubleList = new List<double>(9);

			//Decrypt to Plaintext.
			doubleList[0] = PrepareModel(encryptedDataModel.Age);
			doubleList[1] = PrepareModel(encryptedDataModel.BMI);
			doubleList[2] = PrepareModel(encryptedDataModel.Breaks);
			doubleList[3] = PrepareModel(encryptedDataModel.Height);
			doubleList[4] = PrepareModel(encryptedDataModel.Weight);
			doubleList[5] = PrepareModel(encryptedDataModel.Water);
			doubleList[6] = PrepareModel(encryptedDataModel.Sleep);
			doubleList[7] = PrepareModel(encryptedDataModel.HoursWeek);
			doubleList[8] = PrepareModel(encryptedDataModel.Salary);

			return doubleList;
		}

		public double PrepareModel(Ciphertext cipherInput)
		{
			//Takes in an encryptedDataModel and returns a 
			Plaintext decoded = new Plaintext();
			List<double> doubleList = new List<double>();

			decryptor.Decrypt(cipherInput,decoded);
			
			encoder.Decode(decoded, doubleList);
			//Failing here
			return doubleList[1];
		}

		public double TestingDecrypt(Ciphertext cipher)
		{
			Plaintext plain = new Plaintext();
			//Decrypt the data to a plaintext.
			decryptor.Decrypt(cipher, plain);
			List<double> test = new List<double>();
			encoder.Decode(plain, test);
			Debug.WriteLine(test[0]);

			return test[0];
		}

            

	 



     }
}
