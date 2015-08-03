using libaxolotl.ecc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.state
{
    /**
 * A class that contains a remote PreKey and collection
 * of associated items.
 *
 * @author Moxie Marlinspike
 */
    public class PreKeyBundle
    {

        private uint registrationId;

        private uint deviceId;

        private uint preKeyId;
        private ECPublicKey preKeyPublic;

        private uint signedPreKeyId;
        private ECPublicKey signedPreKeyPublic;
        private byte[] signedPreKeySignature;

        private IdentityKey identityKey;

        public PreKeyBundle(uint registrationId, uint deviceId, uint preKeyId, ECPublicKey preKeyPublic,
                            uint signedPreKeyId, ECPublicKey signedPreKeyPublic, byte[] signedPreKeySignature,
                            IdentityKey identityKey)
        {
            this.registrationId = registrationId;
            this.deviceId = deviceId;
            this.preKeyId = preKeyId;
            this.preKeyPublic = preKeyPublic;
            this.signedPreKeyId = signedPreKeyId;
            this.signedPreKeyPublic = signedPreKeyPublic;
            this.signedPreKeySignature = signedPreKeySignature;
            this.identityKey = identityKey;
        }

        /**
         * @return the device ID this PreKey belongs to.
         */
        public uint getDeviceId()
        {
            return deviceId;
        }

        /**
         * @return the unique key ID for this PreKey.
         */
        public uint getPreKeyId()
        {
            return preKeyId;
        }

        /**
         * @return the public key for this PreKey.
         */
        public ECPublicKey getPreKey()
        {
            return preKeyPublic;
        }

        /**
         * @return the unique key ID for this signed prekey.
         */
        public uint getSignedPreKeyId()
        {
            return signedPreKeyId;
        }

        /**
         * @return the signed prekey for this PreKeyBundle.
         */
        public ECPublicKey getSignedPreKey()
        {
            return signedPreKeyPublic;
        }

        /**
         * @return the signature over the signed  prekey.
         */
        public byte[] getSignedPreKeySignature()
        {
            return signedPreKeySignature;
        }

        /**
         * @return the {@link org.whispersystems.libaxolotl.IdentityKey} of this PreKeys owner.
         */
        public IdentityKey getIdentityKey()
        {
            return identityKey;
        }

        /**
         * @return the registration ID associated with this PreKey.
         */
        public uint getRegistrationId()
        {
            return registrationId;
        }
    }
}
