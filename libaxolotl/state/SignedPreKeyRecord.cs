using Google.ProtocolBuffers;
using libaxolotl.ecc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static libaxolotl.state.StorageProtos;

namespace libaxolotl.state
{
    public class SignedPreKeyRecord
    {

        private SignedPreKeyRecordStructure structure;

        public SignedPreKeyRecord(uint id, ulong timestamp, ECKeyPair keyPair, byte[] signature)
        {
            this.structure = SignedPreKeyRecordStructure.CreateBuilder()
                                                        .SetId(id)
                                                        .SetPublicKey(ByteString.CopyFrom(keyPair.getPublicKey()
                                                                                                 .serialize()))
                                                        .SetPrivateKey(ByteString.CopyFrom(keyPair.getPrivateKey()
                                                                                                  .serialize()))
                                                        .SetSignature(ByteString.CopyFrom(signature))
                                                        .SetTimestamp(timestamp)
                                                        .Build();
        }

        public SignedPreKeyRecord(byte[] serialized)
        {
            this.structure = SignedPreKeyRecordStructure.ParseFrom(serialized);
        }

        public uint getId()
        {
            return this.structure.Id;
        }

        public ulong getTimestamp()
        {
            return this.structure.Timestamp;
        }

        public ECKeyPair getKeyPair()
        {
            try
            {
                ECPublicKey publicKey = Curve.decodePoint(this.structure.PublicKey.ToByteArray(), 0);
                ECPrivateKey privateKey = Curve.decodePrivatePoint(this.structure.PrivateKey.ToByteArray());

                return new ECKeyPair(publicKey, privateKey);
            }
            catch (InvalidKeyException e)
            {
                throw new Exception(e.Message);
            }
        }

        public byte[] getSignature()
        {
            return this.structure.Signature.ToByteArray();
        }

        public byte[] serialize()
        {
            return this.structure.ToByteArray();
        }
    }
}
