using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl
{
    public class InvalidKeyIdException : Exception
    {
        public InvalidKeyIdException(String detailMessage)
            :base(detailMessage)
        {
        }

        public InvalidKeyIdException(Exception exception)
            :base(exception.Message)
        {
        }
    }
}
