using libaxolotl.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.kdf
{
    public class DerivedMessageSecrets
    {

        public static readonly int SIZE = 80;
        private static readonly int CIPHER_KEY_LENGTH = 32;
        private static readonly int MAC_KEY_LENGTH = 32;
        private static readonly int IV_LENGTH = 16;

        private readonly byte[] cipherKey;
        private readonly byte[] macKey;
        private readonly byte[] iv;

        public DerivedMessageSecrets(byte[] okm)
        {
            //try
            //{
            byte[][] keys = ByteUtil.split(okm, CIPHER_KEY_LENGTH, MAC_KEY_LENGTH, IV_LENGTH);

            this.cipherKey = keys[0];
            this.macKey = keys[1];
            this.iv = keys[2];
            /*}
            catch (ParseException e)
            {
                throw new AssertionError(e);
            }*/
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
    }
}
