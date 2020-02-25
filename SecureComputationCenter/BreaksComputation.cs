using Microsoft.Research.SEAL;
using System;
using System.Collections.Generic;
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
		public BreaksComputation()
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
		}

		public void SetConstants()
		{
			encoder.Encode(5.0,scale, FiveMinuteBreak);
			encoder.Encode(60.0, scale, Hour);
			encoder.Encode(0.20, scale, DivideByFive);
		}

		public Ciphertext GetTotalSittingTime(Ciphertext HoursPerWeek,Ciphertext Breaks)
		{
			Ciphertext totalBreakTime = new Ciphertext();
			Ciphertext totalSittingTime = new Ciphertext();
			Ciphertext DailyMinutes = new Ciphertext();
			//Multiply number of breaks by 5 minutes to get total duration in minutes.
			//also adds the Hour lunch break.
			evaluator.MultiplyPlain(Breaks, FiveMinuteBreak, totalBreakTime);
			evaluator.AddPlainInplace(totalBreakTime, Hour);
			DailyMinutes = GetDailyHours(HoursPerWeek);

			//Subtracts break and lunch time from daily Hours and  stores
			//In TotalsittingTime.
			evaluator.Sub(DailyMinutes, totalBreakTime, totalSittingTime);
			

			return totalSittingTime;
		}

		public Ciphertext GetDailyHours(Ciphertext HoursPerWeek)
		{
			//Takes in weekly Hours and Divides by 5.
			Ciphertext DailyHours = new Ciphertext();

			evaluator.MultiplyPlain(HoursPerWeek, DivideByFive, DailyHours);

			return DailyHours;
		}

		public Ciphertext GetDailyMinutes(Ciphertext HoursPerWeek)
		{
			//Takes in Hours per week and converts it to minutes per day.
			Ciphertext DailyHours = new Ciphertext();
			DailyHours = GetDailyHours(HoursPerWeek);

			//Takes in daily Hours and converts to Minutes.
			Ciphertext DailyMinutes = new Ciphertext();
			evaluator.MultiplyPlain(DailyHours, Hour, DailyMinutes);

			return DailyMinutes;
		}
	}
}
