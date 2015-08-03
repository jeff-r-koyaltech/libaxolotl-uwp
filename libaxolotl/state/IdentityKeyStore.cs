using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.state
{
    /**
     * Provides an interface to identity information.
     *
     * @author
     */
    public interface IdentityKeyStore
    {

        /**
         * Get the local client's identity key pair.
         *
         * @return The local client's persistent identity key pair.
         */
        IdentityKeyPair getIdentityKeyPair();

        /**
         * Return the local client's registration ID.
         * <p>
         * Clients should maintain a registration ID, a random number
         * between 1 and 16380 that's generated once at install time.
         *
         * @return the local client's registration ID.
         */
        uint getLocalRegistrationId();

        /**
         * Save a remote client's identity key
         * <p>
         * Store a remote client's identity key as trusted.
         *
         * @param name        The name of the remote client.
         * @param identityKey The remote client's identity key.
         */
        bool saveIdentity(String name, IdentityKey identityKey);


        /**
         * Verify a remote client's identity key.
         * <p>
         * Determine whether a remote client's identity is trusted.  Convention is
         * that the TextSecure protocol is 'trust on first use.'  This means that
         * an identity key is considered 'trusted' if there is no entry for the recipient
         * in the local store, or if it matches the saved key for a recipient in the local
         * store.  Only if it mismatches an entry in the local store is it considered
         * 'untrusted.'
         *
         * @param name        The name of the remote client.
         * @param identityKey The identity key to verify.
         * @return true if trusted, false if untrusted.
         */
        bool isTrustedIdentity(String name, IdentityKey identityKey);

    }
}
