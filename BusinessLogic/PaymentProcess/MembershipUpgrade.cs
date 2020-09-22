using BusinessLogic.Domain;
using BusinessLogic.Repositories;
using BusinessLogic.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.PaymentProcess
{
    // If the payment is an upgrade to a membership, apply the upgrade.
    // If the payment is for a membership or upgrade, e-mail the owner and inform them of the activation/upgrade.
    public class MembershipUpgrade : IPaymentProcess
    {
        private IRepositories _repo;
        private INotificationServices _notificationService;

        public MembershipUpgrade(IRepositories repo, INotificationServices notificationService)
        {
            _repo = repo;
            _notificationService = notificationService;
        }


        public void run(Payment payment)
        {
            Order order = payment.getOrder();
            Customer customer = order.getCustomer();
            LineItem[] lineItems = order.getLineItems();
            foreach (LineItem lineItem in lineItems)
            {
                if (!lineItem.hasCategory(ProductCategory.Membership))
                    continue;

                Membership membership = _repo.findByProduct(lineItem.getProduct());
                Membership previousLevel = membership.getPreviousLevel();
                if (customer.hasMembership(previousLevel))
                    customer.addMembership(membership, _notificationService);
            }
        }
    }
}
