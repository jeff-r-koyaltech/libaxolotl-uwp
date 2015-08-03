using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.state.impl
{
    public class InMemorySignedPreKeyStore : SignedPreKeyStore
    {

        private readonly IDictionary<uint, byte[]> store = new Dictionary<uint, byte[]>();


        public SignedPreKeyRecord loadSignedPreKey(uint signedPreKeyId)
        {
            try
            {
                if (!store.ContainsKey(signedPreKeyId))
                {
                    throw new InvalidKeyIdException("No such signedprekeyrecord! " + signedPreKeyId);
                }

                byte[] record;
                store.TryGetValue(signedPreKeyId, out record);  // get()

                return new SignedPreKeyRecord(record);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<SignedPreKeyRecord> loadSignedPreKeys()
        {
            try
            {
                List<SignedPreKeyRecord> results = new List<SignedPreKeyRecord>();

                foreach (byte[] serialized in store.Values) //values()
                {
                    results.Add(new SignedPreKeyRecord(serialized));
                }

                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void storeSignedPreKey(uint signedPreKeyId, SignedPreKeyRecord record)
        {
            if (store.ContainsKey(signedPreKeyId)) // mimic Java HashMap
            {
                store.Remove(signedPreKeyId);
            }
            store.Add(signedPreKeyId, record.serialize());
        }


        public bool containsSignedPreKey(uint signedPreKeyId)
        {
            return store.ContainsKey(signedPreKeyId);
        }


        public void removeSignedPreKey(uint signedPreKeyId)
        {
            store.Remove(signedPreKeyId);
        }
    }
}
