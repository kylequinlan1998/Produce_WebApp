using Microsoft.Research.SEAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.SecureComputationCenter
{
	public class BreaksComputation
	{
		public Evaluator evaluator;
		public CKKSEncoder encoder;
		public EncryptionParameters parms;
		public ulong polyModulusDegree = 8192;
		public SEALContext context;
		double scale = Math.Pow(2.0, 40);
		public Plaintext FiveMinuteBreak;
		public Plaintext Hour;
		public Plaintext DivideByFive;
		public RelinKeys keys;
		public Plaintext one;
		public BreaksComputation(RelinKeys Keys)
		{
			parms = new EncryptionParameters(SchemeType.CKKS);
			parms.PolyModulusDegree = polyModulusDegree;
			parms.CoeffModulus = CoeffModulus.Create(
				polyModulusDegree, new int[] { 60, 40, 40, 60 });
			//parms.PlainModulus = PlainModulus.Batching(polyModulusDegree, 20);
			context = new SEALContext(parms);
			evaluator = new Evaluator(context);
			encoder = new CKKSEncoder(context);
			SetConstants();
			keys = Keys;
		}

		public void SetConstants()
		{
			FiveMinuteBreak = new Plaintext();
			encoder.Encode(5.0,scale,FiveMinuteBreak);

			DivideByFive = new Plaintext();
			encoder.Encode(0.20, scale, DivideByFive);
		}

		public Ciphertext GetBreaks(Ciphertext Breaks)
		{
			//Create a Ciphertext to store the output.
			Ciphertext totalBreakTime = new Ciphertext();
			//This is working.
			evaluator.MultiplyPlain(Breaks, FiveMinuteBreak,totalBreakTime);

			return totalBreakTime;
		}

		public Ciphertext GetDailyHours( Ciphertext HoursPerWeek)
		{
			Ciphertext result = new Ciphertext();
			//Divides Hours per week by 5.
			evaluator.MultiplyPlain(HoursPerWeek, DivideByFive,result);

			return result;
		}
	}
}
