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
			Ciphertext DailyMinutes = new Ciphertext();
			Ciphertext result = new Ciphertext();
			//Multiply number of breaks by 5 minutes to get total duration in minutes.
			//also adds the Hour lunch break.
			//--------------------------------------------------------------------
			/*Debug.WriteLine("5min break scale before multiply.");
			Debug.WriteLine(Math.Log(FiveMinuteBreak.Scale, newBase: 2));
			Debug.WriteLine("Breaks before multiply.");
			Debug.WriteLine(Math.Log(Breaks.Scale, newBase: 2));

			Debug.WriteLine("total Breaks before after multiply.");
			Debug.WriteLine(Math.Log(totalBreakTime.Scale, newBase: 2));
			//TotalBreakTime is a result of amultiplication operation.
			*/
			//Breaks and FiveMinuteBreak both at a scale of 40 Here.
			//TotalBreak Time does not have a value so scale of 0.
			//----------------------------------------------------------------------
			evaluator.MultiplyPlain(Breaks, FiveMinuteBreak, totalBreakTime);


			Debug.WriteLine("---------------------------------------");
			Debug.WriteLine("total Breaks scale after multiply.");
			//totalBreakTime has a scale of 80 Here.
			Debug.WriteLine(Math.Log(totalBreakTime.Scale, newBase: 2));

			Debug.WriteLine("Breaks scale");
			Debug.WriteLine(Math.Log(Breaks.Scale, newBase: 2));
			Debug.WriteLine("Hour scale");
			Debug.WriteLine(Math.Log(Hour.Scale, newBase: 2));
			Debug.WriteLine("result scale");
			Debug.WriteLine(Math.Log(result.Scale, newBase: 2));


			//Code breaking here.
			//Breaks scale is 40 Here.,Hour -> 40
			Debug.WriteLine("-----------------------------------------------------------------");
			//Being stored in Breaks now.

			evaluator.AddPlainInplace(Breaks, Hour);
			//Breaks stores total time on break.
			DailyMinutes = GetDailyMinutes(HoursPerWeek);
			

			//Subtracts break and lunch time from daily Hours and  stores
			//In TotalsittingTime.
			Debug.WriteLine("---------------------------------------------------------------------");
			Debug.WriteLine("DailyMins scale before sub");
			Debug.WriteLine(DailyMinutes.Scale);

			Debug.WriteLine("totalBreakTime scale before sub");
			Debug.WriteLine(totalBreakTime.Scale);

			Debug.WriteLine("totalsittingtime scale before sub");
			Debug.WriteLine(totalSittingTime.Scale);
			DailyMinutes.Scale = Math.Pow(2.0, 40);
			totalBreakTime.Scale = Math.Pow(2.0, 40);

			Debug.WriteLine("--------------------------------------------------");
			Debug.WriteLine("DailyMinutes Level");
			Debug.WriteLine(context.GetContextData(DailyMinutes.ParmsId).ChainIndex);
			evaluator.ModSwitchToNextInplace(totalBreakTime);
			Debug.WriteLine("totalBreakTime Level");
			Debug.WriteLine(context.GetContextData(totalBreakTime.ParmsId).ChainIndex);
			Debug.WriteLine("DailyMins scale before sub");
			Debug.WriteLine(DailyMinutes.Scale);

			Debug.WriteLine("totalBreakTime scale before sub");
			Debug.WriteLine(totalBreakTime.Scale);
			evaluator.SubInplace(DailyMinutes, totalBreakTime);
			//evaluator.Sub()
			return totalBreakTime;

			/*ParmsId lastParmsId = x3Encrypted.ParmsId;
			evaluator.ModSwitchToInplace(x1Encrypted, lastParmsId);
			evaluator.ModSwitchToInplace(plainCoeff0, lastParmsId);*/
		}

		public Ciphertext GetDailyHours(Ciphertext HoursPerWeek)
		{
			//Takes in weekly Hours and Divides by 5.
			Ciphertext DailyHours = new Ciphertext();

			//Multiplication 1
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

			//Mulltiplication 2
			evaluator.MultiplyPlain(DailyHours, Hour, DailyMinutes);

			//Rescaling the Result of multipling HoursPerWeek.
			evaluator.RescaleToNextInplace(DailyMinutes);
			return DailyMinutes;
		}
	}
}
