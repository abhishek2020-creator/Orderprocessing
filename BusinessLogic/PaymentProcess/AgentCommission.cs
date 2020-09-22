using BusinessLogic.Domain;
using BusinessLogic.PaymentProcess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.PaymentProcess
{
    public class AgentCommissionHandler : IPaymentProcess
    {
        public void run(Payment payment)
        {
            Order order = payment.getOrder();
            LineItem[] lineItems = order.getLineItems();

            Boolean addCommission = false;

            foreach (LineItem lineItem in lineItems)
            {
                if (lineItem.hasCategory(ProductCategory.Books) || lineItem.hasCategory(ProductCategory.Physical))
                {
                    addCommission = true;
                    break;
                }
            }

            if (addCommission)
            {
                Agent agent = order.getAgent();
                agent.generateCommission(order);
            }
        }

    }
}
