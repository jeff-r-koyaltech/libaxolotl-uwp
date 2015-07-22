using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using libaxolotl_csharp.ecc;
using System.Collections;

namespace libaxolotl_test
{
	[TestClass]
	public class BasicTests
	{
		/// <summary>
		/// It's important that we test byte [] for equality by value.
		/// </summary>
		[TestMethod]
		public void TestECPublicKeyEquals()
		{
			curve25519_windows.Curve25519Native native = new curve25519_windows.Curve25519Native();
			byte[] privKey = native.generatePrivateKey();
			byte[] pubKey = native.generatePublicKey(privKey);
			DjbECPublicKey key1 = new DjbECPublicKey(pubKey);

			byte[] pubKey2 = native.generatePublicKey(privKey);

			Assert.IsTrue(StructuralComparisons.StructuralEqualityComparer.Equals(
					pubKey, pubKey2));

			DjbECPublicKey key2 = new DjbECPublicKey(pubKey2);

			Assert.IsTrue(key1.Equals(key2));

			int hash1 = key1.GetHashCode();
			int hash2 = key2.GetHashCode();

			Assert.AreEqual<int>(hash1, hash2);
		}
	}
}
