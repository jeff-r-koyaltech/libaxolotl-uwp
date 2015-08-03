using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl
{
    public class NoSessionException : Exception
    {
        public NoSessionException()
        {
        }

        public NoSessionException(String s)
            : base(s)
        {
        }

        public NoSessionException(Exception exception)
           : base(exception.Message)
        {
        }
    }
}
