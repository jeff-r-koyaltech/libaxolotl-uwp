using System;

namespace libaxolotl_csharp.ecc
{
	public enum CurveImplementation
	{
		/// <summary>
		/// Choose the best implementation available (priority given to a native implementation for speed)
		/// </summary>
		BEST,
		/// <summary>
		/// Choose a native implementation (C++ / WinRT component)
		/// </summary>
		NATIVE,
		/// <summary>
		/// Choose a pure C# implementation (TODO not implemented yet)
		/// </summary>
		CSHARP
	}

	public static class Curve25519Factory
	{
		private static CurveImplementation defaultType = CurveImplementation.BEST;
		public static CurveImplementation DefaultType
		{
			get
			{
				return defaultType;
			}
			set
			{
				defaultType = value;
				_instance = null;
			}
		}

		private static ICurve25519Provider _instance;
		public static ICurve25519Provider Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = getInstance(DefaultType);
				}

				return _instance;
			}
		}

		public static ICurve25519Provider getInstance(CurveImplementation type)
		{
			if((type == CurveImplementation.BEST) || (type == CurveImplementation.NATIVE))
			{
				return new Curve25519NativeProvider();
			}
			else
			{
				throw new NotImplementedException(Resources.LibAxolotl.GetString("NoCSProvider"));
			}
		}
	}
}