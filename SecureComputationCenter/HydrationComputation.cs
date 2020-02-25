using Microsoft.Research.SEAL;
using System;

namespace Produce_WebApp.SecureComputationCenter
{
	public class HydrationComputation
	{
		public Evaluator evaluator;
		//SET IT TO 6
		public int WaterRequired;
		public CKKSEncoder encoder;
		public EncryptionParameters parms;
		public ulong polyModulusDegree = 8192;
		public SEALContext context;
		public Plaintext negativeSix;
		double scale = Math.Pow(2.0, 40);
		public HydrationComputation()
		{
			//Create evaluator and encoder using a new context.
			parms = new EncryptionParameters(SchemeType.CKKS);
			parms.PolyModulusDegree = polyModulusDegree;
			parms.CoeffModulus = CoeffModulus.Create(
				polyModulusDegree, new int[] { 60, 40, 40, 60 });
			//parms.PlainModulus = PlainModulus.Batching(polyModulusDegree, 20);
			context = new SEALContext(parms);
			evaluator = new Evaluator(context);
			encoder = new CKKSEncoder(context);
			SetConstants();
		}


		public Ciphertext DehydrationComputation(Ciphertext WaterEncrypted)
		{
			//Takaes in water drank ciphertext and adds negative six.
			//Must be greater or equal to 6 to check correct amount of water drank.
			Ciphertext HydrationCipherResult = new Ciphertext();

			evaluator.AddPlain(WaterEncrypted, negativeSix, HydrationCipherResult);

			return HydrationCipherResult;
		}

		private void SetConstants()
		{
			//Encodes negative six and stores in variable negativeSix.
			negativeSix = new Plaintext();

			encoder.Encode(-6, scale, negativeSix);
		}


	}
}
