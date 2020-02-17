using Microsoft.Research.SEAL;
using Produce_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.Encryption
{
	public class BfvEncryptor
	{
		public EncryptionParameters parms;
		public SEALContext context;
		public (PublicKey, SecretKey) keys;
		public ulong polyModulusDegree = 8192;
		public KeyGenerator keygen;
		public Encryptor encryptor;
		public IntegerEncoder encoder;
		public BfvEncryptor()
		{
			//Setting up the Encryption parameters for my Encryption,Creating SealContext
			//Setting the PolyModulusDegree and assigning a Coefficient Modulus.
			//Creates an instance of the SEAL context.
			//----------------------------------------------------------------
			parms = new EncryptionParameters(SchemeType.BFV);
			parms.PolyModulusDegree = polyModulusDegree;
			parms.CoeffModulus = CoeffModulus.BFVDefault(polyModulusDegree);
			parms.PlainModulus = PlainModulus.Batching(polyModulusDegree, 20);
			context = new SEALContext(parms);
			//-----------------------------------------------------------------
			//Construct a KeyGenerator using the SealContext and assign
			//The public and Private key values to the keys Variable.
			keygen = new KeyGenerator(context);
			keys = (keygen.PublicKey, keygen.SecretKey);
			//Create an instance of an encryptor to encrypt Data.
			encryptor = new Encryptor(context, keys.Item1);
			encoder = new IntegerEncoder(context);
		}

		public (PublicKey,SecretKey) GetKeys()
		{
			//Return the keys generated upon instantiation.
			return (keys.Item1, keys.Item2);
		}

		public Ciphertext EncryptData(int number)
		{
			Plaintext plainText = new Plaintext();
			Ciphertext cipherText = new Ciphertext();

			plainText = encoder.Encode(number);

			//Encrypted Data saved in cipherText Variable
			encryptor.Encrypt(plainText, cipherText);

			return cipherText;
		}

		public EncryptedDataModel EncryptModel(UserDataModel userData)
		{
			//Takes in a userDataModel and returns an EncryptedDataModel
			EncryptedDataModel EncryptedData = new EncryptedDataModel();
			EncryptedData.Age = EncryptData(userData.Age);
			EncryptedData.Height = EncryptData(userData.Height);
			EncryptedData.HoursWeek = EncryptData(userData.Hours);
			EncryptedData.Weight = EncryptData(userData.Weight);
			EncryptedData.Sleep = EncryptData(userData.Sleep);
			EncryptedData.Salary = EncryptData(userData.Salary);
			EncryptedData.Water = EncryptData(userData.WaterPerDay);
			EncryptedData.Breaks = EncryptData(userData.Breaks);


			return EncryptedData;
		}

		public Ciphertext encryptInt(Plaintext encodedInteger)
		{
			//Create a Ciphertext Object.
			Ciphertext encryptedInteger = new Ciphertext();
			//Encrypt encodedInteger and store the Ciphertext in encryptedInteger.
			encryptor.Encrypt(encodedInteger, encryptedInteger);

			return encryptedInteger;
		}
	}
}
