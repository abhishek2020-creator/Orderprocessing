using BusinessLogic.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Domain
{
    public class Customer
    {
        public void addMembership(Membership membership, INotificationServices notificationService)
        {
            notificationService.notify(this, membership);
        }
        public Boolean hasMembership(Membership membership)
        {
            return false;
        }
    }
}
