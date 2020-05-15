using Microsoft.Research.SEAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.SecureComputationCenter
{
	public class SalaryComputation
	{
		public Evaluator evaluator;
		public CKKSEncoder encoder;
		public EncryptionParameters parms;
		public ulong polyModulusDegree = 8192;
		public SEALContext context;
		public Plaintext DivideByFiftyTwo;
		double scale = Math.Pow(2.0, 40);
		public SalaryComputation()
		{
			//Constructor.
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

		public void SetConstants()
		{
			// Encode the constant 1/52  and store in DivideByFiftyTwo.
			DivideByFiftyTwo = new Plaintext();

			encoder.Encode(0.01923076, scale, DivideByFiftyTwo);
		}

		public Ciphertext GetWeeklySalary(Ciphertext salary)
		{
			// Takes input as salary Ciphertext 
			Ciphertext salaryResult = new Ciphertext();
			// Multiply Plaintext constant by Ciphertext salary and store in salaryResult.
			Stopwatch sw = new Stopwatch();
			Stopwatch sw2 = new Stopwatch();
			sw2.Start();
			var num = 100000000 * 0.2;
			sw2.Stop();

			var sw2time = sw2.Elapsed;
			sw.Start();

			

			
			evaluator.MultiplyPlain(salary, DivideByFiftyTwo, salaryResult);
			sw.Stop();
			var time = sw.Elapsed;
			return salaryResult;
		}
	}
}
