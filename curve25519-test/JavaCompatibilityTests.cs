using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Storage.Streams;
using Windows.Security.Cryptography;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.Generic;

namespace Curve25519WinRT.WindowsPhone_Tests
{
	/// <summary>
	/// These tests have been ported over from curve25519-java to use the same fixed
	/// keys for unit testing. This should help us make sure our libraries can work
	/// with each other. Function names will match those in the Java test code.
	/// </summary>
	[TestClass]
	public class JavaCompatibilityTests
	{
		#region Test helper code
		private curve25519_windows.Curve25519Native curve25519;
		private const int EXPECTED_LEN = 32;
		private static byte[] GetRandomBuffer(int expectedLen)
		{
			IBuffer randomIBuffer = CryptographicBuffer.GenerateRandom((uint)expectedLen);
			return WindowsRuntimeBufferExtensions.ToArray(randomIBuffer, 0, expectedLen);
		}
		#endregion

		[TestInitialize]
		public void Initialize()
		{
			curve25519 = new curve25519_windows.Curve25519Native();
		}

		[TestCleanup]
		public void Cleanup()
		{
			curve25519 = null;
		}

		[TestMethod]
		public void testKeyGen()
		{
			byte[] keyIn = new byte[32];
			byte[] keyOut = null;

			keyIn[0] = 123;

			for (int count = 0; count < 1000; count++)
			{
				keyOut = curve25519.generatePublicKey(keyIn);
				Array.Copy(keyOut, 0, keyIn, 0, EXPECTED_LEN);
			}

			byte[] result2 = new byte[]{
				(byte)0xa2, (byte)0x3c, (byte)0x84, (byte)0x09, (byte)0xf2,
				(byte)0x93, (byte)0xb4, (byte)0x42, (byte)0x6a, (byte)0xf5,
				(byte)0xe5, (byte)0xe7, (byte)0xca, (byte)0xee, (byte)0x22,
				(byte)0xa0, (byte)0x01, (byte)0xc7, (byte)0x9a, (byte)0xca,
				(byte)0x1a, (byte)0xf2, (byte)0xea, (byte)0xcb, (byte)0x4d,
				(byte)0xdd, (byte)0xfa, (byte)0x05, (byte)0xf8, (byte)0xbc,
				(byte)0x7f, (byte)0x37};



			List<byte> keyOutList = new List<byte>(keyOut);
			List<byte> result2List = new List<byte>(result2);
			CollectionAssert.AreEqual(result2, keyOut);
		}

		[TestMethod]
		public void testEcDh()
		{
			byte[] p = new byte[32];
			byte[] q = null;
			byte[] n = new byte[32];

			p[0] = 100;
			n[0] = 100;

			n = curve25519.generatePrivateKey(n);

			for (int count = 0; count < 1000; count++)
			{
				q = curve25519.calculateAgreement(n, p);
				Array.Copy(q, 0, p, 0, 32);
				q = curve25519.calculateAgreement(n, p);
				Array.Copy(q, 0, n, 0, 32);
				n = curve25519.generatePrivateKey(n);
			}

			byte[] result = new byte[]{
				(byte)0xce, (byte)0xb4, (byte)0x4e, (byte)0xd6, (byte)0x4a,
				(byte)0xd4, (byte)0xc2, (byte)0xb5, (byte)0x43, (byte)0x9d,
				(byte)0x25, (byte)0xde, (byte)0xb1, (byte)0x10, (byte)0xa8,
				(byte)0xd7, (byte)0x2e, (byte)0xb3, (byte)0xe3, (byte)0x8e,
				(byte)0xf4, (byte)0x8a, (byte)0x42, (byte)0x73, (byte)0xb1,
				(byte)0x1b, (byte)0x4b, (byte)0x13, (byte)0x8d, (byte)0x17,
				(byte)0xf9, (byte)0x34};

			List<byte> qList = new List<byte>(q);
			List<byte> resultList = new List<byte>(result);
			CollectionAssert.AreEqual(resultList, qList);
		}

		// FIXME: There's no actual vector here.  If verifySignature is broken and always returns true,
		// this test will pass.
		[TestMethod]
		public void testSignVerify()
		{
			byte[] msg = new byte[100];
			byte[] sig_out = new byte[64];
			byte[] privkey = new byte[32];
			byte[] pubkey = new byte[32];
			byte[] random = new byte[64];

			privkey[0] = 123;

			for (int count = 0; count < 1000; count++)
			{
				privkey = curve25519.generatePrivateKey(privkey);
				pubkey = curve25519.generatePublicKey(privkey);
				sig_out = curve25519.calculateSignature(random, privkey, msg);

				Assert.IsTrue(curve25519.verifySignature(pubkey, msg, sig_out));

				Array.Copy(sig_out, 0, privkey, 0, 32);
			}
		}

		//TODO: Implement testSignVerify_Fail(), and make sure it throws an exception.
	}
}