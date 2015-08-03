using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.groups.state
{
    public interface SenderKeyStore
    {

        /**
         * Commit to storage the {@link org.whispersystems.libaxolotl.groups.state.SenderKeyRecord} for a
         * given (groupId + senderId + deviceId) tuple.
         *
         * @param senderKeyName the (groupId + senderId + deviceId) tuple.
         * @param record the current SenderKeyRecord for the specified senderKeyName.
         */
        void storeSenderKey(SenderKeyName senderKeyName, SenderKeyRecord record);

        /**
         * Returns a copy of the {@link org.whispersystems.libaxolotl.groups.state.SenderKeyRecord}
         * corresponding to the (groupId + senderId + deviceId) tuple, or a new SenderKeyRecord if
         * one does not currently exist.
         * <p>
         * It is important that implementations return a copy of the current durable information.  The
         * returned SenderKeyRecord may be modified, but those changes should not have an effect on the
         * durable session state (what is returned by subsequent calls to this method) without the
         * store method being called here first.
         *
         * @param senderKeyName The (groupId + senderId + deviceId) tuple.
         * @return a copy of the SenderKeyRecord corresponding to the (groupId + senderId + deviceId tuple, or
         *         a new SenderKeyRecord if one does not currently exist.
         */

        SenderKeyRecord loadSenderKey(SenderKeyName senderKeyName);
    }
}
