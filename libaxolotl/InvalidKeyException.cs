using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl
{
    public class InvalidKeyException : Exception
    {

        public InvalidKeyException() { }

        public InvalidKeyException(String detailMessage)
            : base(detailMessage)
        {

        }

        public InvalidKeyException(Exception exception)
            : base(exception.Message)
        {

        }

        public InvalidKeyException(String detailMessage, Exception exception)
            : base(detailMessage, exception)
        {

        }
    }
}
