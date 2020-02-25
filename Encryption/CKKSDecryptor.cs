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
			scale = Math.Pow(2.0, 40);
			decryptor = new Decryptor(Context, privateKey);
			encoder = new CKKSEncoder(Context);
		}
		
		public List<double> DecryptModel(EncryptedDataModel encryptedDataModel)
		{
			//Takes in a Ciphertext and returns a List of Plaintext.
			List<double> doubleList = new List<double>();

			//Decrypt to Plaintext.
			doubleList.Add(PrepareModel(encryptedDataModel.Age));
			doubleList.Add(PrepareModel(encryptedDataModel.BMI));
			doubleList.Add(PrepareModel(encryptedDataModel.Breaks));
			doubleList.Add(PrepareModel(encryptedDataModel.Height));
			doubleList.Add(PrepareModel(encryptedDataModel.Weight));
			doubleList.Add(PrepareModel(encryptedDataModel.Water));
			doubleList.Add(PrepareModel(encryptedDataModel.Sleep));
			doubleList.Add(PrepareModel(encryptedDataModel.HoursWeek));
			doubleList.Add(PrepareModel(encryptedDataModel.Salary));
			//Keeps returning 11.
			doubleList.Add(PrepareModel(encryptedDataModel.SleepProductivityLoss));
			doubleList.Add(PrepareModel(encryptedDataModel.WaterProductivityLoss));

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
     }
}
