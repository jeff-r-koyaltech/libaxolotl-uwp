using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.state.impl
{
    public class InMemoryPreKeyStore : PreKeyStore
    {

        private readonly IDictionary<uint, byte[]> store = new Dictionary<uint, byte[]>();


        public PreKeyRecord loadPreKey(uint preKeyId)
        {
            try
            {
                if (!store.ContainsKey(preKeyId))
                {
                    throw new InvalidKeyIdException("No such prekeyrecord!");
                }
                byte[] record;
                store.TryGetValue(preKeyId, out record);  // get()

                return new PreKeyRecord(record);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void storePreKey(uint preKeyId, PreKeyRecord record)
        {
            if (store.ContainsKey(preKeyId)) // mimic Java HashMap
            {
                store.Remove(preKeyId);
            }
            store.Add(preKeyId, record.serialize()); // put
        }


        public bool containsPreKey(uint preKeyId)
        {
            return store.ContainsKey(preKeyId);
        }


        public void removePreKey(uint preKeyId)
        {
            store.Remove(preKeyId);
        }
    }
}
