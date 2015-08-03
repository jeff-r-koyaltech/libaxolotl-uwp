using libaxolotl.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.state
{
    /**
     * An interface describing the local storage of {@link PreKeyRecord}s.
     *
     * @author
     */
    public interface PreKeyStore
    {

        /**
         * Load a local PreKeyRecord.
         *
         * @param preKeyId the ID of the local PreKeyRecord.
         * @return the corresponding PreKeyRecord.
         * @throws InvalidKeyIdException when there is no corresponding PreKeyRecord.
         */
        PreKeyRecord loadPreKey(uint preKeyId);

        /**
         * Store a local PreKeyRecord.
         *
         * @param preKeyId the ID of the PreKeyRecord to store.
         * @param record the PreKeyRecord.
         */
        void storePreKey(uint preKeyId, PreKeyRecord record);

        /**
         * @param preKeyId A PreKeyRecord ID.
         * @return true if the store has a record for the preKeyId, otherwise false.
         */
         bool containsPreKey(uint preKeyId);

        /**
         * Delete a PreKeyRecord from local storage.
         *
         * @param preKeyId The ID of the PreKeyRecord to remove.
         */
        void removePreKey(uint preKeyId);

    }
}
