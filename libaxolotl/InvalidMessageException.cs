using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl
{
    public class InvalidMessageException : Exception
    {

        public InvalidMessageException() { }

        public InvalidMessageException(String detailMessage)
                        : base(detailMessage)
        {

        }

        public InvalidMessageException(Exception exception)
                        : base(exception.Message)
        {

        }

        public InvalidMessageException(String detailMessage, Exception exception)
                        : base(detailMessage, exception)
        {

        }

        public InvalidMessageException(String detailMessage, List<Exception> exceptions)
                        : base(string.Join(",", exceptions.Select(x => x.Message).ToArray()))
        {

        }
        public InvalidMessageException(String detailMessage, LinkedList<Exception> exceptions)
                        : base(string.Join(",", exceptions.Select(x => x.Message).ToArray()))
        {

        }
    }
}
