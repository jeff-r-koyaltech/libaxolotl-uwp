#pragma once

#define CURVE25519_SHARED_KEY_LEN 32
#define CURVE25519_PRIV_KEY_LEN 32
#define CURVE25519_PUB_KEY_LEN 32
#define CURVE25519_SIG_LEN 64
#define CURVE25519_PRIV_KEY_LEN_ERR_MSG "CURVE25519_PRIV_KEY_LEN_ERR_MSG"
#define CURVE25519_PUB_KEY_LEN_ERR_MSG "CURVE25519_PUB_KEY_LEN_ERR_MSG"
#define CURVE25519_SIG_FAILED_MSG "CURVE25519_SIG_FAILED_MSG"

using namespace Platform;

namespace curve25519_windows
{
	//ENTRY POINT: This class is activatable in other C# projects. Access Curve25519 through this.
	public ref class Curve25519Native sealed
	{
	public:
		Curve25519Native();

		//This class implements the interface for Curve25519Provider from curve25519-java's code.
		//Note: In C# code, "Platform::Array<byte>^" should be treated/cast as signed bytes (sbyte) to match with Java's byte type.
		bool isNative();
		Array<uint8>^ calculateAgreement(const Array<uint8>^ ourPrivate, const Array<uint8>^ theirPublic);
		Array<uint8>^ generatePublicKey(const Array<uint8>^ privateKey);
		
		Array<uint8>^ generatePrivateKey();
		
		Array<uint8>^ generatePrivateKey(const Array<uint8>^ random);
		Array<uint8>^ calculateSignature(const Array<uint8>^ random, const Array<uint8>^ privateKey, const Array<uint8>^ message);
		bool verifySignature(const Array<uint8>^ publicKey, const Array<uint8>^ message, const Array<uint8>^ signature);

		//Having some problems passing in a SecureRandomProvider. For now, I'm just going to use the built-in, unaudited
		//Windows.Security.Cryptography.CryptographicBuffer class. Later, we'll solve this with reflection and dynamic
		//loading of a C# assembly which provides the random data through some open source means.
		//void setRandomProvider(SecureRandomProvider provider);
	private:
		
	};
}
