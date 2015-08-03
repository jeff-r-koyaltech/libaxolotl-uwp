using libaxolotl.ecc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl
{
    /**
     * A class for representing an identity key.
     * 
     * @author Moxie Marlinspike
     */

    public class IdentityKey
    {

        private ECPublicKey publicKey;

        public IdentityKey(ECPublicKey publicKey)
        {
            this.publicKey = publicKey;
        }

        public IdentityKey(byte[] bytes, int offset)
        {
            this.publicKey = Curve.decodePoint(bytes, offset);
        }

        public ECPublicKey getPublicKey()
        {
            return publicKey;
        }

        public byte[] serialize()
        {
            return publicKey.serialize();
        }

        public String getFingerprint()
        {
            return publicKey.serialize().ToString(); //Hex
        }

        public override bool Equals(Object other)
        {
            if (other == null) return false;
            if (!(other is IdentityKey)) return false;

            return publicKey.Equals(((IdentityKey)other).getPublicKey());
        }


        public override int GetHashCode()
        {
            return publicKey.GetHashCode();
        }
    }
}
