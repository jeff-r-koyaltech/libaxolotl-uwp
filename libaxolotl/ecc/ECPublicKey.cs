using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.ecc
{
    public interface ECPublicKey : IComparable
    {

        //int KEY_SIZE = 33;

        byte[] serialize();

        int getType();
    }
}
