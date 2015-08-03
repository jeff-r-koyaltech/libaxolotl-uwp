using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl
{
    public class LegacyMessageException : Exception
    {
        public LegacyMessageException()
        {
        }

        public LegacyMessageException(String s)
            : base(s)
        {
        }
    }
}
