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
    public class PreKeyRecord
    {

        private PreKeyRecordStructure structure;

        public PreKeyRecord(uint id, ECKeyPair keyPair)
        {
            this.structure = PreKeyRecordStructure.CreateBuilder()
                                                  .SetId(id)
                                                  .SetPublicKey(ByteString.CopyFrom(keyPair.getPublicKey()
                                                                                           .serialize()))
                                                  .SetPrivateKey(ByteString.CopyFrom(keyPair.getPrivateKey()
                                                                                            .serialize()))
                                                  .Build();
        }

        public PreKeyRecord(byte[] serialized)
        {
            this.structure = PreKeyRecordStructure.ParseFrom(serialized);
        }

        public uint getId()
        {
            return this.structure.Id;
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

        public byte[] serialize()
        {
            return this.structure.ToByteArray();
        }
    }
}
