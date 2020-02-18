using Microsoft.Research.SEAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.SecureComputationCenter
{
	public class HydrationComputation
	{
		public Evaluator evaluator;
		//SET IT TO 6
		public int WaterRequired;
		public IntegerEncoder encoder;
		public EncryptionParameters parms;
		public ulong polyModulusDegree = 8192;
		public SEALContext context;
		Plaintext negativeSix;
		public HydrationComputation()
		{
			//Create evaluator and encoder using a new context.
			parms = new EncryptionParameters(SchemeType.BFV);
			parms.PolyModulusDegree = polyModulusDegree;
			parms.CoeffModulus = CoeffModulus.BFVDefault(polyModulusDegree);
			parms.PlainModulus = PlainModulus.Batching(polyModulusDegree, 20);
			context = new SEALContext(parms);
			evaluator = new Evaluator(context);
			encoder = new IntegerEncoder(context);
			SetConstants();
		}


		public Ciphertext DehydrationComputation(Ciphertext WaterEncrypted)
		{
			//Must be greater or equal to 6 to check correct amount of water drank.
			evaluator.AddPlainInplace(WaterEncrypted, negativeSix);

			return WaterEncrypted;
		}

		private void SetConstants()
		{
			negativeSix = encoder.Encode(-6);
		}


	}
}
