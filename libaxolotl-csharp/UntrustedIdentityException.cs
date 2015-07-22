using System;

namespace libaxolotl_csharp
{
	internal class UntrustedIdentityException : Exception
	{
		public UntrustedIdentityException()
		{
		}

		public UntrustedIdentityException(string message) : base(message)
		{
		}

		public UntrustedIdentityException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}