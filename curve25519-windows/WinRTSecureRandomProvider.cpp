#include "pch.h"
#include "WinRTSecureRandomProvider.h"

using namespace curve25519_windows;

WinRTSecureRandomProvider::WinRTSecureRandomProvider()
{
	//TODO: Implement Windows' crypto PRNG, or find a C/C++ one to import here (like SHA1PRNG or Fortuna or something)
	// The big question is...do we trust a PRNG with no published algorithm? :)
}


WinRTSecureRandomProvider::~WinRTSecureRandomProvider()
{
}

//TODO: How will I actually use this? If I expose the class to managed code, I can not have a
// reference parameter. I have to return a buffer to get data back. This signature will almost
// certainly be re-done later..
Windows::Storage::Streams::IBuffer^ WinRTSecureRandomProvider::nextBytes(Platform::Array<byte>^* output)
{
	Platform::Array<byte>^ output_internal = *output;
	Windows::Storage::Streams::IBuffer^ random_buffer =
		Windows::Security::Cryptography::CryptographicBuffer::GenerateRandom(output_internal->Length);

	return random_buffer;
}

int WinRTSecureRandomProvider::nextInt(int maxValue)
{
	return Windows::Security::Cryptography::CryptographicBuffer::GenerateRandomNumber() % maxValue;
}