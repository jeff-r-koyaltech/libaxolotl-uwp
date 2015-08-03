namespace libaxolotl_csharp.ecc
{
	/// <summary>
	/// If you want to expose an implementation of Curve25519 to this class library,
	/// implement this interface.
	/// </summary>
	public interface ICurve25519Provider
	{
		byte[] calculateAgreement(byte[] ourPrivate, byte[] theirPublic);
		byte[] calculateSignature(byte[] random, byte[] privateKey, byte[] message);
		byte[] generatePrivateKey(byte[] random);
		byte[] generatePublicKey(byte[] privateKey);
		bool isNative();
		bool verifySignature(byte[] publicKey, byte[] message, byte[] signature);
		Curve25519KeyPair generateKeyPair();
	}
}