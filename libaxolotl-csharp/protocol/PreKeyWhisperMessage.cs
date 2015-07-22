using libaxolotl_csharp.ecc;
using libaxolotl_csharp.util;
using System;

namespace libaxolotl_csharp.protocol
{
	public class PreKeyWhisperMessage : CiphertextMessage
	{
		private readonly int version;
		private readonly uint? registrationId;
		private readonly uint? preKeyId;
		private readonly uint? signedPreKeyId;
		private readonly ECPublicKey baseKey;
		private readonly IdentityKey identityKey;
		private readonly Textsecure.WhisperMessage message;
		private readonly byte[] serialized;

		public PreKeyWhisperMessage(byte[] serialized)
		//throws InvalidMessageException, InvalidVersionException
		{
			try {
				this.version = ByteUtil.highBitsToInt(serialized[0]);

				if (this.version > CiphertextMessage.CURRENT_VERSION) {
					throw new InvalidVersionException(string.Format(Resources.LibAxolotl.GetString("UnknownVersion"), this.version));
				}

				Textsecure.PreKeyWhisperMessage preKeyWhisperMessage =
							Textsecure.PreKeyWhisperMessage.Deserialize(ByteUtil.TrimProtoBuf(serialized));

				if ((version == 2 && !preKeyWhisperMessage.hasPreKeyId()) ||
					(version == 3 && !preKeyWhisperMessage.hasSignedPreKeyId()) ||
					!preKeyWhisperMessage.hasBaseKey() ||
					!preKeyWhisperMessage.hasIdentityKey() ||
					!preKeyWhisperMessage.hasMessage())
				{
					throw new InvalidMessageException("Incomplete message.");
				}

				this.serialized = serialized;
				this.registrationId = preKeyWhisperMessage.RegistrationId;
				this.preKeyId = preKeyWhisperMessage.PreKeyId;
				this.signedPreKeyId = preKeyWhisperMessage.SignedPreKeyId;
				this.baseKey = Curve.decodePoint(preKeyWhisperMessage.BaseKey.toByteArray(), 0);
				this.identityKey = new IdentityKey(Curve.decodePoint(preKeyWhisperMessage.IdentityKey.serialize(), 0));
				this.message = preKeyWhisperMessage.Message;
			} catch (Exception e) {
				throw new InvalidMessageException("", e);
			}
		}

		public PreKeyWhisperMessage(int messageVersion, uint? registrationId, uint? preKeyId,
									uint? signedPreKeyId, ECPublicKey baseKey, IdentityKey identityKey,
									Textsecure.WhisperMessage message)
		{
			this.version = messageVersion;
			this.registrationId = registrationId;
			this.preKeyId = preKeyId;
			this.signedPreKeyId = signedPreKeyId;
			this.baseKey = baseKey;
			this.identityKey = identityKey;
			this.message = message;

			//TODO: We should probably look into an immutable byte [] solution (like ByteString for the Java side).
			//For now, "normal"/mutable byte arrays will suffice. If performance is bad, it will probably help to
			//revisit and solve this.

			Textsecure.PreKeyWhisperMessage preKeyWhisperMessage = new Textsecure.PreKeyWhisperMessage()
			{
				SignedPreKeyId = signedPreKeyId,
				BaseKey = baseKey,
				IdentityKey = identityKey,
				Message = message,
				RegistrationId = registrationId
			};

			if (preKeyId.HasValue)
			{
				preKeyWhisperMessage.PreKeyId = preKeyId;
			}

			byte[] versionBytes = { ByteUtil.intsToByteHighAndLow(this.version, CURRENT_VERSION) };
			byte[] messageBytes = Textsecure.PreKeyWhisperMessage.SerializeToBytes(preKeyWhisperMessage);

			this.serialized = ByteUtil.combine(versionBytes, messageBytes);
		}

		public int getMessageVersion()
		{
			return version;
		}

		public IdentityKey getIdentityKey()
		{
			return identityKey;
		}

		public uint? getRegistrationId()
		{
			return registrationId;
		}

		public uint? getPreKeyId()
		{
			return preKeyId;
		}

		public uint? getSignedPreKeyId()
		{
			return signedPreKeyId;
		}

		public ECPublicKey getBaseKey()
		{
			return baseKey;
		}

		public Textsecure.WhisperMessage getWhisperMessage()
		{
			return message;
		}
		
		public override byte[] serialize()
		{
			return serialized;
		}

		public override int getType()
		{
			return CiphertextMessage.PREKEY_TYPE;
		}
	}
}