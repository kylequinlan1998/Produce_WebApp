using Microsoft.Research.SEAL;
using System;
using System.Collections.Generic;
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
		public BMIComputation()
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
		}

		public Ciphertext ComputeBMI(Ciphertext Height,Ciphertext Weight)
		{
			Ciphertext HeightSquared = GetHeightSquared(Height);
			Ciphertext result = new Ciphertext();

			Ciphertext WeightHeightDivided = MultiplyWeightByHeightSquared(Weight, HeightSquared);

			//result = MultiplyBy703(WeightHeightDivided);

			return WeightHeightDivided;
		}
		private void SetConstants()
		{
			//Encode 703 into variable.
			Encoded703 = new Plaintext();
			encoder.Encode(703, scale, Encoded703);
		}
		public Ciphertext GetHeightSquared(Ciphertext Height)
		{
			Ciphertext BMI = new Ciphertext();
			//Take in encrypteed Height and square,output to BMI variable.
			evaluator.Square(Height,BMI);

			return BMI;
		}

		public Ciphertext MultiplyWeightByHeightSquared(Ciphertext weight,Ciphertext heightsquared)
		{
			Ciphertext WeightByHeightSquared = new Ciphertext();
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
