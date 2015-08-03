using Google.ProtocolBuffers;
using libaxolotl;
using libaxolotl.ecc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static libaxolotl.state.StorageProtos;

namespace libaxolotl
{
    /**
     * Holder for public and private identity key pair.
     *
     * @author
     */
    public class IdentityKeyPair
    {

        private readonly IdentityKey publicKey;
        private readonly ECPrivateKey privateKey;

        public IdentityKeyPair(IdentityKey publicKey, ECPrivateKey privateKey)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;
        }

        public IdentityKeyPair(byte[] serialized)
        {
            try
            {
                IdentityKeyPairStructure structure = IdentityKeyPairStructure.ParseFrom(serialized);
                this.publicKey = new IdentityKey(structure.PublicKey.ToByteArray(), 0);
                this.privateKey = Curve.decodePrivatePoint(structure.PrivateKey.ToByteArray());
            }
            catch (InvalidProtocolBufferException e)
            {
                throw new InvalidKeyException(e);
            }
        }

        public IdentityKey getPublicKey()
        {
            return publicKey;
        }

        public ECPrivateKey getPrivateKey()
        {
            return privateKey;
        }

        public byte[] serialize()
        {
            return IdentityKeyPairStructure.CreateBuilder()
                                           .SetPublicKey(ByteString.CopyFrom(publicKey.serialize()))
                                           .SetPrivateKey(ByteString.CopyFrom(privateKey.serialize()))
                                           .Build().ToByteArray();
        }
    }
}
