namespace libaxolotl_csharp.logging
{
	public static class AxolotlLoggerProvider
	{
		private static AxolotlLogger provider;
		public static AxolotlLogger Provider
		{
			get
			{
				return provider;
			}
			set
			{
				provider = value;
			}
		}
	}
}