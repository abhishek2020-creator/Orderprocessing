using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Domain
{
    public class Membership : Product
    {

        public Membership(String product, Membership prev) : base(product)
        {
            _previous = prev;
        }

        private Membership _previous;
        public Membership getPreviousLevel()
        {
            return _previous;
        }
    }
}
