using Microsoft.Research.SEAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.SecureComputationCenter
{
	public class BMIComputation
	{
		private EncryptionParameters parms;
		private ulong polyModulusDegree = 8192;
		private SEALContext context;
		public CKKSEncoder encoder;
		public Evaluator evaluator;
		public Plaintext Encoded703;
		double scale;
		RelinKeys KeysRelin;
		public BMIComputation(RelinKeys Keys)
		{
			parms = new EncryptionParameters(SchemeType.CKKS);
			parms.PolyModulusDegree = polyModulusDegree;
			parms.CoeffModulus = CoeffModulus.Create(
				polyModulusDegree, new int[] { 60, 40, 40, 60 });
			//parms.PlainModulus = PlainModulus.Batching(polyModulusDegree, 20);
			context = new SEALContext(parms);
			evaluator = new Evaluator(context);
			encoder = new CKKSEncoder(context);
			scale = Math.Pow(2.0, 40);
			SetConstants();
			KeysRelin = Keys;
		}

		public Ciphertext ComputeBMI(Ciphertext HeightOverOneSquared,Ciphertext Weight)
		{
			Ciphertext weightHeightMultiplied = new Ciphertext();
			//Scale of 80 i think.
			weightHeightMultiplied = MultiplyWeightByHeightSquared(Weight, HeightOverOneSquared);
			Ciphertext finalResult = new Ciphertext();

			finalResult = MultiplyBy703(weightHeightMultiplied);

			return finalResult;
			
		}
		private void SetConstants()
		{
			//Encode 703 into variable.
			Encoded703 = new Plaintext();
			encoder.Encode(703, scale, Encoded703);
		}

		public Ciphertext MultiplyWeightByHeightSquared(Ciphertext weight,Ciphertext heightsquared)
		{
			Ciphertext WeightByHeightSquared = new Ciphertext();
			//Will be a scale of 80 output.
			evaluator.Multiply(weight, heightsquared, WeightByHeightSquared);
			//Need to find a solution for this.
			
			return WeightByHeightSquared;
		}
		public Ciphertext MultiplyBy703(Ciphertext WeightHeightDivided)
		{
			Ciphertext result = new Ciphertext();
			//Takes in the weight/lbs and multiplies by 703 to get bmi.
			evaluator.MultiplyPlain(WeightHeightDivided, Encoded703,result);

			return result;
		}
	}
}
