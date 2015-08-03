using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.protocol
{
    public abstract class CiphertextMessage // interface
    {

        public const uint UNSUPPORTED_VERSION = 1;
        public const uint CURRENT_VERSION = 3;

        public const uint WHISPER_TYPE = 2;
        public const uint PREKEY_TYPE = 3;
        public const uint SENDERKEY_TYPE = 4;
        public const uint SENDERKEY_DISTRIBUTION_TYPE = 5;

        // This should be the worst case (worse than V2).  So not always accurate, but good enough for padding.
        public const uint ENCRYPTED_MESSAGE_OVERHEAD = 53;

        public abstract byte[] serialize(); // -abstract
        public abstract uint getType(); // -abstract

    }
}
