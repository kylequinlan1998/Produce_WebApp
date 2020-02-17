using Microsoft.Research.SEAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produce_WebApp.Encryption
{
	public class BfvDecryptor
	{
		public EncryptionParameters parms;
		public SEALContext context;
		public ulong polyModulusDegree = 8192;
		public IntegerEncoder encoder;
		public Decryptor decryptor;
		public BfvDecryptor(SEALContext Context,SecretKey PrivateKey)
		{
			//Setting up the Encryption parameters for my Encryption,Creating SealContext
			//Setting the PolyModulusDegree and assigning a Coefficient Modulus.
			//Creates an instance of the SEAL context.
			//----------------------------------------------------------------
			parms = new EncryptionParameters(SchemeType.BFV);
			parms.PolyModulusDegree = polyModulusDegree;
			parms.CoeffModulus = CoeffModulus.BFVDefault(polyModulusDegree);
			parms.PlainModulus = PlainModulus.Batching(polyModulusDegree, 20);
			//Takes in The original seal context as an argument.
			context = Context;
			//-----------------------------------------------------------------
			//Construct a KeyGenerator using the SealContext and assign
			//The public and Private key values to the keys Variable.
			//Create an instance of Decryptor with the Generated Private Key.
			decryptor = new Decryptor(context, PrivateKey);
			encoder = new IntegerEncoder(context);
		}
	}
}
