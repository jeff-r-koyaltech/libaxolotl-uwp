using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl_csharp.state
{
	public interface AxolotlStore : IdentityKeyStore, PreKeyStore, SessionStore, SignedPreKeyStore
	{
	}
}
