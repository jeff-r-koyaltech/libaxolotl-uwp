using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl
{
    public class AxolotlAddress
    {

        private readonly String name;
        private readonly uint deviceId;

        public AxolotlAddress(String name, uint deviceId)
        {
            this.name = name;
            this.deviceId = deviceId;
        }

        public String getName()
        {
            return name;
        }

        public uint getDeviceId()
        {
            return deviceId;
        }

        public override String ToString()
        {
            return name + ":" + deviceId;
        }

        public override bool Equals(Object other)
        {
            if (other == null) return false;
            if (!(other is AxolotlAddress)) return false;

            AxolotlAddress that = (AxolotlAddress)other;
            return this.name.Equals(that.name) && this.deviceId == that.deviceId;
        }


        public override int GetHashCode()
        {
            return this.name.GetHashCode() ^ (int)this.deviceId;
        }
    }
}
