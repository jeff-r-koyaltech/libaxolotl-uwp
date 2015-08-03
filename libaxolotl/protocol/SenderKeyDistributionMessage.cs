using Google.ProtocolBuffers;
using libaxolotl.ecc;
using libaxolotl.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.protocol
{
    public partial class SenderKeyDistributionMessage : CiphertextMessage
    {

        private readonly uint id;
        private readonly uint iteration;
        private readonly byte[] chainKey;
        private readonly ECPublicKey signatureKey;
        private readonly byte[] serialized;

        public SenderKeyDistributionMessage(uint id, uint iteration, byte[] chainKey, ECPublicKey signatureKey)
        {
            byte[] version = { ByteUtil.intsToByteHighAndLow((int)CURRENT_VERSION, (int)CURRENT_VERSION) };
            byte[] protobuf = WhisperProtos.SenderKeyDistributionMessage.CreateBuilder()
                                                                        .SetId(id)
                                                                        .SetIteration(iteration)
                                                                        .SetChainKey(ByteString.CopyFrom(chainKey))
                                                                        .SetSigningKey(ByteString.CopyFrom(signatureKey.serialize()))
                                                                        .Build().ToByteArray();

            this.id = id;
            this.iteration = iteration;
            this.chainKey = chainKey;
            this.signatureKey = signatureKey;
            this.serialized = ByteUtil.combine(version, protobuf);
        }

        public SenderKeyDistributionMessage(byte[] serialized)
        {
            try
            {
                byte[][] messageParts = ByteUtil.split(serialized, 1, serialized.Length - 1);
                byte version = messageParts[0][0];
                byte[] message = messageParts[1];

                if (ByteUtil.highBitsToInt(version) < CiphertextMessage.CURRENT_VERSION)
                {
                    throw new LegacyMessageException("Legacy message: " + ByteUtil.highBitsToInt(version));
                }

                if (ByteUtil.highBitsToInt(version) > CURRENT_VERSION)
                {
                    throw new InvalidMessageException("Unknown version: " + ByteUtil.highBitsToInt(version));
                }

                WhisperProtos.SenderKeyDistributionMessage distributionMessage = WhisperProtos.SenderKeyDistributionMessage.ParseFrom(message);

                if (!distributionMessage.HasId ||
                    !distributionMessage.HasIteration ||
                    !distributionMessage.HasChainKey ||
                    !distributionMessage.HasSigningKey)
                {
                    throw new InvalidMessageException("Incomplete message.");
                }

                this.serialized = serialized;
                this.id = distributionMessage.Id;
                this.iteration = distributionMessage.Iteration;
                this.chainKey = distributionMessage.ChainKey.ToByteArray();
                this.signatureKey = Curve.decodePoint(distributionMessage.SigningKey.ToByteArray(), 0);
            }
            catch (Exception e)
            {
                //InvalidProtocolBufferException | InvalidKey
                throw new InvalidMessageException(e);
            }
        }

        public override byte[] serialize()
        {
            return serialized;
        }


        public override uint getType()
        {
            return SENDERKEY_DISTRIBUTION_TYPE;
        }

        public uint getIteration()
        {
            return iteration;
        }

        public byte[] getChainKey()
        {
            return chainKey;
        }

        public ECPublicKey getSignatureKey()
        {
            return signatureKey;
        }

        public uint getId()
        {
            return id;
        }
    }
}
