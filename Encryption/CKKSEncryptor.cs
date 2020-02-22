﻿using Microsoft.Research.SEAL;
using Produce_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.Encryption
{
	public class CKKSEncryptor
	{
		EncryptionParameters parms;
		ulong polyModulusDegree = 8192;
		double scale;
		SEALContext context;
		CKKSEncoder encoder;
		Encryptor encryptor;
		KeyGenerator keygen;
		PublicKey publicKey;
		SecretKey secretKey;
		public CKKSEncryptor()
		{
			//Set scheme Primes and encryption parameters.
			parms = new EncryptionParameters(SchemeType.CKKS);
			parms.PolyModulusDegree = polyModulusDegree;
			parms.CoeffModulus = CoeffModulus.Create(
				polyModulusDegree, new int[] { 60, 40, 40, 60 });
			scale = Math.Pow(2.0, 40);
			context = new SEALContext(parms);
			keygen = new KeyGenerator(context);
			//Generate private and public key.
			publicKey = keygen.PublicKey;
			secretKey = keygen.SecretKey;
			encryptor = new Encryptor(context, publicKey);
			encoder = new CKKSEncoder(context);
		}

		public Ciphertext EncryptLong(long input)
		{
			//Takes in a input of type long and returns a Ciphertext.
			Plaintext encoded = new Plaintext();
			Ciphertext encrypted = new Ciphertext();

			encoder.Encode(input, encoded);

			encryptor.Encrypt(encoded, encrypted);

			return encrypted;
		}

		public EncryptedDataModel EncryptModel(InputDataModel userData)
		{
			//Takes in a UserDataModel and returns an EncryptedDataModel.
			//List<double> outputList = new List<double>(1);
			EncryptedDataModel encryptedDataModel = new EncryptedDataModel();
			encryptedDataModel.Age = EncryptLong(userData.Age);
			encryptedDataModel.BMI = EncryptLong(userData.BMI);
			encryptedDataModel.Breaks = EncryptLong(userData.Breaks);
			encryptedDataModel.Height = EncryptLong(userData.Height);
			encryptedDataModel.HoursWeek = EncryptLong(userData.Age);
			encryptedDataModel.Salary = EncryptLong(userData.Age);
			encryptedDataModel.Sleep = EncryptLong(userData.Age);
			encryptedDataModel.Water = EncryptLong(userData.WaterPerDay);
			encryptedDataModel.Weight = EncryptLong(userData.Weight);

			return encryptedDataModel;
		}
	}
}
