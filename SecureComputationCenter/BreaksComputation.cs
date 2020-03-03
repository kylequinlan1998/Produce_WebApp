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

			Hour = new Plaintext();
			encoder.Encode(60, scale, Hour);

			one = new Plaintext();
			encoder.Encode(1, scale, one);
		}

		public Ciphertext GetBreaks(Ciphertext Breaks)
		{
			//Create a Ciphertext to store the output.
			Ciphertext totalBreakTime = new Ciphertext();
			//This is working.
			evaluator.MultiplyPlain(Breaks, FiveMinuteBreak,totalBreakTime);

			return totalBreakTime;
		}

		/*public void SetConstants()
		{
			FiveMinuteBreak = new Plaintext();
			Hour = new Plaintext();
			DivideByFive = new Plaintext();
			encoder.Encode(5.0,scale, FiveMinuteBreak);
			encoder.Encode(60.0, scale, Hour);
			encoder.Encode(0.20, scale, DivideByFive);
		}

		public Ciphertext GetTotalSittingTime(Ciphertext HoursPerWeek,Ciphertext Breaks)
		{
			Ciphertext totalBreakTime = new Ciphertext();
			Ciphertext totalSittingTime = new Ciphertext();
			Ciphertext DailyHours = new Ciphertext();
			Ciphertext result = new Ciphertext();
			
			//----------------------------------------------------------------------
			//Scale of 80.
			evaluator.MultiplyPlain(Breaks, FiveMinuteBreak, totalBreakTime);



			//Being stored in Breaks now.
			evaluator.AddPlainInplace(Breaks, Hour);
			//Breaks stores total time on break.

			//scale of 80.
			DailyHours = GetDailyHours(HoursPerWeek);
			

			DailyHours.Scale = Math.Pow(2.0, 40);
			totalBreakTime.Scale = Math.Pow(2.0, 40);

			evaluator.SubInplace(DailyHours, totalBreakTime);

			return DailyHours; ;

			ParmsId lastParmsId = x3Encrypted.ParmsId;
			evaluator.ModSwitchToInplace(x1Encrypted, lastParmsId);
			evaluator.ModSwitchToInplace(plainCoeff0, lastParmsId);
		}

		public Ciphertext GetDailyHours(Ciphertext HoursPerWeek)
		{
			//Takes in weekly Hours and Divides by 5.
			Ciphertext DailyHours = new Ciphertext();

			//Returns DailyHours with a scale of ^80.
			evaluator.MultiplyPlain(HoursPerWeek, DivideByFive, DailyHours);

			
			return DailyHours;
		}

		/*public Ciphertext GetDailyMinutes(Ciphertext HoursPerWeek)
		{
			//Takes in Hours per week and converts it to minutes per day.
			Ciphertext DailyHours = new Ciphertext();
			DailyHours = GetDailyHours(HoursPerWeek);

			//Takes in daily Hours and converts to Minutes.
			Ciphertext DailyMinutes = new Ciphertext();

			//Mulltiplication 2
			evaluator.MultiplyPlain(DailyHours, Hour, DailyMinutes);

			//Rescaling the Result of multipling HoursPerWeek.
			evaluator.RescaleToNextInplace(DailyMinutes);
			return DailyMinutes;
		}*/
	}
}
