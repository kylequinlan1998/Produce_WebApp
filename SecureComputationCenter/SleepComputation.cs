using Microsoft.Research.SEAL;
using Produce_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.SecureComputationCenter
{
	public class SleepComputation
	{
		public EncryptionParameters parms;
		public ulong polyModulusDegree = 8192;
		public SEALContext context;
		public CKKSEncoder encoder;
		public Evaluator evaluator;
		public Plaintext negativeEight;
		double scale = Math.Pow(2.0, 40);

		public SleepComputation()
		{
			parms = new EncryptionParameters(SchemeType.CKKS);
			parms.PolyModulusDegree = polyModulusDegree;
			parms.CoeffModulus = CoeffModulus.Create(
				polyModulusDegree, new int[] { 60, 40, 40, 60 });
			//parms.PlainModulus = PlainModulus.Batching(polyModulusDegree, 20);
			context = new SEALContext(parms);
			evaluator = new Evaluator(context);
			encoder = new CKKSEncoder(context);
			//Set the scale of the encryption.
			
			SetConstants();
		}

		public Ciphertext GetProductivityDeficit(Ciphertext sleepHours)
		{
			//Take 8 from the value to see if a minimum of eight hours sleep has been got.
			Ciphertext CipherResult = new Ciphertext();
			evaluator.AddPlain(sleepHours,negativeEight,CipherResult);

			return CipherResult;
		}

		

		public void SetConstants()
		{
			//Find out if the correct number of hours have been slept
			negativeEight = new Plaintext();

			//Encode negative 8 using the CKKS scheme.
			encoder.Encode(-8,scale,negativeEight);
		}
	}
}
