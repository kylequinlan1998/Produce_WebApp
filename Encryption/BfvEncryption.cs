using Microsoft.Research.SEAL;
using Produce_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.Encryption
{
	public class BfvEncryption
	{
		public BfvEncryption encryption;
		public EncryptionParameters parms;
		public SEALContext context;
		public (PublicKey, SecretKey) keys;
		public ulong polyModulusDegree = 8192;
		public KeyGenerator keygen;
		public Encryptor encryptor;
		public IntegerEncoder encoder;
		public Decryptor decryptor;
		public Evaluator evaluator;
		public BatchEncoder batchEncoder;
		public BfvEncryption()
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
			decryptor = new Decryptor(context, keys.Item2);
			evaluator = new Evaluator(context);
			encoder = new IntegerEncoder(context);
			batchEncoder = new BatchEncoder(context);

			//encryption = new BfvEncryption();
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
			EncryptedData.Age = encryption.EncryptData(userData.Age);
			EncryptedData.Height = encryption.EncryptData(userData.Height);
			EncryptedData.HoursWeek = encryption.EncryptData(userData.HoursPerWeek);
			EncryptedData.Weight = encryption.EncryptData(userData.Weight);
			EncryptedData.Sleep = encryption.EncryptData(userData.Sleep);
			EncryptedData.Water = encryption.EncryptData(userData.WaterPerDay);
			EncryptedData.Breaks = encryption.EncryptData(userData.Breaks);


			return EncryptedData;
			
		}
		public Plaintext encodeInt(ulong integer)
		{
			//Encoding an Integer using my encoder,Returns a Plaintext.
			Plaintext encodedInteger = new Plaintext();
			encodedInteger = encoder.Encode(integer);

			return encodedInteger;
		}

		public Ciphertext encryptInt(Plaintext encodedInteger)
		{
			//Create a Ciphertext Object.
			Ciphertext encryptedInteger = new Ciphertext();
			//Encrypt encodedInteger and store the Ciphertext in encryptedInteger.
			encryptor.Encrypt(encodedInteger, encryptedInteger);

			return encryptedInteger;
		}

		public Ciphertext Evaluate(Ciphertext input)
		{

			return new Ciphertext();
		}

		public int DecryptInt(Ciphertext cipherInput)
		{
			Plaintext plainOut = new Plaintext();
			decryptor.Decrypt(cipherInput, plainOut);
			int intOut;
			intOut = encoder.DecodeInt32(plainOut);

			return intOut;
		}
	}
}
