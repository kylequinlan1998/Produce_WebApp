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
		public IntegerEncoder encoder;
		public Evaluator evaluator;
		private Plaintext Encoded703;
		public BMIComputation()
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

		public Ciphertext GetHeightSquared(Ciphertext Height)
		{
			Ciphertext BMI = new Ciphertext();
			//Take in encrypteed Height and
			evaluator.Square(Height,BMI);

			return BMI;
		}
		private void SetConstants()
		{
			Encoded703 = encoder.Encode(703);
		}

		

		public Ciphertext MultiplyBy703(Ciphertext WeightHeightDivided)
		{
			//Takes in the weight/lbs and multiplies by 703 to get bmi.
			evaluator.MultiplyPlainInplace(WeightHeightDivided, Encoded703);

			return WeightHeightDivided;
		}
	}
}
