using BusinessLogic.PaymentProcess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Domain
{
    public class Payment
    {
        private Order _order;
        public Payment(Order order)
        {
            _order = order;
        }
        public Order getOrder()
        {
            return _order;
        }

        public void Process(IPaymentProcess[] rules)
        {
            foreach (IPaymentProcess rule in rules)
            {
                rule.run(this);
            }
                
        }
    }
}
