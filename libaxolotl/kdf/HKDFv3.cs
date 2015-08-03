using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.kdf
{
    public class HKDFv3 : HKDF
    {
        protected override int getIterationStartOffset()
        {
            return 1;
        }
    }
}
