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
			doubleList.Add(PrepareModel(encryptedDataModel.Age));
			//doubleList.Add(PrepareModel(encryptedDataModel.BMI));
			doubleList.Add(PrepareModel(encryptedDataModel.Breaks));
			doubleList.Add(PrepareModel(encryptedDataModel.Height));
			doubleList.Add(PrepareModel(encryptedDataModel.Weight));
			doubleList.Add(PrepareModel(encryptedDataModel.Water));
			doubleList.Add(PrepareModel(encryptedDataModel.Sleep));
			doubleList.Add(PrepareModel(encryptedDataModel.HoursWeek));
			doubleList.Add(PrepareModel(encryptedDataModel.Salary));

			return doubleList;
		}

		public double PrepareModel(Ciphertext cipherInput)
		{
			//Takes in an encryptedDataModel and returns a 
			Plaintext decoded = new Plaintext();
			List<double> doubleList = new List<double>();

			decryptor.Decrypt(cipherInput,decoded);
			
			encoder.Decode(decoded, doubleList);
			double decode;
			decode = doubleList.First();
			//Failing here
			return decode;
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
