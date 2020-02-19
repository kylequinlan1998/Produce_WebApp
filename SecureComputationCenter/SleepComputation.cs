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
		public IntegerEncoder encoder;
		public Evaluator evaluator;
		private Plaintext negativeEight;

		public SleepComputation()
		{
			parms = new EncryptionParameters(SchemeType.BFV);
			parms.PolyModulusDegree = polyModulusDegree;
			parms.CoeffModulus = CoeffModulus.BFVDefault(polyModulusDegree);
			parms.PlainModulus = PlainModulus.Batching(polyModulusDegree, 20);
			context = new SEALContext(parms);
			evaluator = new Evaluator(context);
			encoder = new IntegerEncoder(context);
			SetConstants();
		}

		public Ciphertext GetProductivityDeficit(Ciphertext sleepHours)
		{
			//Take 8 from the value to see if a minimum of eight hours sleep has been got.
			evaluator.AddPlainInplace(sleepHours,negativeEight);

			return sleepHours;
		}

		public void SetConstants()
		{
			//Find out if the correct number of hours have been slept
			negativeEight = encoder.Encode(-8);
		}
	}
}
