using BusinessLogic.Domain;
using BusinessLogic.Repositories;
using BusinessLogic.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.PaymentProcess
{
    public class MembershipActivate : IPaymentProcess
    {
        private IRepositories _service;
        private INotificationServices _notificationService;

        public MembershipActivate(IRepositories service, INotificationServices notificationService)
        {
            _service = service;
            _notificationService = notificationService;
        }


        public void run(Payment payment)
        {
            Order order = payment.getOrder();
            LineItem[] lineItems = order.getLineItems();
            Customer customer = order.getCustomer();
            foreach (LineItem lineItem in lineItems)
            {
                if (!lineItem.hasCategory(ProductCategory.Membership))
                    continue;

                Membership membership = _service.findByProduct(lineItem.getProduct());
                if (membership != null)
                    customer.addMembership(membership, _notificationService);
            }
        }
    }
}
