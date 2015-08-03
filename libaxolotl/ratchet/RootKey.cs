using libaxolotl.ecc;
using libaxolotl.kdf;
using libaxolotl.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.ratchet
{
    public class RootKey
    {

        private readonly HKDF kdf;
        private readonly byte[] key;

        public RootKey(HKDF kdf, byte[] key)
        {
            this.kdf = kdf;
            this.key = key;
        }

        public byte[] getKeyBytes()
        {
            return key;
        }

        public Pair<RootKey, ChainKey> createChain(ECPublicKey theirRatchetKey, ECKeyPair ourRatchetKey)
        {
            byte[] sharedSecret = Curve.calculateAgreement(theirRatchetKey, ourRatchetKey.getPrivateKey());
            byte[] derivedSecretBytes = kdf.deriveSecrets(sharedSecret, key, Encoding.UTF8.GetBytes("WhisperRatchet"), DerivedRootSecrets.SIZE);
            DerivedRootSecrets derivedSecrets = new DerivedRootSecrets(derivedSecretBytes);

            RootKey newRootKey = new RootKey(kdf, derivedSecrets.getRootKey());
            ChainKey newChainKey = new ChainKey(kdf, derivedSecrets.getChainKey(), 0);

            return new Pair<RootKey, ChainKey>(newRootKey, newChainKey);
        }
    }
}
