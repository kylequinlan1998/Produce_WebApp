using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.ClientSideProcessing
{
	public class ClientDataPreProcessor
	{
		public ClientDataPreProcessor()
		{
			//Constructor.
		}

		public double HeightCalc(long height)
		{
			double result;
			result = (double)1 / (double)height;

			return result;
		}
	}
}
