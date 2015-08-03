using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl
{
    public class DuplicateMessageException : Exception
    {
        public DuplicateMessageException(String s)
            : base(s)
        {
        }
    }
}
