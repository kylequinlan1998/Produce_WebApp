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
			parms = new EncryptionParameters(SchemeType.BFV);
			parms.PolyModulusDegree = polyModulusDegree;
			parms.CoeffModulus = CoeffModulus.BFVDefault(polyModulusDegree);
			parms.PlainModulus = PlainModulus.Batching(polyModulusDegree, 20);
			context = new SEALContext(parms);
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

		public Plaintext matrixCreation(UserDataModel dataModel)
		{
			//Takes in a user data Model,Creates a matrix with the datamodel
			//Batch Encodes the Matrix to Plaintexts.
			ulong slotCount = batchEncoder.SlotCount;
			ulong rowSize = slotCount / 2;
			//Creates a matrix of integersof size slotcount
			//Casts the input values to unsigned long and stores
			//them in the matrix podMatrix.
			ulong[] podMatrix = new ulong[slotCount];
			podMatrix[0] = Convert.ToUInt64(dataModel.Age);
			podMatrix[1] = Convert.ToUInt64(dataModel.Height);
			podMatrix[2] = Convert.ToUInt64(dataModel.Weight);
			podMatrix[3] = Convert.ToUInt64(dataModel.Salary);
			podMatrix[4] = Convert.ToUInt64(dataModel.Sleep);
			podMatrix[5] = Convert.ToUInt64(dataModel.WaterPerDay);
			podMatrix[6] = Convert.ToUInt64(dataModel.HoursPerDay);
			
			//Create a new Matrix to store encoded integers.
			Plaintext plainMatrix = new Plaintext();
			Console.WriteLine("Encode plaintext matrix:");
			batchEncoder.Encode(podMatrix, plainMatrix);

			
			return plainMatrix;
		}

		public Ciphertext matrixEncryption(Plaintext encodedMatrix)
		{
			//Takes in a plantext matrix and Encrypts the matrix with 
			//the encryptor.
			Ciphertext encryptedMatrix = new Ciphertext();
			//Outputting encrypted matrix into encryptedMatrix.
			encryptor.Encrypt(encodedMatrix, encryptedMatrix);
			//Debug.WriteLine(decryptor.InvariantNoiseBudget(encryptedMatrix));

			return encryptedMatrix;
		}

		public Ciphertext Evaluate(Ciphertext input)
		{
			
			Plaintext plain = new Plaintext();
			plain = encodeInt(10);
			evaluator.AddPlainInplace(input,plain);

			return input;
		}

		public long testing(UserDataModel userData)
		{
			BfvEncryption bfv = new BfvEncryption();
			Plaintext plainEncoded = new Plaintext();
			Plaintext plainDeco = new Plaintext();
			plainDeco = encodeInt(10);
			plainEncoded = encodeInt(Convert.ToUInt64(userData.Age));

			Ciphertext encrypted = new Ciphertext();
			Ciphertext[] cipherArray = new Ciphertext[5];

			cipherArray[0] = encryptInt(plainEncoded);

			evaluator.AddPlainInplace(cipherArray[0], plainDeco);
			decryptor.Decrypt(cipherArray[0],plainDeco);

			long result = encoder.DecodeInt64(plainDeco);
			Debug.WriteLine(result);
			return result;

			//Plaintext value1 = 
			//bfv.evaluator.
		}
	}
}
