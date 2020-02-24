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

		public long HeightCalc(long height)
		{
			long result;
			result = (long)(1 / height);

			return result;
		}
	}
}
