using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.state.impl
{
    public class InMemoryIdentityKeyStore : IdentityKeyStore
    {

        private readonly IDictionary<String, IdentityKey> trustedKeys = new Dictionary<String, IdentityKey>();

        private readonly IdentityKeyPair identityKeyPair;
        private readonly uint localRegistrationId;

        public InMemoryIdentityKeyStore(IdentityKeyPair identityKeyPair, uint localRegistrationId)
        {
            this.identityKeyPair = identityKeyPair;
            this.localRegistrationId = localRegistrationId;
        }

        public /*override*/ IdentityKeyPair getIdentityKeyPair()
        {
            return identityKeyPair;
        }


        public /*override*/ uint getLocalRegistrationId()
        {
            return localRegistrationId;
        }

        public /*override*/ bool saveIdentity(String name, IdentityKey identityKey)
        {
            if (trustedKeys.ContainsKey(name)) //mimic HashMap update behaviour
            {
                trustedKeys.Remove(name);
            }
            trustedKeys.Add(name, identityKey); // put
            return true;
        }

        public /*override*/ bool isTrustedIdentity(String name, IdentityKey identityKey)
        {
            IdentityKey trusted;
            trustedKeys.TryGetValue(name, out trusted); // get(name)
            return (trusted == null || trusted.Equals(identityKey));
        }
    }
}
