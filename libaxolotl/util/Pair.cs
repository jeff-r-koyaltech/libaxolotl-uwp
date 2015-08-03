using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.util
{
    public class Pair<T1, T2>
    {
        private readonly T1 v1;
        private readonly T2 v2;

        public Pair(T1 v1, T2 v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public T1 first()
        {
            return v1;
        }

        public T2 second()
        {
            return v2;
        }

        public bool equals(Object o)
        {
            // TODO
            return false;
            /*return o is Pair &&
                equal(((Pair)o).first(), first()) &&
                equal(((Pair)o).second(), second());*/
        }

        public int hashCode()
        {
            return first().GetHashCode() ^ second().GetHashCode();
        }

        private bool equal(Object first, Object second)
        {
            if (first == null && second == null) return true;
            if (first == null || second == null) return false;
            return first.Equals(second);
        }
    }
}
