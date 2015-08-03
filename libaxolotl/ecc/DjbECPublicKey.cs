using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libaxolotl.util;

namespace libaxolotl.ecc
{
    public class DjbECPublicKey : ECPublicKey
    {
        private readonly byte[] publicKey;

        public DjbECPublicKey(byte[] publicKey)
        {
            this.publicKey = publicKey;
        }


        public byte[] serialize()
        {
            byte[] type = { (byte)Curve.DJB_TYPE };
            return ByteUtil.combine(type, publicKey);
        }


        public int getType()
        {
            return Curve.DJB_TYPE;
        }


        public override bool Equals(Object other)
        {
            if (other == null) return false;
            if (!(other is DjbECPublicKey)) return false;

            DjbECPublicKey that = (DjbECPublicKey)other;
            return Enumerable.SequenceEqual(this.publicKey, that.publicKey);
        }


        public override int GetHashCode()
        {
            return string.Join(",", publicKey).GetHashCode();
        }


        public int CompareTo(Object another)
        {
            byte[] theirs = ((DjbECPublicKey)another).publicKey;
            String theirString = string.Join(",", theirs.Select(y => y.ToString()));
            String ourString = string.Join(",", publicKey.Select(y => y.ToString()));
            return ourString.CompareTo(theirString);
        }

        public byte[] getPublicKey()
        {
            return publicKey;
        }

    }
}
