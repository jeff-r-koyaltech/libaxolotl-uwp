using libaxolotl.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.kdf
{
    public class DerivedRootSecrets
    {
        public static readonly int SIZE = 64;

        private readonly byte[] rootKey;
        private readonly byte[] chainKey;

        public DerivedRootSecrets(byte[] okm)
        {
            byte[][] keys = ByteUtil.split(okm, 32, 32);
            this.rootKey = keys[0];
            this.chainKey = keys[1];
        }

        public byte[] getRootKey()
        {
            return rootKey;
        }

        public byte[] getChainKey()
        {
            return chainKey;
        }
    }
}
