using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;

namespace Curve25519WinRT.WindowsPhone_Tests
{
	[TestClass]
	public class BasicTests
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

		/// <summary>
		/// Ensure isNative behaves properly.
		/// </summary>
		[TestMethod]
		public void TestIsNative()
		{
			Assert.IsNotNull(curve25519);
			Assert.IsTrue(curve25519.isNative());
		}

		/// <summary>
		/// Test that we can generate a private key from an array of random bytes.
		/// </summary>
		[TestMethod]
		public void TestGenPrivKeyFromRandom()
		{
			byte[] randomBuffer = GetRandomBuffer(EXPECTED_LEN);
			byte[] privKeyBytes = curve25519.generatePrivateKey(randomBuffer);
			Assert.IsNotNull(privKeyBytes);
			Assert.AreEqual(EXPECTED_LEN, privKeyBytes.Length,
				"This implementation should produce 32 byte private keys.");

			bool allZero = true;
			//force fail to test this logic
			//privKeyBytes = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
			foreach (byte b in privKeyBytes)
			{
				if (!allZero)
					break; //early

				if (b.CompareTo(0) != 0)
				{
					allZero = false;
				}
			}
			Assert.IsFalse(allZero, "A private key shouldn't be all zeroes.");
		}
		
		/// <summary>
		/// Test that we can make public keys from arbitrary private keys.
		/// </summary>
		[TestMethod]
		public void TestGenPublicKeyFromPrivateKey()
		{
			byte[] randomBuffer = GetRandomBuffer(EXPECTED_LEN);
			byte[] privKeyBytes = curve25519.generatePrivateKey(randomBuffer);

			byte[] publicKeyBytes = curve25519.generatePublicKey(privKeyBytes);
			Assert.IsNotNull(publicKeyBytes);
			Assert.AreEqual<int>(EXPECTED_LEN, publicKeyBytes.Length);

			bool allZero = true;
			//force fail to test this logic
			//publicKeyBytes = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
			foreach (byte b in privKeyBytes)
			{
				if (!allZero)
					break; //early

				if (b.CompareTo(0) != 0)
				{
					allZero = false;
				}
			}
			Assert.IsFalse(allZero, "A public key shouldn't be all zeroes.");
		}

		/// <summary>
		/// I use this from time to time in Debug mode to capture byte arrays for fixed-value unit testing.
		/// </summary>
		private string CaptureBytesAsString(byte[] key)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("byte [] b = new byte [] { ");
			foreach (byte b in key)
			{
				uint ui = (uint)b;
				sb.AppendFormat("{0}, ", ui);
			}
			sb.Append("};");
			return sb.ToString();
		}

		/// <summary>
		/// Test for proper failures with too short or too long keys.
		/// </summary>
		[TestMethod]
		public void TestGenPublicKeyFromPrivateKey_FailureCases()
		{
			bool? excThrown = null;

			byte[] randomBuffer = GetRandomBuffer(EXPECTED_LEN);
			byte[] privKeyBytes = curve25519.generatePrivateKey(randomBuffer);

			byte[] tooShortPrivKeyBytes = new byte[]
			{
				privKeyBytes[0], privKeyBytes[1], privKeyBytes[2],
				privKeyBytes[3], privKeyBytes[4], privKeyBytes[5]
			};

			byte[] tooLongPrivKeyBytes = new byte[tooShortPrivKeyBytes.Length + privKeyBytes.Length];
			tooShortPrivKeyBytes.CopyTo(tooLongPrivKeyBytes, 0);
			privKeyBytes.CopyTo(tooLongPrivKeyBytes, tooShortPrivKeyBytes.Length);

			#region Test too short
			excThrown = null;
			try
			{
				byte[] publicKeyBytes = curve25519.generatePublicKey(tooShortPrivKeyBytes);
				excThrown = false;
			}
			catch (Exception)
			{
				excThrown = true;
			}
			if (excThrown != true)
			{
				Assert.Fail("We should not allow keys that are too short.");
			}
			#endregion

			#region Test too long
			excThrown = null;
			try
			{
				byte[] publicKeyBytes = curve25519.generatePublicKey(tooLongPrivKeyBytes);
				excThrown = false;
			}
			catch (Exception)
			{
				excThrown = true;
			}
			if (excThrown != true)
			{
				Assert.Fail("We should not allow keys that are too long.");
			}
			#endregion
		}

		/// <summary>
		/// Ensure that, given a predefined private key, we get the public key expected. This
		/// test can be used to ensure cross-platform compatibility with other implementations,
		/// such as curve25519-java.
		/// </summary>
		[TestMethod]
		public void TestFixedPrivKeyToPublicKey()
		{
			byte[] privateKeyBytes = new byte[] {
				40, 146, 87, 95, 87, 167, 114, 250, 89, 24, 160, 144, 158, 233, 161, 185,
				9, 153, 71, 88, 153, 107, 3, 49, 159, 174, 55, 184, 136, 80, 214, 123
			};

			byte[] expectedPublicKey = new byte[] {
				5, 4, 110, 87, 229, 103, 40, 213, 31, 232, 220, 105, 168, 107, 115, 255,
				147, 215, 171, 130, 192, 180, 71, 12, 6, 20, 212, 30, 157, 31, 175, 20
			};

			byte[] publicKeyBytes = curve25519.generatePublicKey(privateKeyBytes);
			Assert.IsNotNull(publicKeyBytes);
			Assert.AreEqual<int>(publicKeyBytes.Length, expectedPublicKey.Length);

			for (int i = 0; i < expectedPublicKey.Length; i++)
			{
				if (publicKeyBytes[i] != expectedPublicKey[i])
				{
					Assert.Fail("Expected public key and actual public key do not match.");
				}
			}
		}
	}
}
