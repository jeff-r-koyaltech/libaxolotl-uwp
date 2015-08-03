using libaxolotl.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.state
{
    public interface AxolotlStore : IdentityKeyStore, PreKeyStore, SessionStore, SignedPreKeyStore
    {
    }

}
