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
		
		/*public List<double> DecryptModel(EncryptedDataModel encryptedDataModel)
		{
			//Takes in a Ciphertext and returns a List of Plaintext.
			List<double> doubleList = new List<double>();

			//Decrypt to Plaintext.
			doubleList.Add(PrepareModel(encryptedDataModel.Age));
			//Remove BMI for now.
			//doubleList.Add(PrepareModel(encryptedDataModel.BMI));
			doubleList.Add(PrepareModel(encryptedDataModel.Breaks));
			doubleList.Add(PrepareModel(encryptedDataModel.Height));
			doubleList.Add(PrepareModel(encryptedDataModel.Weight));
			doubleList.Add(PrepareModel(encryptedDataModel.Water));
			doubleList.Add(PrepareModel(encryptedDataModel.Sleep));
			doubleList.Add(PrepareModel(encryptedDataModel.HoursWeek));
			doubleList.Add(PrepareModel(encryptedDataModel.HoursPerDay));
			doubleList.Add(PrepareModel(encryptedDataModel.Salary));
			doubleList.Add(PrepareModel(encryptedDataModel.SleepProductivityLoss));
			doubleList.Add(PrepareModel(encryptedDataModel.WaterProductivityLoss));
			doubleList.Add(PrepareModel(encryptedDataModel.WeeklySalary));
			doubleList.Add(PrepareModel(encryptedDataModel.TotalBreakTime));
			return doubleList;
		}*/
		public ComputedDataModel DecryptModel(EncryptedDataModel encryptedDataModel)
		{
			ComputedDataModel computedDataModel = new ComputedDataModel();
			// Store returned double value in given attribute
			computedDataModel.Age = PrepareModel(encryptedDataModel.Age);
			computedDataModel.Height = PrepareModel(encryptedDataModel.Height);
			computedDataModel.Weight = PrepareModel(encryptedDataModel.Weight);
			computedDataModel.Breaks = PrepareModel(encryptedDataModel.Breaks);
			computedDataModel.TotalBreakTime = PrepareModel(encryptedDataModel.TotalBreakTime);
			computedDataModel.WeeklyHours = PrepareModel(encryptedDataModel.HoursWeek);
			computedDataModel.DailyHours = PrepareModel(encryptedDataModel.HoursPerDay);
			computedDataModel.Salary = PrepareModel(encryptedDataModel.Salary);
			computedDataModel.WeeklySalary = PrepareModel(encryptedDataModel.WeeklySalary);
			computedDataModel.Sleep = PrepareModel(encryptedDataModel.Sleep);
			computedDataModel.SleepDeficit = PrepareModel(encryptedDataModel.SleepProductivityLoss);
			computedDataModel.Water = PrepareModel(encryptedDataModel.Water);
			computedDataModel.WaterDeficit = PrepareModel(encryptedDataModel.WaterProductivityLoss);
			
			// Return computedDataModel
			return computedDataModel;
		}

		public double PrepareModel(Ciphertext cipherInput)
		{
			 
			Plaintext decoded = new Plaintext();
			List<double> doubleList = new List<double>();
			// Decrypt CiphertextInput
			decryptor.Decrypt(cipherInput,decoded);
			// Decode plaintext
			encoder.Decode(decoded, doubleList);
			double decode;
			decode = doubleList.First();
			// Return double value
			return decode;
		}
     }
}
