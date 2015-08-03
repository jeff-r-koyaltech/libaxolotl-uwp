using libaxolotl.kdf;
using libaxolotl.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.groups.ratchet
{
    /**
     * The final symmetric material (IV and Cipher Key) used for encrypting
     * individual SenderKey messages.
     *
     * @author 
     */
    public class SenderMessageKey
    {

        private readonly uint iteration;
        private readonly byte[] iv;
        private readonly byte[] cipherKey;
        private readonly byte[] seed;

        public SenderMessageKey(uint iteration, byte[] seed)
        {
            byte[] derivative = new HKDFv3().deriveSecrets(seed, Encoding.UTF8.GetBytes("WhisperGroup"), 48);
            byte[][] parts = ByteUtil.split(derivative, 16, 32);

            this.iteration = iteration;
            this.seed = seed;
            this.iv = parts[0];
            this.cipherKey = parts[1];
        }

        public uint getIteration()
        {
            return iteration;
        }

        public byte[] getIv()
        {
            return iv;
        }

        public byte[] getCipherKey()
        {
            return cipherKey;
        }

        public byte[] getSeed()
        {
            return seed;
        }
    }
}
