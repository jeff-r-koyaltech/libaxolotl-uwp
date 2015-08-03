using libaxolotl.ecc;
using Strilanc.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.ratchet
{
    public class BobAxolotlParameters
    {

        private readonly IdentityKeyPair ourIdentityKey;
        private readonly ECKeyPair ourSignedPreKey;
        private readonly May<ECKeyPair> ourOneTimePreKey;
        private readonly ECKeyPair ourRatchetKey;

        private readonly IdentityKey theirIdentityKey;
        private readonly ECPublicKey theirBaseKey;

        BobAxolotlParameters(IdentityKeyPair ourIdentityKey, ECKeyPair ourSignedPreKey,
                             ECKeyPair ourRatchetKey, May<ECKeyPair> ourOneTimePreKey,
                             IdentityKey theirIdentityKey, ECPublicKey theirBaseKey)
        {
            this.ourIdentityKey = ourIdentityKey;
            this.ourSignedPreKey = ourSignedPreKey;
            this.ourRatchetKey = ourRatchetKey;
            this.ourOneTimePreKey = ourOneTimePreKey;
            this.theirIdentityKey = theirIdentityKey;
            this.theirBaseKey = theirBaseKey;

            if (ourIdentityKey == null || ourSignedPreKey == null || ourRatchetKey == null ||
                ourOneTimePreKey == null || theirIdentityKey == null || theirBaseKey == null)
            {
                throw new Exception("Null value!");
            }
        }

        public IdentityKeyPair getOurIdentityKey()
        {
            return ourIdentityKey;
        }

        public ECKeyPair getOurSignedPreKey()
        {
            return ourSignedPreKey;
        }

        public May<ECKeyPair> getOurOneTimePreKey()
        {
            return ourOneTimePreKey;
        }

        public IdentityKey getTheirIdentityKey()
        {
            return theirIdentityKey;
        }

        public ECPublicKey getTheirBaseKey()
        {
            return theirBaseKey;
        }

        public static Builder newBuilder()
        {
            return new Builder();
        }

        public ECKeyPair getOurRatchetKey()
        {
            return ourRatchetKey;
        }

        public class Builder
        {
            private IdentityKeyPair ourIdentityKey;
            private ECKeyPair ourSignedPreKey;
            private May<ECKeyPair> ourOneTimePreKey;
            private ECKeyPair ourRatchetKey;

            private IdentityKey theirIdentityKey;
            private ECPublicKey theirBaseKey;

            public Builder setOurIdentityKey(IdentityKeyPair ourIdentityKey)
            {
                this.ourIdentityKey = ourIdentityKey;
                return this;
            }

            public Builder setOurSignedPreKey(ECKeyPair ourSignedPreKey)
            {
                this.ourSignedPreKey = ourSignedPreKey;
                return this;
            }

            public Builder setOurOneTimePreKey(May<ECKeyPair> ourOneTimePreKey)
            {
                this.ourOneTimePreKey = ourOneTimePreKey;
                return this;
            }

            public Builder setTheirIdentityKey(IdentityKey theirIdentityKey)
            {
                this.theirIdentityKey = theirIdentityKey;
                return this;
            }

            public Builder setTheirBaseKey(ECPublicKey theirBaseKey)
            {
                this.theirBaseKey = theirBaseKey;
                return this;
            }

            public Builder setOurRatchetKey(ECKeyPair ourRatchetKey)
            {
                this.ourRatchetKey = ourRatchetKey;
                return this;
            }

            public BobAxolotlParameters create()
            {
                return new BobAxolotlParameters(ourIdentityKey, ourSignedPreKey, ourRatchetKey,
                                                ourOneTimePreKey, theirIdentityKey, theirBaseKey);
            }
        }
    }
}
