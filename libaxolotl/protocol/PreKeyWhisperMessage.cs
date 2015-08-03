using Google.ProtocolBuffers;
using libaxolotl.ecc;
using libaxolotl.util;
using Strilanc.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static libaxolotl.protocol.WhisperProtos;

namespace libaxolotl.protocol
{
    public partial class PreKeyWhisperMessage : CiphertextMessage
    {

        private readonly uint version;
        private readonly uint registrationId;
        private readonly May<uint> preKeyId;
        private readonly uint signedPreKeyId;
        private readonly ECPublicKey baseKey;
        private readonly IdentityKey identityKey;
        private readonly WhisperMessage message;
        private readonly byte[] serialized;

        public PreKeyWhisperMessage(byte[] serialized)
        {
            try
            {
                this.version = (uint)ByteUtil.highBitsToInt(serialized[0]);

                if (this.version > CiphertextMessage.CURRENT_VERSION)
                {
                    throw new InvalidVersionException("Unknown version: " + this.version);
                }

                WhisperProtos.PreKeyWhisperMessage preKeyWhisperMessage
                    = WhisperProtos.PreKeyWhisperMessage.ParseFrom(ByteString.CopyFrom(serialized, 1,
                                                                                       serialized.Length - 1));

                if ((version == 2 && !preKeyWhisperMessage.HasPreKeyId) ||
                    (version == 3 && !preKeyWhisperMessage.HasSignedPreKeyId) ||
                    !preKeyWhisperMessage.HasBaseKey ||
                    !preKeyWhisperMessage.HasIdentityKey ||
                    !preKeyWhisperMessage.HasMessage)
                {
                    throw new InvalidMessageException("Incomplete message.");
                }

                this.serialized = serialized;
                this.registrationId = preKeyWhisperMessage.RegistrationId;
                this.preKeyId = preKeyWhisperMessage.HasPreKeyId ? new May<uint>(preKeyWhisperMessage.PreKeyId) : May<uint>.NoValue;
                this.signedPreKeyId = preKeyWhisperMessage.HasSignedPreKeyId ? preKeyWhisperMessage.SignedPreKeyId : uint.MaxValue; // -1
                this.baseKey = Curve.decodePoint(preKeyWhisperMessage.BaseKey.ToByteArray(), 0);
                this.identityKey = new IdentityKey(Curve.decodePoint(preKeyWhisperMessage.IdentityKey.ToByteArray(), 0));
                this.message = new WhisperMessage(preKeyWhisperMessage.Message.ToByteArray());
            }
            catch (Exception e)
            {
                //(InvalidProtocolBufferException | InvalidKeyException | LegacyMessage
                throw new InvalidMessageException(e.Message);
            }
        }

        public PreKeyWhisperMessage(uint messageVersion, uint registrationId, May<uint> preKeyId,
                                    uint signedPreKeyId, ECPublicKey baseKey, IdentityKey identityKey,
                                    WhisperMessage message)
        {
            this.version = messageVersion;
            this.registrationId = registrationId;
            this.preKeyId = preKeyId;
            this.signedPreKeyId = signedPreKeyId;
            this.baseKey = baseKey;
            this.identityKey = identityKey;
            this.message = message;

            WhisperProtos.PreKeyWhisperMessage.Builder builder =
                WhisperProtos.PreKeyWhisperMessage.CreateBuilder()
                                                  .SetSignedPreKeyId(signedPreKeyId)
                                                  .SetBaseKey(ByteString.CopyFrom(baseKey.serialize()))
                                                  .SetIdentityKey(ByteString.CopyFrom(identityKey.serialize()))
                                                  .SetMessage(ByteString.CopyFrom(message.serialize()))
                                                  .SetRegistrationId(registrationId);

            if (preKeyId.HasValue) // .isPresent()
            {
                builder.SetPreKeyId(preKeyId.ForceGetValue()); // get()
            }

            byte[] versionBytes = { ByteUtil.intsToByteHighAndLow((int)this.version, (int)CURRENT_VERSION) };
            byte[] messageBytes = builder.Build().ToByteArray();

            this.serialized = ByteUtil.combine(versionBytes, messageBytes);
        }

        public uint getMessageVersion()
        {
            return version;
        }

        public IdentityKey getIdentityKey()
        {
            return identityKey;
        }

        public uint getRegistrationId()
        {
            return registrationId;
        }

        public May<uint> getPreKeyId()
        {
            return preKeyId;
        }

        public uint getSignedPreKeyId()
        {
            return signedPreKeyId;
        }

        public ECPublicKey getBaseKey()
        {
            return baseKey;
        }

        public WhisperMessage getWhisperMessage()
        {
            return message;
        }


        public override byte[] serialize()
        {
            return serialized;
        }


        public override uint getType()
        {
            return CiphertextMessage.PREKEY_TYPE;
        }

    }
}
