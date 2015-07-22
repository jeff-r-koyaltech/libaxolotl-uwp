using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace libaxolotl_csharp.ecc
{
	public class DjbECPublicKey : ECPublicKey
	{
		private byte[] publicKey;

		public DjbECPublicKey(byte[] publicKey)
		{
			this.publicKey = publicKey;
		}

		public override int CompareTo(ECPublicKey other)
		{
			byte[] myBytes = this.serialize();
			byte[] otherBytes = other.serialize();
			BigInteger myBI = new BigInteger(myBytes);
			BigInteger otherBI = new BigInteger(otherBytes);
			return myBI.CompareTo(otherBI);
		}

		public override byte[] PublicKey
		{
			get
			{
				return publicKey;
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
			List<byte> serialBytes = new List<byte>(1 + publicKey.Length);
			serialBytes.Add(Curve.DJB_TYPE);
			serialBytes.AddRange(publicKey);
			return serialBytes.ToArray();
		}

		public override bool Equals(object other)
		{
			if (other == null) return false;
			if (other.GetType() != typeof(DjbECPublicKey)) return false;

			DjbECPublicKey that = (DjbECPublicKey)other;
			return StructuralComparisons.StructuralEqualityComparer.Equals(this.publicKey, that.publicKey); //TODO: Does this actually work for byte [] ?
		}

		public override int GetHashCode()
		{
			return StructuralComparisons.StructuralEqualityComparer.GetHashCode(publicKey);
		}

		public override byte[] toByteArray()
		{
			return serialize();
		}
	}
}