using libaxolotl.ecc;
using libaxolotl.groups.state;
using libaxolotl.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static libaxolotl.state.StorageProtos;

namespace libaxolotl.groups
{
    /**
     * A durable representation of a set of SenderKeyStates for a specific
     * SenderKeyName.
     *
     * @author
     */
    public class SenderKeyRecord
    {

        private IList<SenderKeyState> senderKeyStates = new List<SenderKeyState>();

        public SenderKeyRecord() { }

        public SenderKeyRecord(byte[] serialized)
        {
            SenderKeyRecordStructure senderKeyRecordStructure = SenderKeyRecordStructure.ParseFrom(serialized);

            foreach (StorageProtos.SenderKeyStateStructure structure in senderKeyRecordStructure.SenderKeyStatesList)
            {
                this.senderKeyStates.Add(new SenderKeyState(structure));
            }
        }

        public bool isEmpty()
        {
            return senderKeyStates.Count == 0;
        }

        public SenderKeyState getSenderKeyState()
        {
            if (!isEmpty())
            {
                return senderKeyStates[0];
            }
            else
            {
                throw new InvalidKeyIdException("No key state in record!");
            }
        }

        public SenderKeyState getSenderKeyState(uint keyId)
        {
            foreach (SenderKeyState state in senderKeyStates)
            {
                if (state.getKeyId() == keyId)
                {
                    return state;
                }
            }

            throw new InvalidKeyIdException("No keys for: " + keyId);
        }

        public void addSenderKeyState(uint id, uint iteration, byte[] chainKey, ECPublicKey signatureKey)
        {
            senderKeyStates.Add(new SenderKeyState(id, iteration, chainKey, signatureKey));
        }

        public void setSenderKeyState(uint id, uint iteration, byte[] chainKey, ECKeyPair signatureKey)
        {
            senderKeyStates.Clear();
            senderKeyStates.Add(new SenderKeyState(id, iteration, chainKey, signatureKey));
        }

        public byte[] serialize()
        {
            SenderKeyRecordStructure.Builder recordStructure = SenderKeyRecordStructure.CreateBuilder();

            foreach (SenderKeyState senderKeyState in senderKeyStates)
            {
                recordStructure.AddSenderKeyStates(senderKeyState.getStructure());
            }

            return recordStructure.Build().ToByteArray();
        }
    }
}
