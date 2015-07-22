namespace libaxolotl_csharp.logging
{
	public abstract class AxolotlLogger
	{
		public const int VERBOSE = 2;
		public const int DEBUG = 3;
		public const int INFO = 4;
		public const int WARN = 5;
		public const int ERROR = 6;
		public const int ASSERT = 7;

		public abstract void log(int priority, string tag, string message);
	}
}