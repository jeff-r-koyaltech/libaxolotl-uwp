using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.state
{
    public interface SignedPreKeyStore
    {


        /**
         * Load a local SignedPreKeyRecord.
         *
         * @param signedPreKeyId the ID of the local SignedPreKeyRecord.
         * @return the corresponding SignedPreKeyRecord.
         * @throws InvalidKeyIdException when there is no corresponding SignedPreKeyRecord.
         */
        SignedPreKeyRecord loadSignedPreKey(uint signedPreKeyId);

        /**
         * Load all local SignedPreKeyRecords.
         *
         * @return All stored SignedPreKeyRecords.
         */
        List<SignedPreKeyRecord> loadSignedPreKeys();

        /**
         * Store a local SignedPreKeyRecord.
         *
         * @param signedPreKeyId the ID of the SignedPreKeyRecord to store.
         * @param record the SignedPreKeyRecord.
         */
        void storeSignedPreKey(uint signedPreKeyId, SignedPreKeyRecord record);

        /**
         * @param signedPreKeyId A SignedPreKeyRecord ID.
         * @return true if the store has a record for the signedPreKeyId, otherwise false.
         */
        bool containsSignedPreKey(uint signedPreKeyId);

        /**
         * Delete a SignedPreKeyRecord from local storage.
         *
         * @param signedPreKeyId The ID of the SignedPreKeyRecord to remove.
         */
        void removeSignedPreKey(uint signedPreKeyId);
    }
}
