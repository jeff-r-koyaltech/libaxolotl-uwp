using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.ratchet
{
    public class MessageKeys
    {

        private readonly byte[] cipherKey;
        private readonly byte[] macKey;
        private readonly byte[] iv;
        private readonly uint counter;

        public MessageKeys(byte[] cipherKey, byte[] macKey, byte[] iv, uint counter)
        {
            this.cipherKey = cipherKey;
            this.macKey = macKey;
            this.iv = iv;
            this.counter = counter;
        }

        public byte[] getCipherKey()
        {
            return cipherKey;
        }

        public byte[] getMacKey()
        {
            return macKey;
        }

        public byte[] getIv()
        {
            return iv;
        }

        public uint getCounter()
        {
            return counter;
        }
    }
}
