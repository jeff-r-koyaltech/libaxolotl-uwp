using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl_classlib
{
    public class SimpleTest
    {
		public static void Test()
		{
			System.Diagnostics.Debug.WriteLine("hello");

			curve25519_windows.Curve25519Native native = new curve25519_windows.Curve25519Native();
			
		}
    }
}
