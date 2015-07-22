namespace libaxolotl_csharp.protocol
{
	public abstract class CiphertextMessage
	{
		public static readonly int UNSUPPORTED_VERSION = 1;
		public static readonly int CURRENT_VERSION = 3;

		public static readonly int WHISPER_TYPE = 2;
		public static readonly int PREKEY_TYPE = 3;
		public static readonly int SENDERKEY_TYPE = 4;
		public static readonly int SENDERKEY_DISTRIBUTION_TYPE = 5;

		// This should be the worst case (worse than V2).  So not always accurate, but good enough for padding.
		public static readonly int ENCRYPTED_MESSAGE_OVERHEAD = 53;

		public abstract byte[] serialize();
		public abstract int getType();
	}
}