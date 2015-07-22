using System;

namespace libaxolotl_csharp.ecc
{
	public class DjbECPrivateKey : ECPrivateKey
	{
		private byte[] privateKey;
		public DjbECPrivateKey(byte[] privKey)
		{
			privateKey = privKey;
		}

		public override byte[] PrivateKey
		{
			get
			{
				return privateKey;
			}
		}

		public override int Type
		{
			get
			{
				return Curve.DJB_TYPE;
			}
		}

		public override byte[] serialize()
		{
			return privateKey;
		}
	}
}