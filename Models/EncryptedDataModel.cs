using Microsoft.Research.SEAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.Models
{
	public class EncryptedDataModel
	{
		public Ciphertext Age { get; set; }
		public Ciphertext Weight { get; set; }
		public Ciphertext Height { get; set; }
		public Ciphertext Water { get; set; }
		public Ciphertext Sleep { get; set; }
		public Ciphertext Salary { get; set; }
		public Ciphertext HoursWeek { get; set; }
		public Ciphertext Breaks { get; set; }
		public Ciphertext SleepProductivityLoss { get; set; }
		public Ciphertext WaterProductivityLoss { get; set; }
		public Ciphertext BreaksProductivityLoss { get; set; }
		public Ciphertext BMI { get; set; }
	}
}
