#pragma once
namespace Curve25519WinRT {
	class WinRTSecureRandomProvider
	{
	public:
		WinRTSecureRandomProvider();
		~WinRTSecureRandomProvider();

		//SecureRandomProvider interface implementation (from Java's implementation)
		Windows::Storage::Streams::IBuffer^ nextBytes(Platform::Array<uint8>^* output);
		int nextInt(int maxValue);
	};
}
