using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.groups
{
    /**
     * A representation of a (groupId + senderId + deviceId) tuple.
     */
    public class SenderKeyName
    {

        private readonly String groupId;
        private readonly AxolotlAddress sender;

        public SenderKeyName(String groupId, AxolotlAddress sender)
        {
            this.groupId = groupId;
            this.sender = sender;
        }

        public String getGroupId()
        {
            return groupId;
        }

        public AxolotlAddress getSender()
        {
            return sender;
        }

        public String serialize()
        {
            return groupId + "::" + sender.getName() + "::" + sender.getDeviceId();
        }


        public override bool Equals(Object other)
        {
            if (other == null) return false;
            if (!(other is SenderKeyName)) return false;

            SenderKeyName that = (SenderKeyName)other;

            return
                this.groupId.Equals(that.groupId) &&
                this.sender.Equals(that.sender);
        }

        public override int GetHashCode()
        {
            return this.groupId.GetHashCode() ^ this.sender.GetHashCode();
        }

    }
}
