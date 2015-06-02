#include "pch.h"
#include "Curve25519Native.h"
#include "Curve25519_Internal.h" //curve25519_donna
#include "ed25519\additions\curve_sigs.h" //curve25519_sign

using namespace Curve25519WinRT;
using namespace Platform;
using namespace Windows::Storage::Streams;
using namespace Windows::Security::Cryptography;

Curve25519Native::Curve25519Native()
{	
}

//May be used in the future to distinguish native and alternative
//Curve25519 implementations (as it's used in TextSecure).
bool Curve25519Native::isNative()
{
	return true;
}

Array<uint8>^ Curve25519Native::calculateAgreement(const Array<uint8>^ privateKey, const Array<uint8>^ publicKey)
{
	if (privateKey->Length != CURVE25519_PRIV_KEY_LEN)
	{
		throw ref new Exception(NTE_BAD_LEN, CURVE25519_PRIV_KEY_LEN_ERR_MSG);
	}

	if (publicKey->Length != CURVE25519_PUB_KEY_LEN)
	{
		throw ref new Exception(NTE_BAD_LEN, CURVE25519_PUB_KEY_LEN_ERR_MSG);
	}

	uint8* sharedKeyBytes = new uint8[CURVE25519_SHARED_KEY_LEN];
	ZeroMemory(sharedKeyBytes, (sizeof(uint8) * CURVE25519_SHARED_KEY_LEN));

	uint8* privateKeyBytes = privateKey->Data;
	uint8* publicKeyBytes = publicKey->Data;

	curve25519_donna(sharedKeyBytes, privateKeyBytes, publicKeyBytes);

	Array<uint8>^ sharedKey = ref new Array<uint8>(sharedKeyBytes, CURVE25519_SHARED_KEY_LEN);
	delete sharedKeyBytes;
	
	return sharedKey;
}

Array<uint8>^ Curve25519Native::generatePublicKey(const Array<uint8>^ privateKey)
{
	if (privateKey->Length != CURVE25519_PRIV_KEY_LEN)
	{
		throw ref new Exception(NTE_BAD_LEN, CURVE25519_PRIV_KEY_LEN_ERR_MSG);
	}

	static const uint8  basepoint[32] = { 9 };

	uint8* publicKeyBytes = new uint8[CURVE25519_PUB_KEY_LEN];
	//curve25519_donna() is assuming this is zeroed out, so let's make sure...
	ZeroMemory(publicKeyBytes, (sizeof(uint8) * CURVE25519_PUB_KEY_LEN));

	uint8* privateKeyBytes = privateKey->Data;

	curve25519_donna(publicKeyBytes, privateKeyBytes, basepoint);

	Array<uint8>^ publicKey = ref new Array<uint8>(publicKeyBytes, CURVE25519_PUB_KEY_LEN);
	delete publicKeyBytes;
	return publicKey;
}

Array<uint8>^ Curve25519Native::generatePrivateKey()
{
	IBuffer^ buffer = CryptographicBuffer::GenerateRandom(CURVE25519_PRIV_KEY_LEN);
	DataReader^ reader = DataReader::FromBuffer(buffer);

	Array<uint8>^ random = ref new Array<uint8>(CURVE25519_PRIV_KEY_LEN);
	reader->ReadBytes(random);
	
	return generatePrivateKey(random);
}

Array<uint8>^ Curve25519Native::generatePrivateKey(const Array<uint8>^ random)
{
	if (random->Length != CURVE25519_PRIV_KEY_LEN)
	{
		throw ref new Exception(NTE_BAD_LEN, CURVE25519_PRIV_KEY_LEN_ERR_MSG);
	}

	Array<uint8>^ privateKey = ref new Array<uint8>(CURVE25519_PRIV_KEY_LEN);
	for (int i = 0; i < CURVE25519_PRIV_KEY_LEN; i++)
	{
		privateKey[i] = random[i];
	}

	//These appear to be performance related adjustments for Curve25519
	//http://crypto.stackexchange.com/questions/11810/when-using-curve25519-why-does-the-private-key-always-have-a-fixed-bit-at-2254
	privateKey[0] &= 248;
	privateKey[31] &= 127;
	privateKey[31] |= 64;
	
	return privateKey;
}

Array<uint8>^ Curve25519Native::calculateSignature(const Array<uint8>^ random, const Array<uint8>^ privateKey, const Array<uint8>^ message)
{
	if (privateKey->Length != CURVE25519_PRIV_KEY_LEN)
	{
		throw ref new Exception(NTE_BAD_LEN, CURVE25519_PRIV_KEY_LEN_ERR_MSG);
	}

	uint8* signatureBytes = new uint8[CURVE25519_SIG_LEN];
	ZeroMemory(signatureBytes, (sizeof(uint8) * CURVE25519_SIG_LEN));

	uint8* privateKeyBytes = privateKey->Data;
	uint8* randomBytes = random->Data;
	uint8* messageBytes = message->Data;
	unsigned long messageLength = message->Length;

	int result = curve25519_sign(signatureBytes, privateKeyBytes, messageBytes, messageLength, randomBytes);
	
	Array<uint8>^ signature = ref new Array<uint8>(signatureBytes, CURVE25519_SIG_LEN);
	delete signatureBytes;

	if (result == 0)
	{
		return signature;
	}
	else
	{
		throw ref new Exception(NTE_BAD_SIGNATURE, CURVE25519_SIG_FAILED_MSG);
	}
	
}

bool Curve25519Native::verifySignature(const Array<uint8>^ publicKey, const Array<uint8>^ message, const Array<uint8>^ signature)
{
	uint8* publicKeyBytes = publicKey->Data;
	uint8* messageBytes = message->Data;
	unsigned long messageLength = message->Length;
	uint8* signatureBytes = signature->Data;

	int iVerify = curve25519_verify(signatureBytes, publicKeyBytes, messageBytes, messageLength);
	bool verified = (iVerify == 0);

	return verified;
}
