using libaxolotl.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.groups.ratchet
{
    /**
     * Each SenderKey is a "chain" of keys, each derived from the previous.
     *
     * At any given point in time, the state of a SenderKey can be represented
     * as the current chain key value, along with its iteration count.  From there,
     * subsequent iterations can be derived, as well as individual message keys from
     * each chain key.
     *
     * @author
    */
    public class SenderChainKey
    {

        private static readonly byte[] MESSAGE_KEY_SEED = { 0x01 };
        private static readonly byte[] CHAIN_KEY_SEED = { 0x02 };

        private readonly uint iteration;
        private readonly byte[] chainKey;

        public SenderChainKey(uint iteration, byte[] chainKey)
        {
            this.iteration = iteration;
            this.chainKey = chainKey;
        }

        public uint getIteration()
        {
            return iteration;
        }

        public SenderMessageKey getSenderMessageKey()
        {
            return new SenderMessageKey(iteration, getDerivative(MESSAGE_KEY_SEED, chainKey));
        }

        public SenderChainKey getNext()
        {
            return new SenderChainKey(iteration + 1, getDerivative(CHAIN_KEY_SEED, chainKey));
        }

        public byte[] getSeed()
        {
            return chainKey;
        }

        private byte[] getDerivative(byte[] seed, byte[] key)
        {
            // try
            //{
            return Sign.sha256sum(key, seed);
            /*}
            catch (NoSuchAlgorithmException | InvalidKeyException e) {
                throw new AssertionError(e);
            }*/
        }

    }
}
