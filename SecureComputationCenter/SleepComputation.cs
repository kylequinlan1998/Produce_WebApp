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
		private EncryptionParameters parms;
		private ulong polyModulusDegree = 8192;
		private SEALContext context;
		public CKKSEncoder encoder;
		public Evaluator evaluator;
		private Plaintext negativeEight;
		double scale;

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
			scale = Math.Pow(2, 40);
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
			List<double> resultList = new List<double>();
			//Encode negative 8 using the CKKS scheme.
			encoder.Encode(-8,scale,negativeEight);
		}
	}
}
