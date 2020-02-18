using Microsoft.Research.SEAL;
using Produce_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.Encryption
{
	public class BfvDecryptor
	{
		public SEALContext context;
		public IntegerEncoder encoder;
		public Decryptor decryptor;
		public BfvDecryptor(SEALContext Context,SecretKey PrivateKey)
		{
			//Takes in The original seal context as an argument.
			context = Context;
			//-----------------------------------------------------------------
			//Create an instance of Decryptor with the Generated Private Key.
			decryptor = new Decryptor(context, PrivateKey);
			encoder = new IntegerEncoder(context);
		}

		public int DecryptInt(Ciphertext EncryptedInteger)
		{
			//Takes in an encrypted Integer and Decrypts then Decodes to Int32.
			Plaintext plaintextOutput = new Plaintext();
			decryptor.Decrypt(EncryptedInteger, plaintextOutput);
			//Decode the decrypted output.
			int intDecoded;
			intDecoded = encoder.DecodeInt32(plaintextOutput);

			return intDecoded;
		}

		public UserDataModel DecryptEncryptedDataModel(EncryptedDataModel encryptedDataModel)
		{
			//Takes in an EncryptedDataModel and Returns a UserDataModel.
			UserDataModel userDataModel = new UserDataModel();
			userDataModel.Breaks = DecryptInt(encryptedDataModel.Breaks);
			userDataModel.Height = DecryptInt(encryptedDataModel.Height);
			userDataModel.Age = DecryptInt(encryptedDataModel.Age);
			userDataModel.Salary = DecryptInt(encryptedDataModel.Salary);
			userDataModel.Sleep = DecryptInt(encryptedDataModel.Sleep);
			userDataModel.WaterPerDay = DecryptInt(encryptedDataModel.Water);
			userDataModel.Hours = DecryptInt(encryptedDataModel.HoursWeek);
			userDataModel.Weight = DecryptInt(encryptedDataModel.Weight);

			return userDataModel;
		}
	}
}
