using System;

namespace libaxolotl_csharp
{
	internal class AssertionException : Exception
	{
		private InvalidKeyException e;

		public AssertionException()
		{
		}

		public AssertionException(string message) : base(message)
		{
		}

		public AssertionException(InvalidKeyException e)
		{
			this.e = e;
		}

		public AssertionException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}