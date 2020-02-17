using Microsoft.Research.SEAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.Encryption
{
	public static class GlobalPreferences
	{
		public static EncryptionParameters parms;
		public static SEALContext context;
		public static (PublicKey, SecretKey) keys;
		public static ulong polyModulusDegree = 8192;
		static GlobalPreferences()
		{
			parms.CoeffModulus = CoeffModulus.BFVDefault(polyModulusDegree);
			parms.PlainModulus = PlainModulus.Batching(polyModulusDegree, 20);
			context = new SEALContext(parms);
		}
		
	}
}
